using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("ResourceCategories")]
    public class ResourceCategory
    {
        #region Fields

        int _resourceCategoryID;
        int _resourceTypeID;
        string _resourceName;
        bool _isSystemCategory;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the resource category ID
        /// </summary>
        [Key]
        public int ResourceCategoryID
        {
            get { return _resourceCategoryID; }
            set { _resourceCategoryID = value; }
        }

        /// <summary>
        /// Gets or sets the resource type ID (ResourceTypeEnum)
        /// </summary>
        public int ResourceTypeID
        {
            get { return _resourceTypeID; }
            set { _resourceTypeID = value; }
        }

        /// <summary>
        /// Gets or sets the name of the resource category.
        /// </summary>
        [MaxLength(64)]
        public string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

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
