using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    public abstract class Packet
    {
        public abstract PacketTypeEnum PacketType {get;}
    }
}
