using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class BanUserPacket: PacketBase
    {
        [ProtoMember(1)]
        public string BanReason { get; set; }

        public BanUserPacket()
        {
            this.BanReason = "";
        }
    }
}
