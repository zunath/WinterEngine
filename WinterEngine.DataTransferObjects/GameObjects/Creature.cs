﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.DataTransferObjects
{
    [ProtoContract]
    [Serializable]
    [Table("Creatures")]
    public class Creature : GameObjectBase
    {
        #region Properties

        [ProtoMember(1)]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        [MaxLength(32)]
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

        public int? MainHandItemID { get; set; }
        public int? OffHandItemID { get; set; }
        public int? HeadItemID { get; set; }
        public int? BodyItemID { get; set; }
        public int? BackItemID { get; set; }
        public int? HandsItemID { get; set; }
        public int? WaistItemID { get; set; }
        public int? LegsItemID { get; set; }
        public int? FeetItemID { get; set; }
        public int? EarLeftItemID { get; set; }
        public int? EarRightItemID { get; set; }
        public int? RingLeftItemID { get; set; }
        public int? RingRightItemID { get; set; }
        public int? NeckItemID { get; set; }

        [ForeignKey("MainHandItemID")]
        public virtual Item MainHandItem { get; set; }
        [ForeignKey("OffHandItemID")]
        public virtual Item OffHandItem { get; set; }
        [ForeignKey("HeadItemID")]
        public virtual Item HeadItem { get; set; }
        [ForeignKey("BodyItemID")]
        public virtual Item BodyItem { get; set; }
        [ForeignKey("BackItemID")]
        public virtual Item BackItem { get; set; }
        [ForeignKey("HandsItemID")]
        public virtual Item HandsItem { get; set; }
        [ForeignKey("WaistItemID")]
        public virtual Item WaistItem { get; set; }
        [ForeignKey("LegsItemID")]
        public virtual Item LegsItem { get; set; }
        [ForeignKey("FeetItemID")]
        public virtual Item FeetItem { get; set; }
        [ForeignKey("EarLeftItemID")]
        public virtual Item EarLeftItem { get; set; }
        [ForeignKey("EarRightItemID")]
        public virtual Item EarRightItem { get; set; }
        [ForeignKey("RingLeftItemID")]
        public virtual Item RingLeftItem { get; set; }
        [ForeignKey("RingRightItemID")]
        public virtual Item RingRightItem { get; set; }
        [ForeignKey("NeckItemID")]
        public virtual Item NeckItem { get; set; }

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

        public Creature(bool instantiateLists)
        {
            if (instantiateLists)
            {
                LocalVariables = new List<LocalVariable>();
            }
            else
            {
            }
        }

        #endregion
    }
}
