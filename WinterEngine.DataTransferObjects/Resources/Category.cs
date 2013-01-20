using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Serializable]
    [Table("Categories")]
    public class Category : GameResource
    {
        #region Fields

        bool _isSystemCategory;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether or not this is a system category.
        /// System categories are treated differently from regular categories.
        /// They cannot be modified or deleted by the user.
        /// </summary>
        public bool IsSystemCategory
        {
            get { return _isSystemCategory; }
            set { _isSystemCategory = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
