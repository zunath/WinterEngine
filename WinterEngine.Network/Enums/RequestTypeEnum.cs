using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Network.Enums
{
    public enum RequestTypeEnum : byte
    {
        ServerContentPackageList = 1,
        Disconnect = 2
    }
}
