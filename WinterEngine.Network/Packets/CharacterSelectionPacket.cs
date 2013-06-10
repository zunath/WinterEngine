using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.Network.Packets
{
    [ProtoContract]
    public class CharacterSelectionPacket : PacketBase
    {
        [ProtoMember(1)]
        public List<PlayerCharacter> CharacterList { get; set; }
        [ProtoMember(2)]
        public string ServerName { get; set; }
        [ProtoMember(3)]
        public string ServerAnnouncement { get; set; }
        [ProtoMember(4)]
        public bool CanDeleteCharacters { get; set; }
    }
}
