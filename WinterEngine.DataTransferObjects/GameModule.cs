using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("GameModule")]
    public sealed class GameModule
    {
        #region Fields

        // Note: There should only ever be one row in the GameModule table.
        private int _moduleID;
        private string _moduleTag;
        private string _moduleName;
        private string _description;
        private string _comment;
        private int _maxLevel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the module ID.
        /// </summary>
        [Key]
        public int ModuleID
        {
            get { return _moduleID; }
            set { _moduleID = value; }
        }

        /// <summary>
        /// Gets or sets the module's tag.
        /// </summary>
        [MaxLength(32)]
        public string ModuleTag
        {
            get { return _moduleTag; }
            set { _moduleTag = value; }
        }

        /// <summary>
        /// Gets or sets the module's name
        /// </summary>
        [MaxLength(64)]
        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        /// <summary>
        /// Gets or set the module's description
        /// </summary>
        [MaxLength(4000)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the module's comment
        /// </summary>
        [MaxLength(4000)]
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        /// <summary>
        /// Gets or sets the module's max level.
        /// </summary>
        public int MaxLevel
        {
            get { return _maxLevel; }
            set { _maxLevel = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
