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
    public class ItemType : GameResource
    {
        #region Fields

        private bool _has2DIcon;
        private bool _has3DModel;
        private int _iconWidth;
        private int _iconHeight;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether an item type has a 2D icon.
        /// </summary>
        public bool Has2DIcon
        {
            get { return _has2DIcon; }
            set { _has2DIcon = value; }
        }

        /// <summary>
        /// Gets or sets whether an item type has a 3D model.
        /// </summary>
        public bool Has3DModel
        {
            get { return _has3DModel; }
            set { _has3DModel = value; }
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
