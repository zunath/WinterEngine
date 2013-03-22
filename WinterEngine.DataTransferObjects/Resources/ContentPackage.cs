using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Table("ContentPackages")]
    public class ContentPackage : GameResourceBase
    {
        #region Fields

        private string _contentPackagePath;

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

        #endregion
    }
}
