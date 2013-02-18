using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network
{
    public static class MasterServerConfiguration
    {
        // Values are stored in this class rather than app.config because
        // libraries do not pick up their own app.config files - they use the 
        // application's app.config file.
        private const string MasterServerURL = "localhost";
        private const int MasterServerPort = 5121;
        private const string MasterServerAppID = "20F7B215-5E4D-4B62-A2DF-543E436BB232";

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

        /// <summary>
        /// Returns the application identifier for master server connections
        /// </summary>
        public static string MasterServerApplicationIdentifier
        {
            get
            {
                return MasterServerAppID;
            }
        }
    }
}
