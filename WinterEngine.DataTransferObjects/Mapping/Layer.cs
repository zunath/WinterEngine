using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataTransferObjects.Mapping
{
    [Table("Layers")]
    [Serializable]
    public class Layer: IEntity
    {

        #region Fields

        private int _layerID;
        private List<LayerRow> _layerRows;
        private Tileset _tileset;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the layer ID
        /// </summary>
        [Key]
        public int LayerID
        {
            get { return _layerID; }
            set { _layerID = value; }
        }

        /// <summary>
        /// Gets or sets the list of layer rows.
        /// </summary>
        public List<LayerRow> LayerRows
        {
            get { return _layerRows; }
            set { _layerRows = value; }
        }

        /// <summary>
        /// Gets or sets the tileset used by the layer.
        /// </summary>
        public Tileset LayerTileset
        {
            get { return _tileset; }
            set { _tileset = value; }
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
