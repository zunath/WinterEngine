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
    public class Area : GameObject, IEntity
    {
        #region Fields

        public virtual List<Layer> Layers { get; set; }

        #endregion

        #region Properties


        #endregion

        #region Methods
        #endregion
    }
}
