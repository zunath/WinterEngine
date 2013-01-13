using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Library.DataAccess.DataTransferObjects.ResourceObjects
{
    [Serializable]
    [Table("ResourceCategories")]
    public class ResourceCategory
    {
        #region Fields

        int _resourceCategoryID;
        int _resourceTypeID;
        string _resourceName;

        #endregion

        #region Properties

        [Key]
        public int ResourceCategoryID
        {
            get { return _resourceCategoryID; }
            set { _resourceCategoryID = value; }
        }

        public int ResourceTypeID
        {
            get { return _resourceTypeID; }
            set { _resourceTypeID = value; }
        }

        [MaxLength(64)]
        
        public string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
