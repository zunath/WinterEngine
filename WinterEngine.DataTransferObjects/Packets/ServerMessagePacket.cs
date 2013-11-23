using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class ServerMessagePacket : PacketBase
    {
        [ProtoMember(1)]
        public string ServerMessage { get; set; }

        public ServerMessagePacket(string message)
        {
            this.ServerMessage = message;
        }
    }
}
