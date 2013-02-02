using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.Graphics;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    [Serializable]
    [Table("Placeables")]
    public sealed class Placeable : GameObject, IEntity
    {
        #region Fields

        private string _description;
        private bool _isUseable;
        private bool _hasInventory;
        private List<Item> _inventoryItems;
        private SpriteSheet _portraitGraphic;
        private SpriteSheet _modelGraphic;

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

        /// <summary>
        /// Gets or sets the 3D model of a placeable.
        /// </summary>
        public SpriteSheet ModelGraphic
        {
            get { return _modelGraphic; }
            set { _modelGraphic = value; }
        }

        /// <summary>
        /// Gets or sets the portrait of a placeable.
        /// </summary>
        public SpriteSheet PortraitGraphic
        {
            get { return _portraitGraphic; }
            set { _portraitGraphic = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
