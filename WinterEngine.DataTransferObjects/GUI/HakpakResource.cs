using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.GUI
{
    public class HakpakResource
    {
        #region Fields

        private string _resourceName;
        private string _filePath;
        private bool _isItem;
        private bool _is2D;
        private HakpakResource _linkedResource;
        private ItemPartEnum _partType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of a hakpak resource.
        /// </summary>
        public string ResourceName
        {
            get { return _resourceName; }
            set { _resourceName = value; }
        }

        /// <summary>
        /// Gets or sets the file path of the resource
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        /// <summary>
        /// Gets the extension of the file.
        /// </summary>
        public string FileExtension
        {
            get { return Path.GetExtension(_filePath); }
        }

        /// <summary>
        /// Gets or sets whether the resource is an item.
        /// </summary>
        public bool IsItem
        {
            get { return _isItem; }
            set { _isItem = value; }
        }

        /// <summary>
        /// Gets or sets the linked resource
        /// </summary>
        public HakpakResource LinkedResource
        {
            get { return _linkedResource; }
            set { _linkedResource = value; }
        }

        /// <summary>
        /// Gets or sets whether the resource is 2D.
        /// If this is true, the resource is a 2D object.
        /// If this is false, the resource is a 3D object.
        /// </summary>
        public bool Is2D
        {
            get { return _is2D; }
            set { _is2D = value; }
        }

        /// <summary>
        /// Gets or sets the item part type of the resource.
        /// </summary>
        public ItemPartEnum ItemPartType
        {
            get { return _partType; }
            set { _partType = value; }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(ResourceName))
            {
                string retString = ResourceName;
                
                // Display the file extension, if available.
                if (!String.IsNullOrWhiteSpace(FilePath))
                {
                    FileInfo info = new FileInfo(FilePath);
                    retString += "(" + info.Extension.ToUpper() + ")";
                }

                return retString;
            }
            else
            {
                return new FileInfo(FilePath).Directory.Name + "\\" + Path.GetFileName(FilePath);
            }
        }

        #endregion

    }
}
