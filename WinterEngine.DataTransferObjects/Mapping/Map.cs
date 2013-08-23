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
        #region Properties

        /// <summary>
        /// Gets or sets the tiles used by this map.
        /// </summary>
        public List<Tile> Tiles { get; set; }

        public int GraphicID { get; set; }

        /// <summary>
        /// Gets or sets the spritesheet graphic used by this map.
        /// </summary>
        [ForeignKey("GraphicID")]
        public ContentPackageResource Graphic { get; set; }

        /// <summary>
        /// Gets or sets the number of tiles wide this map is.
        /// </summary>
        public int TilesWide { get; set; }

        /// <summary>
        /// Gets the number of tiles high this map is.
        /// </summary>
        public int TilesHigh { get; set; }

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
