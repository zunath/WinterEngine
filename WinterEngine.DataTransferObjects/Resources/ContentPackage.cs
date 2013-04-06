using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Table("ContentPackages")]
    public class ContentPackage : GameResourceBase
    {
        #region Fields

        private string _contentPackagePath;
        private string _fileName;
        private string _description;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path to the content package path.
        /// This value is not saved to the database.
        /// </summary>
        [NotMapped]
        public string ContentPackagePath
        {
            get { return _contentPackagePath; }
            set { _contentPackagePath = value; }
        }

        /// <summary>
        /// Gets or sets the name of the physical file. This excludes the path and the extension.
        /// </summary>
        [MaxLength(4000)]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = Path.GetFileNameWithoutExtension(value); }
        }

        /// <summary>
        /// Gets or sets the description of the content package.
        /// </summary>
        [MaxLength(4000)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion
    }
}
