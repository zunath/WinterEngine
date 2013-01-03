using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DejaVu;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects
{
    [Serializable]
    [Table("ResourceTypes")]
    public class ResourceType
    {
        #region Fields

        readonly UndoRedo<int> _resourceTypeID = new UndoRedo<int>();
        readonly UndoRedo<string> _resourceName = new UndoRedo<string>();

        #endregion

        #region Properties

        [Key]
        public int ResourceTypeID
        {
            get { return _resourceTypeID.Value; }
            set { _resourceTypeID.Value = value; }
        }

        [MaxLength(64)]
        public string ResourceName
        {
            get { return _resourceName.Value; }
            set { _resourceName.Value = value; }
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
