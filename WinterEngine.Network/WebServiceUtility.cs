using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WinterEngine.Network.Entities;

namespace WinterEngine.Network
{
    public class WebServiceUtility
    {
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
            string jsonResult = JsonConvert.SerializeObject(details);

            JObject jobj = new JObject(details);
            

            WebRequest request = WebRequest.Create("http://master.winterengine.com/api/utility/UpdateServerDetails");
            
            

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
