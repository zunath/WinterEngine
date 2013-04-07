﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Table("ContentPackages")]
    public class ContentPackage : GameResourceBase
    {
        #region Fields

        private string _contentPackagePath;
        private string _fileName;
        private string _description;
        private int _loadOrder;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path to the content package path.
        /// </summary>
        public string ContentPackagePath
        {
            get { return _contentPackagePath; }
            set { _contentPackagePath = value; }
        }

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
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the order in which this content package is loaded.
        /// If there is a resource name conflict (I.E: more than one resource with the same name), this value will be used
        /// to determine which resource is loaded.
        /// </summary>
        public int LoadOrder
        {
            get { return _loadOrder; }
            set 
            {
                int modifiedOrder = _loadOrder + value;
                if (modifiedOrder < 1)
                {
                    modifiedOrder = 1;
                }
                _loadOrder = modifiedOrder; 
            }
        }

        #endregion

        #region Constructors

        public ContentPackage()
        {
            this.ResourceType = ResourceTypeEnum.ContentPackage;
        }

        public ContentPackage(string path, bool isSystemResource, string visibleName = "")
        {
            this._contentPackagePath = path;
            this._fileName = Path.GetFileName(path);
            this.ResourceType = ResourceTypeEnum.ContentPackage;
            this.IsSystemResource = isSystemResource;

            if (String.IsNullOrWhiteSpace(visibleName))
            {
                this.VisibleName = Path.GetFileNameWithoutExtension(path);
            }
            else
            {
                this.VisibleName = visibleName;
            }
        }

        #endregion
    }
}
