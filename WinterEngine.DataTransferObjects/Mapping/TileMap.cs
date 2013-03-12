using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("TileMaps")]
    public class TileMap
    {
        #region Fields

        private int _tileMapID;
        private List<TileLayer> _tileLayers;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique ID number of the Tile Map.
        /// </summary>
        [Key]
        public int TileMapID
        {
            get { return _tileMapID; }
            set { _tileMapID = value; }
        }

        /// <summary>
        /// Gets or sets the layers of tiles contained in this TileMap
        /// </summary>
        public List<TileLayer> TileLayers
        {
            get { return _tileLayers; }
            set { _tileLayers = value; }
        }

        #endregion

        #region Constructors

        public TileMap()
        {
        }

        #endregion

    }
}
