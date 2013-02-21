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
        private const string MasterServerURL = "localhost";
        private const int MasterServerPort = 5121;

        /// <summary>
        /// Returns the IP address of the master server
        /// </summary>
        public static IPAddress MasterServerIPAddress
        {
            get
            {
                return Dns.GetHostAddresses(MasterServerURL)[0];
            }
        }

        /// <summary>
        /// Returns the port of the master server
        /// </summary>
        public static int Port
        {
            get
            {
                return MasterServerPort;
            }
        }
    }
}
