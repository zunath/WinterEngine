using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network.Configuration
{
    // Values are stored in this class rather than app.config because
    // libraries do not pick up their own app.config files - they use the 
    // application's app.config file.
    public class MasterServerConfiguration
    {
        private const string _masterServerURL = "https://www.winterengine.com/";
        //private const string _masterServerURL = "http://localhost:12901/";            // For debugging
        private const int _syncDelaySeconds = 3; // DEFAULT: 30

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
        /// Returns the number of seconds before a master server client will attempt to sync
        /// to the master server.
        /// </summary>
        public static int SyncDelaySeconds
        {
            get
            {
                return _syncDelaySeconds;
            }
        }
    }
}
