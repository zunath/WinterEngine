using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class ClientDisconnectPacket: PacketBase
    {
        [ProtoMember(1)]
        public string Reason { get; set; }

        public ClientDisconnectPacket()
        {
            this.Reason = "";
        }

        public ClientDisconnectPacket(string reason)
        {
            this.Reason = reason;
        }
    }
}
