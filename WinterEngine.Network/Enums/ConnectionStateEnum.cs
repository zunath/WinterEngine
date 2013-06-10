using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Network.Enums
{
    public enum ConnectionStateEnum
    {
        ConnectRequestSent = 1,
        Connected = 2,
        DisconnectRequestSent = 3,
        Disconnected = 4
    }
}
