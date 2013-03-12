using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Resources
{
    [Table("GraphicResources")]
    public class GraphicResource : GameResourceBase
    {
        #region Fields

        private ContentPackage _contentPackage;
        private string _graphicFileName;

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
        /// Gets or sets the name of the graphic's file which is contained inside of the content package.
        /// </summary>
        public string GraphicFileName
        {
            get { return _graphicFileName; }
            set { _graphicFileName = value; }
        }

        #endregion
    }
}
