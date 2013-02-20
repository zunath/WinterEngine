using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    /// <summary>
    /// Packet used to request data from the server or master server.
    /// </summary>
    public class RequestPacket : Packet
    {
        #region Fields
        private PacketTypeEnum _packetType = PacketTypeEnum.Request;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the request type of this packet.
        /// </summary>
        public RequestTypeEnum RequestType { get; set; }

        /// <summary>
        /// Gets or sets this packet's type
        /// </summary>
        public override PacketTypeEnum PacketType
        {
            get { return _packetType; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new request packet.
        /// </summary>
        /// <param name="requestType">The type of request to send.</param>
        public RequestPacket(RequestTypeEnum requestType)
        {
            this.RequestType = requestType;
        }

        #endregion

    }
}
