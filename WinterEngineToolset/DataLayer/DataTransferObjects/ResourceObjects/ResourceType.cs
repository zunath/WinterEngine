using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects
{
    [Serializable]
    [Table("ResourceTypes")]
    public class ResourceType
    {
        #region Fields

        int _resourceTypeID;
        string _resourceName;

        #endregion

        #region Properties

        [Key]
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

        ResourceType(int resourceTypeID, string resourceName)
        {
            this.ResourceTypeID = resourceTypeID;
            this.ResourceName = resourceName;
        }

        #endregion
    }
}
