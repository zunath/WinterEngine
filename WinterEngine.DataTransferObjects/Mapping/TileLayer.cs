using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("TileLayers")]
    public class TileLayer
    {
        #region Fields

        private int _tileLayerID;
        private Tile[][] _tiles;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique ID of this tile layer.
        /// </summary>
        [Key]
        public int TileLayerID
        {
            get { return _tileLayerID; }
            set { _tileLayerID = value; }
        }

        /// <summary>
        /// Gets or sets the tile layer's tiles.
        /// </summary>
        public Tile[][] Tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
        }

        #endregion

        #region Constructors

        #endregion
    }
}
