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

        public string SendJsonRequest(string methodName, WebServiceMethodTypeEnum methodType, object jsonObject)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(MasterServerConfiguration.MasterServerURL + "api/" + EnumerationHelper.GetEnumerationDescription(methodType) + "/" + methodName);
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
                string publicIP = "";

                WebRequest request = WebRequest.Create("http://master.winterengine.com/api/utility/GetServerIPAddress");
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        publicIP = stream.ReadToEnd();
                    }
                }
                publicIP = JsonConvert.DeserializeObject(publicIP) as string;
                
                return publicIP;
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
                return SendJsonRequest("UpdateServerDetails", WebServiceMethodTypeEnum.Utility, jsonObject);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending server details.", ex);
            }
        }
    }
}
