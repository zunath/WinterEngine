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

        private ContentPackage _contentPackage;
        private ContentPackageResourceTypeEnum _contentPackageResourceType;
        private ContentBuilderFileTypeEnum _fileType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content package to which this graphic resource belongs.
        /// </summary>
        [ForeignKey("ContentPackageID")]
        public ContentPackage Package
        {
            get 
            {
                if (!Object.ReferenceEquals(_contentPackage, null))
                {
                    if (String.IsNullOrWhiteSpace(_contentPackage.ContentPackagePath))
                    {
                        _contentPackage.ContentPackagePath = DirectoryPaths.ContentPackageDirectoryPath + _contentPackage.FileName;
                    }
                }

                return _contentPackage; 
            }
            set { _contentPackage = value; }
        }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        [MaxLength(64)]
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the path to the resource.
        /// </summary>
        [NotMapped]
        public string ResourcePath { get; set; }

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

        /// <summary>
        /// Gets or sets whether the resource is contained in a content package or exists on disk.
        /// </summary>
        [NotMapped]
        public ContentBuilderFileTypeEnum FileType { get; set; }

        /// <summary>
        /// Gets or sets the name of the file. The file extension is included.
        /// </summary>
        [MaxLength(4000)]
        public string FileName { get; set; }

        public int ContentPackageID { get; set; }

        #endregion

        #region Constructors

        public ContentPackageResource()
        {
            this.ResourceType = ResourceTypeEnum.ContentPackageResource;
        }

        public ContentPackageResource(string resourcePath, ContentPackageResourceTypeEnum resourceType, ContentBuilderFileTypeEnum fileType)
        {
            this.ResourceType = ResourceTypeEnum.ContentPackageResource;
            this.ResourceName = Path.GetFileNameWithoutExtension(resourcePath);
            this.ResourcePath = resourcePath;
            this.ContentPackageResourceType = resourceType;
            this.FileName = Path.GetFileName(resourcePath);
        }

        public ContentPackageResource(ContentPackageResourceTypeEnum resourceType, ContentBuilderFileTypeEnum fileType, string resourceName, string fileName)
        {
            this.ResourceType = ResourceTypeEnum.ContentPackageResource;
            this.ResourceName = resourceName;
            this.ContentPackageResourceType = resourceType;
            this.FileType = fileType;
            this.FileName = fileName;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// ToString override for use with the list boxes and other WinForms controls.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string retValue = ResourcePath;

            if (!String.IsNullOrWhiteSpace(ResourceName))
            {
                retValue = ResourceName;
            }

            return retValue;
        }


        /// <summary>
        /// Returns a unique hash for this object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            // Reference: http://stackoverflow.com/questions/892618/create-a-hashcode-of-two-numbers
            int hashPrime = 23 * 31;
            int fileNameHash = FileName.GetHashCode();
            int contentPackageIDHash = ContentPackageID.GetHashCode();

            return hashPrime + fileNameHash; //+ contentPackageIDHash;
        }

        /// <summary>
        /// Returns true if two ContentPackage objects are the same.
        /// Returns false if they are not the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            ContentPackageResource comparedObject = obj as ContentPackageResource;

            if (Object.ReferenceEquals(comparedObject, null))
            {
                return false;
            }
            else if (this.GetHashCode() == comparedObject.GetHashCode())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

    }
}
