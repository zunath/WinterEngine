using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Placeables")]
    public sealed class Placeable : GameObjectBase
    {
        #region Fields

        private List<Item> _inventoryItems;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether or not a placeable is useable in-game.
        /// </summary>
        public bool IsUseable { get; set; }

        /// <summary>
        /// Gets or sets whether or not a placeable can contain items in its inventory.
        /// </summary>
        public bool HasInventory { get; set; }

        /// <summary>
        /// Gets or sets the list of items contained in a placeable's inventory.
        /// </summary>
        public List<Item> InventoryItems
        {
            get { return _inventoryItems; }
            set { _inventoryItems = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
