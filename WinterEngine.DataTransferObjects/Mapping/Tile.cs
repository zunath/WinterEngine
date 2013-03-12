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
        private Cell _cell;
        private Tileset _tileset;
        
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
        /// Gets or sets the cell of the tile.
        /// </summary>
        public Cell TileCell
        {
            get { return _cell; }
            set { _cell = value; }
        }

        /// <summary>
        /// Gets or sets the tileset which own this tile object.
        /// </summary>
        public Tileset TileTileset
        {
            get { return _tileset; }
            set { _tileset = value; }
        }

        /// <summary>
        /// Gets the rectangle of the destination on the sprite sheet.
        /// </summary>
        [NotMapped]
        public Rectangle DestinationRectangle
        {
            get
            {
                int xPosition = TileCell.X * (int)MappingEnum.TileWidth;
                int yPosition = TileCell.Y * (int)MappingEnum.TileHeight;
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
        /// <param name="cell">The cell associated with this tile.</param>
        public Tile(Cell cell)
        {
            this._cell = cell;
        }

        #endregion
    }
}
