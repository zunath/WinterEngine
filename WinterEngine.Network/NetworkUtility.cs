using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network
{
    public class NetworkUtility
    {
        /// <summary>
        /// Queries www.whatismyip.com and returns the user's external IP address.
        /// If the query fails, the default value will be displayed ("Unknown")
        /// </summary>
        /// <returns></returns>
        public string GetExternalIPAddress()
        {
            // Reference: http://www.8b1b.com/showthread.php?64-How-to-get-my-own-IP-address-in-C
            try
            {
                String publicIP = "";

                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        publicIP = stream.ReadToEnd();
                    }
                }

                //Search for the ip in the html
                int first = publicIP.IndexOf("Address: ") + 9;
                int last = publicIP.LastIndexOf("</body>");
                publicIP = publicIP.Substring(first, last - first);

                return publicIP;
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
