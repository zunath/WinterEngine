using System;
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
    public class ServerDetailsPacket : Packet
    {
        #region Fields

        private PacketTypeEnum _packetType = PacketTypeEnum.Server;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description of the server.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the minimum level of the server.
        /// </summary>
        public byte MinLevel { get; set; }
        /// <summary>
        /// Gets or sets the maximum level of the server.
        /// </summary>
        public byte MaxLevel { get; set; }

        /// <summary>
        /// Gets or sets the this packet's type
        /// </summary>
        public override PacketTypeEnum PacketType
        {
            get { return _packetType; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new empty server packet.
        /// </summary>
        public ServerDetailsPacket()
        {
        }

        /// <summary>
        /// Constructs a new server packet.
        /// </summary>
        /// <param name="name">The name of the server</param>
        /// <param name="description">The description of the server</param>
        /// <param name="minimumLevel">The minimum character level of the server</param>
        /// <param name="maximumLevel">The maximum character level of the server</param>
        public ServerDetailsPacket(string name, string description, byte minimumLevel, byte maximumLevel)
        {
            this.Name = name;
            this.Description = description;
            this.MinLevel = minimumLevel;
            this.MaxLevel = maximumLevel;
        }

        #endregion

    }
}
