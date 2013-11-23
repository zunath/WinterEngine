using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class BootUserPacket : PacketBase
    {
        [ProtoMember(1)]
        public string BootReason { get; set; }

        public BootUserPacket()
        {
            this.BootReason = "";
        }

    }
}
