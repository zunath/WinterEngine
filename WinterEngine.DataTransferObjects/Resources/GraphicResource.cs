using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Serializable]
    [Table("GraphicResources")]
    public sealed class GraphicResource : GameResource
    {
        #region Fields

        private string _resourcePackagePath;
        private string _resourceFileName;
        private bool _is2DGraphic;
        private string _temporaryDisplayName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path to the package containing this GraphicResource
        /// </summary>
        public string ResourcePackagePath
        {
            get { return _resourcePackagePath; }
            set { _resourcePackagePath = value; }
        }

        /// <summary>
        /// Gets or sets the name of the resource file for this GraphicResource
        /// </summary>
        public string ResourceFileName
        {
            get { return _resourceFileName; }
            set { _resourceFileName = value; }
        }

        /// <summary>
        /// Gets or sets whether or not this graphic resource is a 2-Dimensional graphic.
        /// </summary>
        public bool Is2DGraphic
        {
            get { return _is2DGraphic; }
            set { _is2DGraphic = value; }
        }

        /// <summary>
        /// Gets or sets the temporary display name. 
        /// If this is set, this object's ToString() method will return this value. 
        /// </summary>
        public string TemporaryDisplayName
        {
            get { return _temporaryDisplayName; }
            set { _temporaryDisplayName = value; }
        }

        #endregion

        #region Overrides
        
        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(TemporaryDisplayName))
            {
                return base.ToString();
            }
            else
            {
                return TemporaryDisplayName;
            }
        }

        #endregion

    }
}
