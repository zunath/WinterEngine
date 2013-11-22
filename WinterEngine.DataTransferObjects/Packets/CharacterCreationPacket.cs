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
        public List<Race> RaceList { get; set; }
        public List<Gender> GenderList { get; set; }
    }
}
