using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.Mapping;

namespace WinterEngine.Toolset.ExtendedEventArgs
{
    public class TilesetEventArgs : EventArgs
    {
        private Tileset _tileset;

        /// <summary>
        /// Gets or sets the tileset passed as an event argument.
        /// </summary>
        public Tileset Tileset
        {
            get { return _tileset; }
            set { _tileset = value; }
        }

        public TilesetEventArgs(Tileset tileset)
        {
            this._tileset = tileset;
        }

    }
}
