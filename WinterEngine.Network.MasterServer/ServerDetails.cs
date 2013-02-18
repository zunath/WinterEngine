using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network.MasterServer
{
    class ServerDetails
    {
        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public IPAddress IPAddress { get; set; }
        public int Port { get; set; }
        public float Ping { get; set; }

        #endregion
    }
}
