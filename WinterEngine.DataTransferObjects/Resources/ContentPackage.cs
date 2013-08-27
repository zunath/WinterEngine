using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    [Table("ContentPackages")]
    public class ContentPackage : GameResourceBase
    {
        #region Fields

        private string _fileName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the physical file. This includes the file extension.
        /// </summary>
        [MaxLength(4000)]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = Path.GetFileName(value); }
        }

        /// <summary>
        /// Gets or sets the description of the content package.
        /// </summary>
        [MaxLength(4000)]
        public string Description { get; set; }

        public virtual List<ContentPackageResource> ResourceList { get; set; }

        #endregion

        #region Constructors

        public ContentPackage()
        {
            this.ResourceType = ResourceTypeEnum.ContentPackage;
            this.ResourceList = new List<ContentPackageResource>();
        }

        #endregion

    }
}
