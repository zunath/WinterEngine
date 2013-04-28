using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;


namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Areas")]
    public class Area : GameObjectBase
    {
        #region Fields

        private Map _map;

        #endregion

        #region Properties

        public Map TileMap
        {
            get { return _map; }
            set { _map = value; }
        }

        #endregion
    }
}
