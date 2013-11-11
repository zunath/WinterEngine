using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("TileCollisionBoxes")]
    public class TileCollisionBox
    {
        #region Properties

        [Key]
        public int CollisionBoxID { get; set; }
        public bool IsPassable { get; set; }
        public int TileLocationIndex { get; set; }

        public virtual int TileID { get; set; }
        [ForeignKey("TileID")]
        public virtual Tile ParentTile { get; set; }

        #endregion
    }
}
