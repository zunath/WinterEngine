using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.Toolset.DataLayer.DataTransferObjects
{
    /// <summary>
    /// Details are used to store module-level information.
    /// Typically this is used for storing the module's Name and Tag
    /// but other information may be stored as well.
    /// </summary>
    [Serializable]
    [Table("ModuleDetails")]
    public sealed class ModuleDetail
    {
        #region Fields

        private string _detailUniqueName;
        private string _detailValue;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique detail ID for a module detail.
        /// </summary>
        [Key]
        [MaxLength(16)]
        public string DetailName
        {
            get { return _detailUniqueName; }
            set { _detailUniqueName = value; }
        }

        /// <summary>
        /// Gets or sets the detail value for a module detail.
        /// </summary>
        [MaxLength(32)]
        public string DetailValue
        {
            get { return _detailValue; }
            set { _detailValue = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
