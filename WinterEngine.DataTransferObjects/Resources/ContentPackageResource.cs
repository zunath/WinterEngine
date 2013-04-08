using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Table("ContentPackageResources")]
    public class ContentPackageResource : GameResourceBase
    {
        #region Fields

        private ContentPackage _contentPackage;
        private string _resourceName;
        private string _resourcePath;
        private ContentPackageResourceTypeEnum _contentPackageResourceType;
        private ContentBuilderFileTypeEnum _fileType;
        private string _fileName;
        private int _contentPackageID;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content package to which this graphic resource belongs.
        /// </summary>
        [ForeignKey("ContentPackageID")]
        public ContentPackage Package
        {
            get { return _contentPackage; }
            set { _contentPackage = value; }
        }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        [MaxLength(64)]
        public string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

        /// <summary>
        /// Gets or sets the path to the resource.
        /// </summary>
        [MaxLength(4000)]
        [NotMapped]
        public string ResourcePath
        {
            get { return _resourcePath; }
            set { _resourcePath = value; }
        }

        /// <summary>
        /// Gets or sets the resource type of the content package resource.
        /// </summary>
        public ContentPackageResourceTypeEnum ContentPackageResourceType
        {
            get { return _contentPackageResourceType; }
            set { _contentPackageResourceType = value; }
        }

        /// <summary>
        /// Gets or sets whether the resource is contained in a content package or exists on disk.
        /// </summary>
        [NotMapped]
        public ContentBuilderFileTypeEnum FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        /// <summary>
        /// Gets or sets the name of the file. The file extension is included.
        /// </summary>
        [MaxLength(4000)]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public int ContentPackageID
        {
            get { return _contentPackageID; }
            set { _contentPackageID = value; }
        }

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
