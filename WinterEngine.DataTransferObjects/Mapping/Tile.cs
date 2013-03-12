using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("Tiles")]
    public class Tile
    {
        #region Fields

        private int _tileID;
        private Texture2D _texture;
        private int _cellX;
        private int _cellY;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique ID of this Tile
        /// </summary>
        [Key]
        public int TileID
        {
            get { return _tileID; }
            set { _tileID = value; }
        }

        /// <summary>
        /// Gets or sets the Texture2D used by this tile.
        /// </summary>
        public Texture2D SheetTexture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Gets or sets the X position of the cell used by this tile.
        /// </summary>
        public int CellX
        {
            get { return _cellX; }
            set { _cellX = value; }
        }

        /// <summary>
        /// Gets or sets the Y position of the cell used by this tile.
        /// </summary>
        public int CellY
        {
            get { return _cellY; }
            set { _cellY = value; }
        }

        /// <summary>
        /// Gets the rectangle of the destination on the sprite sheet.
        /// </summary>
        public Rectangle DestinationRectangle
        {
            get
            {
                int xPosition = CellX * (int)MappingEnum.TileWidth;
                int yPosition = CellY * (int)MappingEnum.TileHeight;
                Rectangle destinationRectangle = new Rectangle(xPosition, yPosition, (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                return destinationRectangle;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new tile, using the cell X and cell Y positions on
        /// a tileset.
        /// </summary>
        /// <param name="cellX">The cell X position of the tile.</param>
        /// <param name="cellY">The cell Y position of the tile.</param>
        public Tile(Texture2D texture, int cellX, int cellY)
        {
            this._texture = texture;
            this._cellX = cellX;
            this._cellY = cellY;
        }

        #endregion
    }
}
