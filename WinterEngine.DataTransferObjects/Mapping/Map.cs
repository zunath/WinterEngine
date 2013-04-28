using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace WinterEngine.DataTransferObjects
{
    [Table("Maps")]
    public class Map : GameResourceBase
    {
        #region Fields

        private Tile[][] _tiles;
        private int _width;
        private int _height;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of tiles wide this map is.
        /// </summary>
        public int NumberOfTilesWide
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Gets or sets the number of tiles high this map is.
        /// </summary>
        public int NumberOfTilesHigh
        {
            get { return _height; }
            set { _height = value; }
        }

        public Tile[][] Tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
        }

        #endregion
    }
}
