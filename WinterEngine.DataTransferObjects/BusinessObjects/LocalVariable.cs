using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.BusinessObjects
{
    [ProtoContract]
    public class LocalVariable
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Value { get; set; }
    }
}
