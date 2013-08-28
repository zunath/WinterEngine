using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace WinterEngine.DataTransferObjects
{
    [Table("Maps")]
    public class Map : GameResourceBase
    {
        #region Constants

        private const int MaxTilesWide = 32;
        private const int MaxTilesHigh = 32;

        #endregion

        #region Fields

        private int _tilesWide;
        private int _tilesHigh;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tiles used by this map.
        /// </summary>
        public List<Tile> Tiles { get; set; }

        public int? GraphicID { get; set; }

        /// <summary>
        /// Gets or sets the spritesheet graphic used by this map.
        /// </summary>
        [ForeignKey("GraphicID")]
        public ContentPackageResource Graphic { get; set; }

        /// <summary>
        /// Gets or sets the number of tiles wide this map is. Range: 1-32
        /// </summary>
        public int TilesWide 
        {
            get
            {
                if (_tilesWide < 1) _tilesWide = 1;
                else if (_tilesWide > MaxTilesWide) _tilesWide = MaxTilesWide;
                
                return _tilesWide;
            }
            set
            {
                if (value < 1) value = 1;
                else if (value > MaxTilesWide) value = MaxTilesWide;

                _tilesWide = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of tiles high this map is. Range: 1-32
        /// </summary>
        public int TilesHigh 
        {
            get
            {
                if (_tilesHigh < 1) _tilesHigh = 1;
                else if (_tilesHigh > MaxTilesHigh) _tilesHigh = MaxTilesHigh;

                return _tilesHigh;
            }
            set
            {
                if (value < 1) value = 1;
                else if (value > MaxTilesHigh) value = MaxTilesHigh;

                _tilesHigh = value;
            }
        }

        #endregion

        #region Constructors

        public Map()
        {
        }

        public Map(int tilesWide, int tilesHigh)
        {
            this.TilesWide = tilesWide;
            this.TilesHigh = tilesHigh;

            int totalNumberOfTiles = tilesWide * tilesHigh;
            Tiles = new List<Tile>();

            Tile emptyTile = new Tile
            {
                
            };

            for (int index = 1; index <= totalNumberOfTiles; index++ )
            {
                Tiles.Add(emptyTile);
            }

        }

        #endregion
    }
}
