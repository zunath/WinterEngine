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
        #region Fields

        private Map _map;

        #endregion

        #region Properties

        public Map TileMap
        {
            get { return _map; }
            set { _map = value; }
        }

        // EVENT SCRIPTS
        public int? OnAreaEnterEventScriptID { get; set; }
        public int? OnAreaExitEventScriptID { get; set; }
        public int? OnAreaHeartbeatEventScriptID { get; set; }
        public int? OnAreaUserDefinedEventScriptID { get; set; }

        
        [ForeignKey("OnAreaEnterEventScriptID")]
        public virtual Script OnAreaEnterEventScript { get; set; }
        [ForeignKey("OnAreaExitEventScriptID")]
        public virtual Script OnAreaExitEventScript { get; set; }
        [ForeignKey("OnAreaHeartbeatEventScriptID")]
        public virtual Script OnAreaHeartbeatEventScript { get; set; }
        [ForeignKey("OnAreaUserDefinedEventScriptID")]
        public virtual Script OnAreaUserDefinedEventScript { get; set; }
        
        #endregion

        #region Constructors

        public Area()
        {
        }

        #endregion
    }
}
