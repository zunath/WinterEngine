using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.DataTransferObjects.Mapping
{
    public class Cell: IEntity
    {
        #region Fields

        private int _cellX;
        private int _cellY;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the X position of the cell
        /// </summary>
        public int CellX
        {
            get { return _cellX; }
            set { _cellX = value; }
        }

        /// <summary>
        /// Gets or sets the Y position of the cell
        /// </summary>
        public int CellY
        {
            get { return _cellY; }
            set { _cellY = value; }
        }

        /// <summary>
        /// Gets the X position of the tile
        /// </summary>
        public int TileX
        {
            get
            {
                // Sprite sheet width is a constant that we need to grab from the tile.
                // May not be the best way to do this...
                Tile tile = new Tile();
                return _cellX * tile.SpriteSheetWidth;
            }
        }

        /// <summary>
        /// Gets the Y position of the tile
        /// </summary>
        public int TileY
        {
            get
            {
                Tile tile = new Tile();
                return _cellY * tile.SpriteSheetHeight;
            }
        }

        /// <summary>
        /// Gets the maximum width any tile may be.
        /// </summary>
        public int MaxTileWidth
        {
            get
            {
                Tile tile = new Tile();
                return tile.SpriteSheetWidth;
            }
        }

        /// <summary>
        /// Gets the maximum height any time may be.
        /// </summary>
        public int MaxTileHeight
        {
            get
            {
                Tile tile = new Tile();
                return tile.SpriteSheetHeight;
            }
        }

        #endregion

        #region Constructors
        #endregion

        #region Methods

        #endregion

        #region Overrides
        #endregion
    }
}
