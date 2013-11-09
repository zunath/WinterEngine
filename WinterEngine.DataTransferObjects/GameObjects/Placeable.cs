using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Placeables")]
    public class Placeable : GameObjectBase
    {
        #region Fields

        #endregion

        #region Properties

        public bool IsUseable { get; set; }
        public bool HasInventory { get; set; }
        public List<Item> InventoryItems { get; set; }


        // EVENT SCRIPTS
        public int OnSpawnEventScriptID { get; set; }
        public int OnDamagedEventScriptID { get; set; }
        public int OnDeathEventScriptID { get; set; }
        public int OnHeartbeatEventScriptID { get; set; }
        public int OnOpenEventScriptID { get; set; }
        public int OnCloseEventScriptID { get; set; }
        public int OnUsedEventScriptID { get; set; }

        [ForeignKey("OnSpawnEventScriptID")]
        [JsonIgnore]
        public virtual Script OnSpawnEventScript { get; set; }
        [ForeignKey("OnDamagedEventScriptID")]
        [JsonIgnore]
        public virtual Script OnDamagedEventScript { get; set; }
        [ForeignKey("OnDeathEventScriptID")]
        [JsonIgnore]
        public virtual Script OnDeathEventScript { get; set; }
        [ForeignKey("OnHeartbeatEventScriptID")]
        [JsonIgnore]
        public virtual Script OnHeartbeatEventScript { get; set; }
        [ForeignKey("OnOpenEventScriptID")]
        [JsonIgnore]
        public virtual Script OnOpenEventScript { get; set; }
        [ForeignKey("OnCloseEventScriptID")]
        [JsonIgnore]
        public virtual Script OnCloseEventScript { get; set; }
        [ForeignKey("OnUsedEventScriptID")]
        [JsonIgnore]
        public virtual Script OnUsedEventScript { get; set; }


        #endregion

        #region Constructors

        public Placeable()
        {
        }

        public Placeable(bool instantiateLists)
        {
            if (instantiateLists)
            {
                LocalVariables = new List<LocalVariable>();
                InventoryItems = new List<Item>();
            }
            else
            {
            }
        }

        #endregion
    }
}
