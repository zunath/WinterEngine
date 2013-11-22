using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class CharacterCreationPacket : PacketBase
    {
        [ProtoMember(1)]
        public List<Race> RaceList { get; set; }
        [ProtoMember(2)]
        public List<Gender> GenderList { get; set; }
    }
}
