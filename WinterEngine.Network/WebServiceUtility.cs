using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WinterEngine.Library.Utility;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network
{
    public class WebServiceUtility
    {

        /// <summary>
        /// Builds the URL to the master server.
        /// </summary>
        /// <param name="methodType">The method type to use.</param>
        /// <returns></returns>
        private string BuildURLString(WebServiceMethodTypeEnum methodType)
        {
            return MasterServerConfiguration.MasterServerURL + "api/" + EnumerationHelper.GetEnumerationDescription(methodType) + "/";
        }

        /// <summary>
        /// Sends a GET request to the master server and returns the Json result.
        /// </summary>
        /// <param name="methodName">Name of the method on the server to use.</param>
        /// <param name="methodType">The type of method</param>
        /// <returns></returns>
        private string SendGetRequest(string methodName, WebServiceMethodTypeEnum methodType)
        {
            WebRequest request = WebRequest.Create(BuildURLString(methodType) + methodName);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
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
                    return reader.ReadToEnd();
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
                string jsonResult = SendGetRequest("GetServerIPAddress", WebServiceMethodTypeEnum.Server);
                return JsonConvert.DeserializeObject(jsonResult) as string;
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
        public string SendServerDetails(ServerDetails details)
        {
            try
            {
                string jsonObject = JsonConvert.SerializeObject(details);
                return SendJsonRequest("UpdateServerDetails", WebServiceMethodTypeEnum.Server, jsonObject);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending server details.", ex);
            }
        }

        /// <summary>
        /// Sends a user's master server login credentials to the master server.
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        public bool AttemptUserLogin(LoginCredentials loginCredentials)
        {
            try
            {
                string jsonObject = JsonConvert.SerializeObject(loginCredentials);
                string result = SendJsonRequest("ValidateLoginCredentials", WebServiceMethodTypeEnum.User, jsonObject);
                result = JsonConvert.DeserializeObject(result) as string;
                return Convert.ToBoolean(result);
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
        /// <returns></returns>
        public bool SendUserProfile(UserProfile profile)
        {
            try
            {
                string jsonObject = JsonConvert.SerializeObject(profile);
                string result = SendJsonRequest("UpdateUserProfile", WebServiceMethodTypeEnum.User, jsonObject);
                result = JsonConvert.DeserializeObject(result) as string;
                return Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending user profile.", ex);
            }
        }

    }
}
