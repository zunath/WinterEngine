using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects
{
    [Table("LocalVariables")]
    [ProtoContract]
    public class LocalVariable : GameResourceBase
    {
        [Key]
        public int LocalVariableID { get; set; }
        [ProtoMember(1)]
        [MaxLength(64)]
        public string Name { get; set; }
        [ProtoMember(2)]
        [MaxLength(4000)]
        public string Value { get; set; }
    }
}
