using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Resources
{
    /// <summary>
    /// Data Transfer Object for use with the Advanced View.
    /// </summary>
    public class TableInfo
    {
        #region Fields

        private TableTypeEnum _tableType;
        private string _displayName;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of table this object refers to.
        /// </summary>
        public TableTypeEnum TableType
        {
            get { return _tableType; }
            set { _tableType = value; }
        }

        /// <summary>
        /// Gets or sets the display name which is viewable by the user.
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return DisplayName;
        }

        #endregion
    }
}
