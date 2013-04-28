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

        private string _description;
        private bool _isUseable;
        private bool _hasInventory;
        private List<Item> _inventoryItems;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the description of a placeable.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets whether or not a placeable is useable in-game.
        /// </summary>
        public bool IsUseable
        {
            get { return _isUseable; }
            set { _isUseable = value; }
        }

        /// <summary>
        /// Gets or sets whether or not a placeable can contain items in its inventory.
        /// </summary>
        public bool HasInventory
        {
            get { return _hasInventory; }
            set { _hasInventory = value; }
        }

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
