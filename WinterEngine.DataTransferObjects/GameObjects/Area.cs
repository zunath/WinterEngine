using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.BusinessObjects;


namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Areas")]
    public class Area : GameObjectBase
    {
        #region Constants

        private const int MaxTilesWide = 32;
        private const int MaxTilesHigh = 32;

        #endregion

        #region Fields

        private int _tilesWide;
        private int _tilesHigh;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tiles used by this area.
        /// </summary>
        public virtual List<Tile> Tiles { get; set; }

        /// <summary>
        /// Gets or sets the number of tiles wide this map is. Range: 1-32
        /// </summary>
        public int TilesWide
        {
            get
            {
                if (_tilesWide < 1) _tilesWide = 1;
                else if (_tilesWide > MaxTilesWide) _tilesWide = MaxTilesWide;

                return _tilesWide;
            }
            set
            {
                if (value < 1) value = 1;
                else if (value > MaxTilesWide) value = MaxTilesWide;

                _tilesWide = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of tiles high this map is. Range: 1-32
        /// </summary>
        public int TilesHigh
        {
            get
            {
                if (_tilesHigh < 1) _tilesHigh = 1;
                else if (_tilesHigh > MaxTilesHigh) _tilesHigh = MaxTilesHigh;

                return _tilesHigh;
            }
            set
            {
                if (value < 1) value = 1;
                else if (value > MaxTilesHigh) value = MaxTilesHigh;

                _tilesHigh = value;
            }
        }

        // EVENT SCRIPTS
        public int? OnEnterEventScriptID { get; set; }
        public int? OnExitEventScriptID { get; set; }
        public int? OnHeartbeatEventScriptID { get; set; }
        public int? OnUserDefinedEventScriptID { get; set; }

        
        [ForeignKey("OnEnterEventScriptID")]
        public virtual Script OnAreaEnterEventScript { get; set; }
        [ForeignKey("OnExitEventScriptID")]
        public virtual Script OnAreaExitEventScript { get; set; }
        [ForeignKey("OnHeartbeatEventScriptID")]
        public virtual Script OnAreaHeartbeatEventScript { get; set; }
        [ForeignKey("OnUserDefinedEventScriptID")]
        public virtual Script OnAreaUserDefinedEventScript { get; set; }
        
        #endregion

        #region Constructors

        public Area()
        {
        }

        public Area(int tilesWide, int tilesHigh)
        {
            this.TilesWide = tilesWide;
            this.TilesHigh = tilesHigh;

            int totalNumberOfTiles = tilesWide * tilesHigh;
            Tiles = new List<Tile>();

            Tile emptyTile = new Tile
            {

            };

            for (int index = 1; index <= totalNumberOfTiles; index++)
            {
                Tiles.Add(emptyTile);
            }
        }

        #endregion
    }
}
