using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class StreamingFileDetailsPacket : PacketBase
    {
        [ProtoMember(1)]
        public long FileSize { get; set; }
    }
}
