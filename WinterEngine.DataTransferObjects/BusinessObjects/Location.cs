using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.BusinessObjects
{
    [ProtoContract]
    public class Location
    {
        [ProtoMember(1)]
        public int AreaID { get; set; }
        [ProtoMember(2)]
        public float X { get; set; }
        [ProtoMember(3)]
        public float Y { get; set; }
        [ProtoMember(4)]
        public float Z { get; set; }
    }
}
