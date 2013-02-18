using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.DataTransferObjects.Mapping
{
    //[Table("LayerRows")]
    //[Serializable]
    public class LayerRow: IEntity
    {

        #region Fields

        private int _layerRowID;
        private List<Tile> _rowTiles;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ID of the layer row
        /// </summary>
        [Key]
        public int LayerRowID
        {
            get { return _layerRowID; }
            set { _layerRowID = value; }
        }

        /// <summary>
        /// Gets or sets the row of tiles 
        /// </summary>
        public List<Tile> RowTiles
        {
            get { return _rowTiles; }
            set { _rowTiles = value; }
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
