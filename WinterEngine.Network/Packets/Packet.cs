using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using ProtoBuf;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    [ProtoContract]
    [ProtoInclude(100, typeof(RequestPacket))]
    [ProtoInclude(101, typeof(ContentPackageListPacket))]
    public class Packet
    {
        [ProtoMember(1)]
        public virtual PacketTypeEnum PacketType { get; set; }

        /// <summary>
        /// Gets or sets the sender's NetConnection
        /// </summary>
        public NetConnection SenderConnection { get; set; }

        public Packet()
        {
        }

    }
}
