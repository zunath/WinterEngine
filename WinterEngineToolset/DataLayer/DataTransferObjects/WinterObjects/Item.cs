using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects
{
    [Serializable]
    [Table("Items")]
    public sealed class Item : WinterObject
    {
        #region Fields

        private int _type;
        private string _description;
        private int _price;
        private int _weight;
        private bool _isUndroppable;
        private bool _isPlot;
        private bool _isStolen;
        private List<ItemProperty> _itemProperties;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the item type of a particular item.
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Gets or sets the viewable description for an item.
        /// </summary>
        [MaxLength(4000)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the price of an item.
        /// </summary>
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        /// <summary>
        /// Gets or sets the weight of an item.
        /// </summary>
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        /// <summary>
        /// Gets or sets whether or not an item can be dropped by a player.
        /// </summary>
        public bool IsUndroppable
        {
            get { return _isUndroppable; }
            set { _isUndroppable = value; }
        }

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
        public bool IsPlot
        {
            get { return _isPlot; }
            set { _isPlot = value; }
        }

        /// <summary>
        /// Gets or sets whether or not an item is stolen.
        /// </summary>
        public bool IsStolen
        {
            get { return _isStolen; }
            set { _isStolen = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
