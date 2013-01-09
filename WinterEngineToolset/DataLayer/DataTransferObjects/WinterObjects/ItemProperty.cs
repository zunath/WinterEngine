using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects
{
    [Serializable]
    [Table("ItemProperties")]
    public class ItemProperty
    {
        #region Fields

        private int _itemPropertyID;

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

        #endregion

    }
}
