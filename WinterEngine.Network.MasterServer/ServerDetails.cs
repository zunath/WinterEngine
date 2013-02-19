using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Lidgren.Network;
using WinterEngine.Network.Entities;

namespace WinterEngine.Network.MasterServer
{
    class ServerDetails
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public float Ping { get; set; }
        public ConnectionAddress Connection { get; set; }
        public DateTime LastPacketReceived { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Override to display the server details in list boxes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
