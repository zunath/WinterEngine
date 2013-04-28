using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using WinterEngine.DataTransferObjects.Mapping;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    [Serializable]
    [Table("Areas")]
    public class Area : GameObjectBase
    {
        #region Fields

        private Tileset _tileset;

        #endregion

        #region Properties

        public Tileset AreaTileset
        {
            get { return _tileset; }
            set { _tileset = value; }
        }

        #endregion
    }
}
