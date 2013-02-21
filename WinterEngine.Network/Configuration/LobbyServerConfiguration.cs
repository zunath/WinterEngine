using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network.Configuration
{
    public static class LobbyServerConfiguration
    {
        private const string LobbyServerAppID = "20F7B215-5E4D-4B62-A2DF-543E436BB232";
        private const int ServerTimeoutMin = 1;

        /// <summary>
        /// Returns the application identifier for lobby server connections
        /// </summary>
        public static string ApplicationID
        {
            get
            {
                return LobbyServerAppID;
            }
        }

        /// <summary>
        /// Returns the number of minutes it takes before a server times out from the master server.
        /// </summary>
        public static int ServerTimeoutMinutes
        {
            get
            {
                return ServerTimeoutMin;
            }
        }

    }
}
