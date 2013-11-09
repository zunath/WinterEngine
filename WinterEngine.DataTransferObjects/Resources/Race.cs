using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects
{
    [ProtoContract]
    [Serializable]
    [Table("Races")]
    public class Race : GameResourceBase
    {
        #region Fields

        #endregion

        #region Properties

        #endregion
    }
}
