using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    [ProtoContract]
    public class ContentPackageListPacket : PacketBase
    {
        #region Fields

        #endregion

        #region Properties

        [ProtoMember(1)]
        public List<string> FileNames { get; set; }

        #endregion


    }
}
