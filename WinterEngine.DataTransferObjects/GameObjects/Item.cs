using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects;


namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Items")]
    public class Item : GameObjectBase
    {
        #region Fields

        #endregion

        #region Properties

        public ItemType Type { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public bool IsUndroppable { get; set; }
        public List<ItemProperty> ItemProperties { get; set; }
        public bool IsPlot { get; set; }
        public bool IsStolen { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        // EVENT SCRIPTS

        public int? OnSpawnEventScriptID { get; set; }

        [ForeignKey("OnSpawnEventScriptID")]
        public virtual Script OnSpawnEventScript { get; set; }

        #endregion

        #region Constructors

        public Item()
        {
        }

        public Item(bool instantiateLists)
        {
            if (instantiateLists)
            {
                LocalVariables = new List<LocalVariable>();
                ItemProperties = new List<ItemProperty>();
            }
            else
            {
            }
        }

        #endregion
    }
}
