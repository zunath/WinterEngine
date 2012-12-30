using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DejaVu;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects
{
    [Serializable]
    public class ResourceCategoryDTO
    {
        #region Fields

        readonly UndoRedo<int> _resourceCategoryID = new UndoRedo<int>();
        readonly UndoRedo<int> _resourceTypeID = new UndoRedo<int>();
        readonly UndoRedo<string> _resourceName = new UndoRedo<string>();

        #endregion

        #region Properties

        public int ResourceCategoryID
        {
            get { return _resourceCategoryID.Value; }
            set { _resourceCategoryID.Value = value; }
        }

        public int ResourceTypeID
        {
            get { return _resourceTypeID.Value; }
            set { _resourceTypeID.Value = value; }
        }

        public string ResourceName
        {
            get { return _resourceName.Value; }
            set { _resourceName.Value = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
