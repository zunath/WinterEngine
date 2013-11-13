using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace WinterEngine.Network.BusinessObjects
{
    public class ConnectionStatusEventArgs : EventArgs
    {
        public NetConnection Connection { get; set; }
    }
}
