using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("GameModule")]
    public class GameModule : GameObjectBase
    {
        #region Properties

        // Note: There should only ever be one row in the GameModule table.

        /// <summary>
        /// Gets or sets the module ID.
        /// </summary>
        [Key]
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the module's max level.
        /// </summary>
        public int MaxLevel { get; set; }

        [NotMapped]
        public string FileName { get; set; }


        public int OnPlayerEnterEventScriptID { get; set; }
        public int OnPlayerLeavingEventScriptID { get; set; }
        public int OnPlayerLeftEventScriptID { get; set; }
        public int OnHeartbeatEventScriptID { get; set; }
        public int OnModuleLoadEventScriptID { get; set; }
        public int OnPlayerDeathEventScriptID { get; set; }
        public int OnPlayerDyingEventScriptID { get; set; }
        public int OnPlayerRespawnEventScriptID { get; set; }

        [JsonIgnore]
        [ForeignKey("OnPlayerEnterEventScriptID")]
        public virtual Script OnPlayerEnterEventScript { get; set; }
        [JsonIgnore]
        [ForeignKey("OnPlayerLeavingEventScriptID")]
        public virtual Script OnPlayerLeavingEventScript { get; set; }
        [JsonIgnore]
        [ForeignKey("OnPlayerLeftEventScriptID")]
        public virtual Script OnPlayerLeftEventScript { get; set; }
        [JsonIgnore]
        [ForeignKey("OnHeartbeatEventScriptID")]
        public virtual Script OnHeartbeatEventScript { get; set; }
        [JsonIgnore]
        [ForeignKey("OnModuleLoadEventScriptID")]
        public virtual Script OnModuleLoadEventScript { get; set; }
        [JsonIgnore]
        [ForeignKey("OnPlayerDeathEventScriptID")]
        public virtual Script OnPlayerDeathEventScript { get; set; }
        [JsonIgnore]
        [ForeignKey("OnPlayerDyingEventScriptID")]
        public virtual Script OnPlayerDyingEventScript { get; set; }
        [JsonIgnore]
        [ForeignKey("OnPlayerRespawnEventScriptID")]
        public virtual Script OnPlayerRespawnEventScript { get; set; }

        #endregion

        #region Constructors

        public GameModule()
        {
        }

        public GameModule(bool instantiateLists = false)
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
