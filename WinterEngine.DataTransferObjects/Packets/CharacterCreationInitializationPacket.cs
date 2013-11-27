using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class CharacterCreationInitializationPacket : PacketBase
    {
        [ProtoMember(1)]
        public List<Race> RaceList { get; set; }
        [ProtoMember(2)]
        public List<Gender> GenderList { get; set; }
        [ProtoMember(3)]
        public List<CharacterClass> ClassList { get; set; }
        [ProtoMember(4)]
        public List<Ability> AbilityList { get; set; }
        [ProtoMember(5)]
        public List<Skill> SkillList { get; set; }

        public CharacterCreationInitializationPacket()
        {
            this.RaceList = new List<Race>();
            this.GenderList = new List<Gender>();
            this.ClassList = new List<CharacterClass>();
            this.AbilityList = new List<Ability>();
        }
    }
}
