using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    class PacketBase
    {
        #region Properties

        public PacketTypeEnum PacketType { get; set; }

        #endregion
    }
}
