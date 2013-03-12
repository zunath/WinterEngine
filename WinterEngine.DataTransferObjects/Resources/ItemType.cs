using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Serializable]
    [Table("ItemTypes")]
    public class ItemType : GameResourceBase
    {
        #region Fields

        private bool _hasIcon;
        private int _iconWidth;
        private int _iconHeight;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether an item type has a icon.
        /// </summary>
        public bool HasIcon
        {
            get { return _hasIcon; }
            set { _hasIcon = value; }
        }


        /// <summary>
        /// Gets or sets the item type's icon width
        /// </summary>
        public int IconWidth
        {
            get { return _iconWidth; }
            set { _iconWidth = value; }
        }

        /// <summary>
        /// Gets or sets the item type's icon height
        /// </summary>
        public int IconHeight
        {
            get { return _iconHeight; }
            set { _iconHeight = value; }
        }

        #endregion

    }
}
