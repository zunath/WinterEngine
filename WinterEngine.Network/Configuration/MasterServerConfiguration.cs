using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network.Configuration
{
    public class MasterServerConfiguration
    {
        // Values are stored in this class rather than app.config because
        // libraries do not pick up their own app.config files - they use the 
        // application's app.config file.
        //private const string _masterServerURL = "http://master.winterengine.com/";
        private const string _masterServerURL = "http://localhost:12901/";
        private const int _masterServerPort = 5122;

        /// <summary>
        /// Returns the IP address of the master server
        /// </summary>
        public static IPAddress MasterServerIPAddress
        {
            get
            {
                return Dns.GetHostAddresses(_masterServerURL)[0];
            }
        }

        /// <summary>
        /// Returns the URL of the master server.
        /// </summary>
        public static string MasterServerURL
        {
            get
            {
                return _masterServerURL;
            }
        }

        /// <summary>
        /// Returns the port of the master server
        /// </summary>
        public static int Port
        {
            get
            {
                return _masterServerPort;
            }
        }
    }
}
