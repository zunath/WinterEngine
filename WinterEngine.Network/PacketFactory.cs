using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network
{
    public class PacketFactory
    {
        public Packet BuildPacket(NetIncomingMessage message)
        {
            // Convert the first byte (the identifier to determine packet type) to a PacketTypeEnum
            PacketTypeEnum packetType = (PacketTypeEnum)Enum.ToObject(typeof(PacketTypeEnum), message.ReadByte());

            switch (packetType)
            {
                default:
                {
                    return null;
                }
            }

        }
    }
}
