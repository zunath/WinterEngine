using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.AttributeObjects
{
    [Serializable]
    [Table("ItemProperties")]
    public class ItemProperty
    {
        #region Fields

        private int _itemPropertyID;
        private string _itemPropertyName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the item property's ID
        /// </summary>
        [Key]
        public int ItemPropertyID
        {
            get { return _itemPropertyID; }
            set { _itemPropertyID = value; }
        }

        /// <summary>
        /// Gets or sets the name of an item property.
        /// </summary>
        public string ItemPropertyName
        {
            get { return _itemPropertyName; }
            set { _itemPropertyName = value; }
        }

        #endregion

    }
}
