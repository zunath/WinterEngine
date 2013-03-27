using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Table("ContentPackageResources")]
    public class ContentPackageResource : GameResourceBase
    {
        #region Fields

        private ContentPackage _contentPackage;
        private string fileName;
        private GameObjectTypeEnum _gameObjectType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content package to which this graphic resource belongs.
        /// </summary>
        public ContentPackage Package
        {
            get { return _contentPackage; }
            set { _contentPackage = value; }
        }

        /// <summary>
        /// Gets or sets the name of the resource file which is contained inside of the content package.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Gets or sets this content package resource's game object type.
        /// </summary>
        public GameObjectTypeEnum GameObjectType
        {
            get { return _gameObjectType; }
            set { _gameObjectType = value; }
        }

        #endregion
    }
}
