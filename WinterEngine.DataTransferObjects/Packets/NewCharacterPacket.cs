using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract()]
    public class NewCharacterPacket : PacketBase
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }
        [ProtoMember(3)]
        public int Age { get; set; }
        [ProtoMember(4)]
        public int RaceID { get; set; }
        [ProtoMember(5)]
        public int PortraitID { get; set; }
        [ProtoMember(6)]
        public int GenderID { get; set; }
        [ProtoMember(7)]
        public int CharacterClassID { get; set; }
        [ProtoMember(8)]
        public int AbilityChoices { get; set; }
        [ProtoMember(9)]
        public int SkillPoints { get; set; }
        [ProtoMember(10)]
        public List<Ability> SelectedAbilities { get; set; }
        [ProtoMember(11)]
        public int Strength { get; set; }
        [ProtoMember(12)]
        public int Dexterity { get; set; }
        [ProtoMember(13)]
        public int Constitution { get; set; }
        [ProtoMember(14)]
        public int Intelligence { get; set; }
        [ProtoMember(15)]
        public int Wisdom { get; set; }

        public NewCharacterPacket()
        {
        }
    }
}
