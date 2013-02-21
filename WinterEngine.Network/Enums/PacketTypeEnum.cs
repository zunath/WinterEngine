using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.Network.Enums
{
    public enum PacketTypeEnum : byte
    {
        Server = 1,
        Request = 2,
        ClientServerList = 3,
    }
}
