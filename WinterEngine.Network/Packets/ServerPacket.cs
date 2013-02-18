﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    /// <summary>
    /// Sent From: Server
    /// Sent To: Master Server
    /// </summary>
    public class ServerPacket : Packet
    {
        #region Fields

        private PacketTypeEnum _packetType = PacketTypeEnum.Server;

        #endregion

        #region Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }

        public override PacketTypeEnum PacketType
        {
            get { return _packetType; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new empty server packet.
        /// </summary>
        public ServerPacket()
        {
        }

        /// <summary>
        /// Constructs a new server packet.
        /// </summary>
        /// <param name="name">The name of the server</param>
        /// <param name="description">The description of the server</param>
        /// <param name="minimumLevel">The minimum character level of the server</param>
        /// <param name="maximumLevel">The maximum character level of the server</param>
        public ServerPacket(string name, string description, byte minimumLevel, byte maximumLevel)
        {
            this.Name = name;
            this.Description = description;
            this.MinLevel = minimumLevel;
            this.MaxLevel = maximumLevel;
        }

        #endregion

    }
}
