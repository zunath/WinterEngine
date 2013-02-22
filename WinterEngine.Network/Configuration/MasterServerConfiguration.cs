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
        private const string _MasterServerURL = "localhost";
        private const int _MasterServerPort = 5122;

        /// <summary>
        /// Returns the IP address of the master server
        /// </summary>
        public static IPAddress MasterServerIPAddress
        {
            get
            {
                return Dns.GetHostAddresses(_MasterServerURL)[0];
            }
        }

        /// <summary>
        /// Returns the URL of the master server.
        /// </summary>
        public static string MasterServerURL
        {
            get
            {
                return _MasterServerURL;
            }
        }

        /// <summary>
        /// Returns the port of the master server
        /// </summary>
        public static int Port
        {
            get
            {
                return _MasterServerPort;
            }
        }
    }
}
