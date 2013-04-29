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
        #region Fields

        private int _tileID;
        private int _texturePositionX;
        private int _texturePositionY;

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
        /// Returns true if a tile has texture positions set.
        /// Returns false if a tile does not have texture positions set.
        /// </summary>
        public bool HasGraphic
        {
            get
            {
                if (_texturePositionX > 0 && _texturePositionY > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new tile
        /// </summary>
        public Tile()
        {
        }

        /// <summary>
        /// Constructs a new tile with specified X and Y positions on the texture.
        /// </summary>
        public Tile(int texturePositionX, int texturePositionY)
        {
            this._texturePositionX = texturePositionX;
            this._texturePositionY = texturePositionY;
        }

        #endregion
    }
}
