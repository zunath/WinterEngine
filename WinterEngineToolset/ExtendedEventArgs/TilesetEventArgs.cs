using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Graphics;
using WinterEngine.DataTransferObjects.Mapping;

namespace WinterEngine.Toolset.ExtendedEventArgs
{
    public class TilesetEventArgs : EventArgs
    {
        private Tileset _tileset;
        private SpriteSheet _spriteSheet;

        /// <summary>
        /// Gets or sets the tileset passed as an event argument.
        /// </summary>
        public Tileset Tileset
        {
            get { return _tileset; }
            set { _tileset = value; }
        }

        /// <summary>
        /// Gets or sets the sprite sheet passed as an event argument.
        /// </summary>
        public SpriteSheet SpriteSheet
        {
            get { return _spriteSheet; }
            set { _spriteSheet = value; }
        }

        public TilesetEventArgs(Tileset tileset, SpriteSheet spriteSheet)
        {
            this._tileset = tileset;
            this._spriteSheet = spriteSheet;
        }

    }
}
