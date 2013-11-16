using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class StreamingFilePacket : PacketBase
    {
        [ProtoMember(1)]
        public string FileName { get; set; }
        [ProtoMember(2)]
        public byte[] FileBytes { get; set; }
        [ProtoMember(3)]
        public bool IsEndOfFile { get; set; }
    }
}
