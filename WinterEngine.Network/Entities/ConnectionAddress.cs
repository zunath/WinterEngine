using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WinterEngine.Network.Entities
{
    public class ConnectionAddress
    {
        #region Properties

        public IPAddress IP { get; set; }
        public int Port { get; set; }

        #endregion
    }
}
