using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.Network.Packets
{
    [ProtoContract]
    public class UsernamePacket: PacketBase
    {
        [ProtoMember(1)]
        public string Username { get; set; }
    }
}
