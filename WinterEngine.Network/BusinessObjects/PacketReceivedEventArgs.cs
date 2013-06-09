using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.BusinessObjects
{
    public class PacketReceivedEventArgs : EventArgs
    {
        public PacketBase Packet { get; set; }

        public PacketReceivedEventArgs(PacketBase packet)
        {
            this.Packet = packet;
        }
    }
}
