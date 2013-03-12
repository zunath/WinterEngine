using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("TileMaps")]
    public class TileMap : GameResourceBase
    {
        #region Fields

        private Tileset _tileset;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tileset used by the map.
        /// </summary>
        public Tileset MapTileset
        {
            get { return _tileset; }
            set { _tileset = value; }
        }

        #endregion

        #region Constructors

        public TileMap()
        {
        }

        #endregion

    }
}
