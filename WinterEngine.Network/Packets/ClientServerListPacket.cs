using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    /// <summary>
    /// Sent From: Master Server
    /// Sent To: Client
    /// </summary>
    public class ClientServerListPacket : Packet
    {
        #region Fields

        private PacketTypeEnum _packetType = PacketTypeEnum.ClientServerList;
        private List<ServerDetails> _serverList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets this packet's type
        /// </summary>
        public override PacketTypeEnum PacketType
        {
            get { return _packetType; }
        }

        /// <summary>
        /// Gets or sets the server list.
        /// </summary>
        private List<ServerDetails> ServerList
        {
            get { return _serverList; }
            set { _serverList = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new empty ClientServerListPacket
        /// </summary>
        public ClientServerListPacket()
        {
        }

        /// <summary>
        /// Constructs a new ClientServerListPacket.
        /// </summary>
        /// <param name="serverList">The list of active client-server servers to send to the client.</param>
        public ClientServerListPacket(List<ServerDetails> serverList)
        {
            this.ServerList = ServerList;
        }

        #endregion

    }
}
