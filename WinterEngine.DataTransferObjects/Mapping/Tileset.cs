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
        #region Properties

        [Key]
        public int TilesetID { get; set; }
        public string Name { get; set; }
        public Tile[][] Tiles { get; set; }
        public int SpriteSheetID { get; set; }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
