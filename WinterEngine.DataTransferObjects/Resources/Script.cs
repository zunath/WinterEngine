using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Serializable]
    [Table("Scripts")]
    public class Script : GameResourceBase
    {
        #region Fields

        private int _resourceCategoryID;
        private Category _resourceCategory;

        #endregion

        #region Properties

        public int ResourceCategoryID
        {
            get { return _resourceCategoryID; }
            set { _resourceCategoryID = value; }
        }

        public Category ResourceCategory
        {
            get { return _resourceCategory; }
            set { _resourceCategory = value; }
        }

        #endregion
    }
}
