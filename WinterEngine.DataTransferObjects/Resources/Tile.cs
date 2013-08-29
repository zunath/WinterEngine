using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    [Table("Tiles")]
    public class Tile
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique ID of this Tile
        /// </summary>
        [Key]
        public int TileID { get; set; }

        /// <summary>
        /// Gets or sets the X cell position of the texture being used for this tile.
        /// </summary>
        public int TextureCellX { get; set; }
        /// <summary>
        /// Gets or sets the Y cell position of the texture being used for this tile.
        /// </summary>
        public int TextureCellY { get; set; }
        /// <summary>
        /// Gets or sets the map height position of the tile.
        /// </summary>
        public int TileHeight { get; set; }
        /// <summary>
        /// Gets or sets the map X cell position of the tile.
        /// </summary>
        public int MapCellX { get; set; }
        /// <summary>
        /// Gets or sets the map Y cell position of the tile.
        /// </summary>
        public int MapCellY { get; set; }

        #endregion

        #region Constructors

        public Tile()
        {
        }

        /// <summary>
        /// Constructs a new tile with specified X and Y positions on the texture.
        /// </summary>
        public Tile(int textureCellX, int textureCellY)
        {
            this.TextureCellX = textureCellX;
            this.TextureCellY = textureCellY;
        }

        public Tile(int textureCellX, int textureCellY, int mapCellX, int mapCellY)
        {
            this.TextureCellX = textureCellX;
            this.TextureCellY = textureCellY;
            this.MapCellX = mapCellX;
            this.MapCellY = mapCellY;
        }

        #endregion
    }
}
