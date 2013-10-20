using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
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
    [Serializable]
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

        /// <summary>
        /// Gets or sets the resource type of the content package resource.
        /// </summary>
        public ContentPackageResourceTypeEnum ContentPackageResourceType
        {
            get { return _contentPackageResourceType; }
            set { _contentPackageResourceType = value; }
        }

        [JsonIgnore] // Note: Ignore this property because it will create a circular reference loop when serializing via JSON.NET
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
