using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Network.Configuration
{
    public static class ClientServerConfiguration
    {
        private const string ClientServerAppID = "29273776-C76F-40E2-A6AD-7C7E53582485";

        public static string ApplicationID
        {
            get
            {
                return ClientServerAppID;
            }
        }
    }
}
