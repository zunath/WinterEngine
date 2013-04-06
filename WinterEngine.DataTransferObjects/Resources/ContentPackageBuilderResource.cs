using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Resources
{
    /// <summary>
    /// Contains details about content package resources for use with the Content Builder form.
    /// Should not be used elsewhere.
    /// </summary>
    public class ContentPackageBuilderResource
    {
        #region Fields

        private string _resourceName;
        private string _resourcePath;
        private GameObjectTypeEnum _resourceType;
        private ContentBuilderFileTypeEnum _fileType;
        private string _fileName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        public string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

        /// <summary>
        /// Gets or sets the path to the resource.
        /// </summary>
        public string ResourcePath
        {
            get { return _resourcePath; }
            set { _resourcePath = value; }
        }

        /// <summary>
        /// Gets or sets the resource type of the content package resource.
        /// </summary>
        public GameObjectTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        /// <summary>
        /// Gets or sets whether the resource is contained in a content package or exists on disk.
        /// </summary>
        public ContentBuilderFileTypeEnum FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        /// <summary>
        /// Gets or sets the name of the file. The file extension is included.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        #endregion

        #region Constructors

        public ContentPackageBuilderResource(string resourcePath, GameObjectTypeEnum resourceType, ContentBuilderFileTypeEnum fileType)
        {
            this.ResourceName = Path.GetFileNameWithoutExtension(resourcePath);
            this.ResourcePath = resourcePath;
            this.ResourceType = resourceType;
            this.FileName = Path.GetFileName(resourcePath);
        }

        public ContentPackageBuilderResource(GameObjectTypeEnum resourceType, ContentBuilderFileTypeEnum fileType, string resourceName, string fileName)
        {
            this.ResourceName = resourceName;
            this.ResourceType = resourceType;
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

        #endregion

    }
}
