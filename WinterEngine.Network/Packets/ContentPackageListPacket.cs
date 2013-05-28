using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    public class ContentPackageListPacket : Packet
    {
        #region Fields

        private PacketTypeEnum _packetType = PacketTypeEnum.ContentPackageList;

        #endregion

        #region Properties

        public List<string> FileNames { get; set; }

        public override PacketTypeEnum PacketType
        {
            get { return _packetType; }
            set{_packetType = value;}
        }

        #endregion


    }
}
