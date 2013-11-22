using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ProtoBuf;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    [ProtoContract]
    [Serializable]
    [Table("Genders")]
    public class Gender : GameResourceBase
    {
        [ProtoMember(1)]
        public GenderTypeEnum GenderType { get; set; }
    }
}
