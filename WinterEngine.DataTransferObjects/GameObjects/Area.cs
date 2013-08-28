using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.BusinessObjects;


namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Areas")]
    public class Area : GameObjectBase
    {
        #region Properties

        //public int? TileMapID { get; set; }

        //[ForeignKey("TileMapID")]
        public virtual Map TileMap { get; set; }


        // EVENT SCRIPTS
        public int? OnEnterEventScriptID { get; set; }
        public int? OnExitEventScriptID { get; set; }
        public int? OnHeartbeatEventScriptID { get; set; }
        public int? OnUserDefinedEventScriptID { get; set; }

        
        [ForeignKey("OnEnterEventScriptID")]
        public virtual Script OnAreaEnterEventScript { get; set; }
        [ForeignKey("OnExitEventScriptID")]
        public virtual Script OnAreaExitEventScript { get; set; }
        [ForeignKey("OnHeartbeatEventScriptID")]
        public virtual Script OnAreaHeartbeatEventScript { get; set; }
        [ForeignKey("OnUserDefinedEventScriptID")]
        public virtual Script OnAreaUserDefinedEventScript { get; set; }
        
        #endregion

        #region Constructors

        public Area()
        {
            TileMap = new Map();
        }

        #endregion
    }
}
