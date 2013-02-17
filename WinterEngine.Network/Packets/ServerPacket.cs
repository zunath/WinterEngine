using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.Network.Packets
{
    class ServerPacket : PacketBase
    {
        #region Properties

        public string Name { get; set; }
        public IPEndPoint IPAddress { get; set; }
        public string Description { get; set; }

        #endregion
    }
}
