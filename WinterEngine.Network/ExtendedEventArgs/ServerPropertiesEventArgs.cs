using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Entities;

namespace WinterEngine.Network.ExtendedEventArgs
{
    public class ServerPropertiesEventArgs : EventArgs
    {
        public ServerDetails serverDetails { get; set; }

        /// <summary>
        /// Creates a new ServerPropertiesEventArgs object for use in event handling.
        /// </summary>
        /// <param name="details"></param>
        public ServerPropertiesEventArgs(ServerDetails details)
        {
            this.serverDetails = details;
        }
    }
}
