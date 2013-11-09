using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Tilesets")]
    public class Tileset : GameObjectBase
    {
        #region Fields
        #endregion

        #region Properties

        [JsonIgnore] // Ignore to prevent circular references
        public virtual List<Tile> TileList { get; set; }

        #endregion

        #region Constructors

        public Tileset()
        {
        }

        #endregion
    }
}
