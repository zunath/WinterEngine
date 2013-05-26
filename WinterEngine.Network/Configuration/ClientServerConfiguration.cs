using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Network.Configuration
{
    public static class ClientServerConfiguration
    {
        private const string ClientServerAppID = "29273776-C76F-40E2-A6AD-7C7E53582485";
        private const int ClientServerPort = 5121;

        /// <summary>
        /// Returns the default port number used by the game server.
        /// This may be overridden by the end-user.
        /// </summary>
        public static int DefaultPort
        {
            get { return ClientServerPort; }
        }

        /// <summary>
        /// Returns the unique application ID used by the game server
        /// </summary>
        public static string ApplicationID
        {
            get
            {
                return ClientServerAppID;
            }
        }
    }
}
