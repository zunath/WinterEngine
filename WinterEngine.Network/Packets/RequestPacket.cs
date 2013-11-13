﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    [ProtoContract]
    public class RequestPacket : PacketBase
    {
        #region Fields
        private RequestTypeEnum _requestType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the request type of this packet.
        /// </summary>
        [ProtoMember(1)]
        public RequestTypeEnum RequestType 
        {
            get
            {
                return _requestType;
            }
            set
            {
                _requestType = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new request packet with a null request type.
        /// </summary>
        public RequestPacket()
        {
        }

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
