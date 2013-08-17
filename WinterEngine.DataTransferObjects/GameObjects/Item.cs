using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects;


namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Items")]
    public sealed class Item : GameObjectBase
    {
        #region Fields

        private List<ItemProperty> _itemProperties;

        #endregion

        #region Properties

        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the item type of a particular item.
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Gets or sets the price of an item.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the weight of an item.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets whether or not an item can be dropped by a player.
        /// </summary>
        public bool IsUndroppable { get; set; }

        /// <summary>
        /// Gets or sets the list of item properties for an item.
        /// </summary>
        public List<ItemProperty> ItemProperties
        {
            get { return _itemProperties; }
            set { _itemProperties = value; }
        }

        /// <summary>
        /// Gets or sets whether or not an item is plot.
        /// </summary>
        public bool IsPlot { get; set; }

        /// <summary>
        /// Gets or sets whether or not an item is stolen.
        /// </summary>
        public bool IsStolen { get; set; }

        /// <summary>
        /// Gets or sets the width of an inventory item.
        /// </summary>
        public int TileWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of an inventory item.
        /// </summary>
        public int TileHeight { get; set; }

        #endregion

    }
}
