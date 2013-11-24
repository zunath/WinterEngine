using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.Library.Utility;
using WinterEngine.Network.Configuration;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.XMLObjects;
using Newtonsoft.Json;
using WinterEngine.DataTransferObjects.ViewModels;
using WinterEngine.DataTransferObjects.Packets;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Network.Clients
{
    public class WebServiceClientUtility
    {
        
        private JsonSerializerSettings SerializerSettings { get; set; }

        public WebServiceClientUtility()
        {
            this.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        /// <summary>
        /// Builds the URL to the master server.
        /// </summary>
        /// <param name="methodType">The method type to use.</param>
        /// <returns></returns>
        private string BuildURLString(WebServiceMethodTypeEnum methodType)
        {
            return MasterServerConfiguration.MasterServerURL + "api/APIControllers/" + EnumerationHelper.GetEnumerationDescription(methodType) + "/";
        }

        /// <summary>
        /// Sends a GET request to the master server and returns the HTTP or Json result.
        /// </summary>
        /// <param name="methodName">Name of the method on the server to use.</param>
        /// <param name="methodType">The type of method</param>
        /// <returns></returns>
        private string SendGetRequest(string methodName, WebServiceMethodTypeEnum methodType, string queryString = "")
        {
            WebRequest request = WebRequest.Create(BuildURLString(methodType) + methodName + queryString);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            {
                if (IsMasterServerAlive())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        return ReplaceJsonArraySpecialCharacters(reader.ReadToEnd());
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Sends a POST request to the master server, including a Json object.
        /// Returns the web server's Json result.
        /// </summary>
        /// <param name="methodName">The name of the method to call on the server.</param>
        /// <param name="methodType">The type of method to use.</param>
        /// <param name="jsonObject">The Json object to send in the request.</param>
        /// <returns></returns>
        private string SendJsonRequest(string methodName, WebServiceMethodTypeEnum methodType, object jsonObject)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BuildURLString(methodType) + methodName);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            if (IsMasterServerAlive())
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonObject);
                    writer.Flush();
                    writer.Close();
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        return ReplaceJsonArraySpecialCharacters(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// Queries the Winter Engine web service and returns the user's external IP address.
        /// If the query fails, the default value will be displayed ("Unknown")
        /// </summary>
        /// <returns></returns>
        public string GetExternalIPAddress()
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return SendGetRequest("GetServerIPAddress", WebServiceMethodTypeEnum.Server);
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Sends a server's details to the master server which
        /// tracks the active server list.
        /// </summary>
        /// <param name="details">The server details to send to the master server.</param>
        public string SendServerDetails(ServerViewModel viewModel)
        {
            try
            {
                bool isActive = false;
                if (viewModel.ServerStatus == ServerStatusEnum.Running ||
                   viewModel.ServerStatus == ServerStatusEnum.Starting)
                {
                    isActive = true;
                }

                MasterServerStatusPacket packet = new MasterServerStatusPacket
                {
                    GameTypeID = (byte)viewModel.ServerSettings.GameType,
                    IsAutoDownloadEnabled = viewModel.ServerSettings.AllowFileAutoDownload,
                    IsCharacterDeletionEnabled = viewModel.ServerSettings.AllowCharacterDeletion,
                    PVPTypeID = (byte)viewModel.ServerSettings.PVPSetting,
                    ServerCurrentPlayers = viewModel.ConnectedUsernames.Count(),
                    ServerDescription = viewModel.ServerSettings.Description,
                    ServerIPAddress = viewModel.ServerIPAddress,
                    ServerMaxLevel = viewModel.ServerSettings.MaxLevel,
                    ServerMaxPlayers = viewModel.ServerSettings.MaxPlayers,
                    ServerName = viewModel.ServerSettings.Name,
                    ServerPort = viewModel.ServerSettings.PortNumber,
                    IsActive = isActive
                };

                string json = JsonConvert.SerializeObject(packet);
                string result = SendJsonRequest("UpdateServerDetails", WebServiceMethodTypeEnum.Server, json);



                return result;
            }
            catch
            {
                throw;
            }
        }

        private string ReplaceJsonArraySpecialCharacters(string jsonObject)
        {
            if (jsonObject.Length > 1)
            {
                jsonObject = jsonObject.Replace("\\", "");                // Remove the back slashes
                jsonObject = jsonObject.Remove(0, 1);                     // Remove quote at beginning of Json object
                jsonObject = jsonObject.Remove(jsonObject.Length - 1, 1); // Remove quote at end of Json object
            }

            return jsonObject;
        }

        public string GetAllActiveServers()
        {
            try
            {
                return SendGetRequest("GetAllActiveServers", WebServiceMethodTypeEnum.Server);
            }
            catch(Exception ex)
            {
                throw new Exception("WebServiceClientUtility -> GetAllActiveServers()", ex);
            }
        }

        /// <summary>
        /// Sends a user's master server login credentials to the master server.
        /// If successful, the user's profile information will be sent back
        /// excluding the password.
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        public UserProfile AttemptUserLogin(LoginCredentials loginCredentials)
        {
            try
            {
                // Ensure that we don't send any invalid data to the server.
                if (Object.ReferenceEquals(loginCredentials, null) || String.IsNullOrWhiteSpace(loginCredentials.Password) || String.IsNullOrWhiteSpace(loginCredentials.UserName))
                {
                    return null;
                }

                string jsonObject = JsonConvert.SerializeObject(loginCredentials, SerializerSettings);
                string result = SendJsonRequest("ValidateLoginCredentials", WebServiceMethodTypeEnum.User, jsonObject);

                return JsonConvert.DeserializeObject<UserProfile>(result, SerializerSettings);
            }
            catch(Exception ex)
            {
                throw new Exception("Error sending login credentials.", ex);
            }
        }

        public AuthorizationTypeEnum RequestClientAuthorization(string ipAddress, int port, string username, string authorizationToken)
        {
            try
            {
                UserValidationPacket packet = new UserValidationPacket
                {
                    ServerIPAddress = ipAddress,
                    ServerPort = port,
                    Username = username,
                    ActiveAuthorizationToken = authorizationToken
                };

                string json = JsonConvert.SerializeObject(packet);
                string result = SendJsonRequest("AuthorizeUserForClientServer", WebServiceMethodTypeEnum.Server, json);
                ClientAuthorizationResponsePacket response = JsonConvert.DeserializeObject<ClientAuthorizationResponsePacket>(result);
                
                return response.AuthorizationResponse;
            }
            catch(Exception ex)
            {
                throw new Exception("Error requesting client authorization.", ex);
            }
        }

        public bool IsUserAuthorizedForServer(int port, string username)
        {
            try
            {
                UserValidationPacket packet = new UserValidationPacket
                {
                    ServerPort = port,
                    Username = username
                };

                string json = JsonConvert.SerializeObject(packet);
                string response = SendJsonRequest("CheckUserAuthorizedForClientServer", WebServiceMethodTypeEnum.Server, json);
                bool success = Convert.ToBoolean(response);

                return success;
            }
            catch(Exception ex)
            {
                throw new Exception("Error checking user authorization.", ex);
            }
        }

        /// <summary>
        /// Sends a user's profile to the master server.
        /// </summary>
        /// <param name="profile">The user's profile containing username, password, email, etc.</param>
        /// <param name="createNewProfile">If true, the account will attempt to be created. If false, it will be updated.</param>
        /// <returns></returns>
        public UserProfileResponseTypeEnum SendUserProfile(UserProfile profile, bool createNewProfile)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonObject = serializer.Serialize(profile);
                string result;
                
                if(createNewProfile)
                {
                    result = SendJsonRequest("CreateUserProfile", WebServiceMethodTypeEnum.User, jsonObject);
                }
                else
                {   
                    result = SendJsonRequest("UpdateUserProfile", WebServiceMethodTypeEnum.User, jsonObject);
                }

                return serializer.Deserialize<UserProfileResponseTypeEnum>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending user profile.", ex);
            }
        }

        /// <summary>
        /// Sends a request to master server to resend account activation email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool RequestActivationEmailResend(string email)
        {
            bool success = false;

            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string result = SendGetRequest("ResendActivationEmail", WebServiceMethodTypeEnum.User, "?email=" + email);

                success = serializer.Deserialize<bool>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error requesting activation email.", ex);
            }

            return success;
        }

        public bool IsMasterServerAlive()
        {
            bool isUp = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(MasterServerConfiguration.MasterServerURL);
            request.Method = "HEAD";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        isUp = true;
                    }
                }
            }
            catch
            {
                isUp = false;
            }

            return isUp;
        }


    }
}
