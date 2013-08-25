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
        #region Properties

        // Note: There should only ever be one row in the GameModule table.

        /// <summary>
        /// Gets or sets the module ID.
        /// </summary>
        [Key]
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the module's tag.
        /// </summary>
        [MaxLength(32)]
        public string ModuleTag { get; set; }

        /// <summary>
        /// Gets or sets the module's name
        /// </summary>
        [MaxLength(64)]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or set the module's description
        /// </summary>
        [MaxLength(4000)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the module's comment
        /// </summary>
        [MaxLength(4000)]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the module's max level.
        /// </summary>
        public int MaxLevel { get; set; }

        [NotMapped]
        public string FileName { get; set; }

        #endregion
    }
}
