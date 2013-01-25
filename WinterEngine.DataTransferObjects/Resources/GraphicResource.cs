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

        #endregion
    }
}
