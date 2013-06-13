using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.DataTransferObjects
{
    [ProtoContract]
    [Serializable]
    [Table("Creatures")]
    public class Creature : GameObjectBase
    {
        #region Properties

        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }
        [ProtoMember(3)]
        public Race Race { get; set; }
        [ProtoMember(4)]
        public string Description { get; set; }
        [ProtoMember(5)]
        public int Strength { get; set; }
        [ProtoMember(6)]
        public int Dexterity { get; set; }
        [ProtoMember(7)]
        public int Wisdom { get; set; }
        [ProtoMember(8)]
        public int Constitution { get; set; }
        [ProtoMember(9)]
        public int Intelligence { get; set; }
        [ProtoMember(10)]
        [NotMapped]
        public int HitPoints { get; set; }
        [ProtoMember(11)]
        [NotMapped]
        public int Mana { get; set; }
        [ProtoMember(12)]
        public int MaxHitPoints { get; set; }
        [ProtoMember(13)]
        public int MaxMana { get; set; }
        [ProtoMember(14)]
        public int Level { get; set; }

        [ProtoMember(15)] // Protobuf / Packet serialization
        [XmlIgnore] // PlayerCharacter XML serialization
        [NotMapped] // Entity Framework
        public Location Location { get; set; }

        [ProtoMember(16)]
        [NotMapped]
        public List<LocalVariable> LocalVariables { get; set; }

        #endregion

        #region Constructors

        public Creature()
        {
            LocalVariables = new List<LocalVariable>();
        }

        #endregion
    }
}
