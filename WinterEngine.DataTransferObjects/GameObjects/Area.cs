using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinterEngine.DataTransferObjects.GameObjects
{
    [Serializable]
    [Table("Areas")]
    public class Area : GameObjectBase
    {
        //public List<Layer> Layers { get; set; }
    }
}
