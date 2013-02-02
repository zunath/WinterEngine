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
    [Table("Tilesets")]
    [Serializable]
    public class Tileset: IEntity
    {
        #region Fields

        private int _tilesetID;
        private string _tilesetName;
        private SpriteSheet _spriteSheet;
        private Tile[][] _tiles;

        #endregion

        #region Properties

        [Key]
        public int TilesetID
        {
            get { return _tilesetID; }
            set { _tilesetID = value; }
        }

        /// <summary>
        /// Gets or sets the name of a tileset
        /// </summary>
        public string Name
        {
            get { return _tilesetName; }
            set { _tilesetName = value; }
        }

        /// <summary>
        /// Gets or sets the 2D array of tiles contained by the tileset.
        /// </summary>
        public Tile[][] Tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
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
