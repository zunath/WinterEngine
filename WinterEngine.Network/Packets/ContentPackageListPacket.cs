using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    public class ContentPackageListPacket : PacketBase
    {
        #region Fields

        #endregion

        #region Properties

        public List<string> FileNames { get; set; }

        #endregion


    }
}
