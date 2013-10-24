using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ProtoBuf;
using WinterEngine.DataTransferObjects.Enumerations;
using Newtonsoft.Json;

namespace WinterEngine.DataTransferObjects
{
    [Table("LocalVariables")]
    [ProtoContract]
    public class LocalVariable
    {
        [Key]
        public int LocalVariableID { get; set; }
        [ProtoMember(1)]
        [MaxLength(64)]
        public string Name { get; set; }
        [ProtoMember(2)]
        [MaxLength(4000)]
        public string Value { get; set; }

        public int GameObjectBaseID { get; set; }

        [JsonIgnore] // Note: Ignore this property because it will create a circular reference loop when serializing via JSON.NET
        [ForeignKey("GameObjectBaseID")]
        public virtual GameObjectBase GameObject { get; set; }
    }
}
