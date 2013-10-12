using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Paths;

namespace WinterEngine.DataTransferObjects
{
    [Table("ContentPackageResources")]
    public class ContentPackageResource : GameResourceBase
    {
        #region Fields

        private ContentPackageResourceTypeEnum _contentPackageResourceType;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the file. The file extension is included.
        /// </summary>
        [MaxLength(4000)]
        public string FileName { get; set; }

        public int ContentPackageResourceTypeID
        {
            get { return (int)_contentPackageResourceType; }
            set
            {
                _contentPackageResourceType = (ContentPackageResourceTypeEnum)Enum.Parse(typeof(ContentPackageResourceTypeEnum), Convert.ToString(value));
            }
        }

        /// <summary>
        /// Gets or sets the resource type of the content package resource.
        /// </summary>
        [NotMapped]
        public ContentPackageResourceTypeEnum ContentPackageResourceType
        {
            get { return _contentPackageResourceType; }
            set { _contentPackageResourceType = value; }
        }

        public virtual ContentPackage ContentPackage { get; set; }

        #endregion

        #region Constructors

        public ContentPackageResource()
        {
            this.ResourceType = ResourceTypeEnum.ContentPackageResource;
        }

        #endregion

    }
}
