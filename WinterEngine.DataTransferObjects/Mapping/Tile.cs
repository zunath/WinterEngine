using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Graphics;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("Tiles")]
    public class Tile : IEntity
    {
        #region Fields

        private int _tileID;
        private int _spriteSheetX;
        private int _spriteSheetY;
        private bool _isPassable;
        private const int _spriteSheetWidth = 64;
        private const int _spriteSheetHeight = 64;

        #endregion

        #region Properties


        [Key]
        public virtual SpriteSheet TileSpriteSheet { get; set; }

        [Key]
        public int ID
        {
            get { return _tileID; }
            set { _tileID = value; }
        }

        /// <summary>
        /// Gets or sets the X position of the tile on the sprite sheet
        /// </summary>
        public int SpriteSheetX
        {
            get { return _spriteSheetX; }
            set { _spriteSheetX = value; }
        }

        /// <summary>
        /// Gets or sets the Y position of the tile on the sprite sheet
        /// </summary>
        public int SpriteSheetY
        {
            get { return _spriteSheetY; }
            set { _spriteSheetY = value; }
        }

        /// <summary>
        /// Gets or sets whether the entire tile is passable by creatures.
        /// </summary>
        public bool IsPassable
        {
            get { return _isPassable; }
            set { _isPassable = value; }
        }

        /// <summary>
        /// Gets the width of the tile on the sprite sheet
        /// </summary>
        public int SpriteSheetWidth
        {
            get { return _spriteSheetWidth; }
        }

        /// <summary>
        /// Gets the height of the tile on the sprite sheet.
        /// </summary>
        public int SpriteSheetHeight
        {
            get { return _spriteSheetHeight; }
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
