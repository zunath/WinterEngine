using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects
{
    [Serializable]
    [Table("Tilesets")]
    public class Tileset : GameObjectBase
    {
        #region Fields
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Tileset()
        {
        }

        #endregion
    }
}
