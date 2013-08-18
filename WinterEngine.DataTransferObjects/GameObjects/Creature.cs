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
        [XmlIgnore]
        public bool IsInvulnerable { get; set; }

        // EQUIPPED ITEMS

        public Item MainHandItem { get; set; }
        public Item OffHandItem { get; set; }
        public Item HeadItem { get; set; }
        public Item BodyItem { get; set; }
        public Item BackItem { get; set; }
        public Item HandsItem { get; set; }
        public Item WaistItem { get; set; }
        public Item LegsItem { get; set; }
        public Item FeetItem { get; set; }
        public Item EarLeftItem { get; set; }
        public Item EarRightItem { get; set; }
        public Item RingLeftItem { get; set; }
        public Item RingRightItem { get; set; }
        public Item NeckItem { get; set; }

        // EVENT SCRIPTS
        public int? OnSpawnEventScriptID { get; set; }
        public int? OnDamagedEventScriptID { get; set; }
        public int? OnDeathEventScriptID { get; set; }
        public int? OnConversationEventScriptID { get; set; }
        public int? OnHeartbeatEventScriptID { get; set; }

        [ForeignKey("OnSpawnEventScriptID")]
        public virtual Script OnSpawnEventScript { get; set; }
        [ForeignKey("OnDamagedEventScriptID")]
        public virtual Script OnDamagedEventScript { get; set; }
        [ForeignKey("OnDeathEventScriptID")]
        public virtual Script OnDeathEventScript { get; set; }
        [ForeignKey("OnConversationEventScriptID")]
        public virtual Script OnConversationEventScript { get; set; }
        [ForeignKey("OnHeartbeatEventScriptID")]
        public virtual Script OnHeartbeatEventScript { get; set; }

        #endregion

        #region Constructors

        public Creature()
        {
        }

        #endregion
    }
}
