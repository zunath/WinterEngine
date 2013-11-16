using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Packets;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
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
