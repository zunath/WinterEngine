using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DejaVu;

namespace WinterEngine.Toolset.Data.DataTransferObjects
{
    [Serializable]
    public class ResourceTypeDTO
    {
        #region Fields

        readonly UndoRedo<int> _resourceTypeID = new UndoRedo<int>();
        readonly UndoRedo<string> _name = new UndoRedo<string>();

        #endregion

        #region Properties

        public int ResourceTypeID
        {
            get { return _resourceTypeID.Value; }
            set { _resourceTypeID.Value = value; }
        }

        public string Name
        {
            get { return _name.Value; }
            set { _name.Value = value; }
        }

        #endregion

        #region Methods

        ResourceTypeDTO(int resourceTypeID, string resourceName)
        {
            this.ResourceTypeID = resourceTypeID;
            this.Name = resourceName;
        }

        #endregion
    }
}
