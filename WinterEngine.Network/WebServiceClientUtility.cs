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
using WinterEngine.Network.Enums;

namespace WinterEngine.Network
{
    public class WebServiceClientUtility
    {

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
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return ReplaceJsonArraySpecialCharacters(reader.ReadToEnd());
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
        public string SendServerDetails(WinterServer details)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonObject = serializer.Serialize(details);
                return SendJsonRequest("UpdateServerDetails", WebServiceMethodTypeEnum.Server, jsonObject);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending server details.", ex);
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

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonObject = serializer.Serialize(loginCredentials);
                string result = SendJsonRequest("ValidateLoginCredentials", WebServiceMethodTypeEnum.User, jsonObject);
                
                return serializer.Deserialize<UserProfile>(result);
            }
            catch(Exception ex)
            {
                throw new Exception("Error sending login credentials.", ex);
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

    }
}
