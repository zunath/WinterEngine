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
    public class Tile : IEntity
    {
        #region Fields

        public int SpriteSheetX {get; set;}
        public int SpriteSheetY {get; set;}
        public bool IsPassable { get; set; }
        private const int _spriteSheetWidth = 64;
        private const int _spriteSheetHeight = 64;

        #endregion

        #region Properties

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
    }
}
