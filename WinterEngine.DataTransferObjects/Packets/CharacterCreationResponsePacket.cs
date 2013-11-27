using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    public class CharacterCreationResponsePacket: PacketBase
    {
        public SuccessFailEnum SuccessType { get; set; }
        public string ErrorReason { get; set; }


        public CharacterCreationResponsePacket()
        {
            SuccessType = SuccessFailEnum.Unknown;
            ErrorReason = "";
        }
    }
}
