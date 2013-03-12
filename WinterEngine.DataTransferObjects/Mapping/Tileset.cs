using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("Tilesets")]
    public class Tileset : GameResourceBase
    {
        #region Fields

        private GraphicResource _graphicResource;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the graphic resource used by this tileset.
        /// </summary>
        public GraphicResource Graphic
        {
            get { return _graphicResource; }
            set { _graphicResource = value; }
        }

        #endregion
    }
}
