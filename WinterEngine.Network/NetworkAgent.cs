using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lidgren.Network;
using ProtoBuf;
using WinterEngine.Network.BusinessObjects;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network
{
    public class NetworkAgent
    {
        #region Fields

        private NetPeer _peer;
        private NetPeerConfiguration _configuration;
        private AgentRoleEnum _role;
        private int _port;
        private NetOutgoingMessage _outgoingMessage;
        private List<NetIncomingMessage> _incomingMessages;
        private INetEncryption _encryption;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a list of connections in use by the agent.
        /// </summary>
        public List<NetConnection> Connections
        {
            get
            {
                return _peer.Connections;
            }
        }

        public NetPeerStatus Status
        {
            get
            {
                return _peer.Status;
            }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private INetEncryption Encryption
        {
            get { return _encryption; }
            set { _encryption = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Customize appIdentifier. Note: Client and server appIdentifier must be the same.
        /// </summary>
        public NetworkAgent(AgentRoleEnum role, string tag, int customPort)
        {
            _role = role;
            _configuration = new NetPeerConfiguration(tag);
            _port = customPort;

            Initialize();
        }

        #endregion

        #region Events / Delegates

        /// <summary>
        /// Event hook which fires after a connection has been established.
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> OnConnected;
        /// <summary>
        /// Event hook which fires after a client has begun disconnecting.
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> OnDisconnecting;
        /// <summary>
        /// Event hook which fires after a client has disconnected.
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> OnDisconnected;

        #endregion

        #region Methods

        private void Initialize()
        {
            Encryption = new NetXtea(GameServerConfiguration.EncryptionKey);

            if (_role == AgentRoleEnum.Server)
            {
                _configuration.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
                _configuration.Port = _port;
                
                //Casts the NetPeer to a NetServer
                _peer = new NetServer(_configuration);
            }

            else if (_role == AgentRoleEnum.Client)
            {
                _configuration.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
                //Casts the NetPeer to a NetClient
                _peer = new NetClient(_configuration);
            }

            _peer.Start();
            
            _incomingMessages = new List<NetIncomingMessage>();
            _outgoingMessage = _peer.CreateMessage();
        }

        /// <summary>
        /// Connects to a server. Throws an exception if you attempt to call Connect as a Server.
        /// </summary>
        public void Connect(string ip)
        {
            if (_role == AgentRoleEnum.Client)
            {
                _peer.Connect(ip, _port);
            }
            else
            {
                throw new SystemException("Attempted to connect as server. Only clients should connect.");
            }
        }

        /// <summary>
        /// Disconnects from all connections
        /// </summary>
        public void Disconnect()
        {
            foreach (NetConnection connection in Connections)
            {
                connection.Disconnect("Disconnecting");
            }
        }

        public void Shutdown()
        {
            _peer.Shutdown("Shutting down server");
        }

        /// <summary>
        /// Writes a packet's properties to an outgoing message.
        /// </summary>
        /// <param name="packet"></param>
        public void WriteMessage(PacketBase packet)
        {
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize<PacketBase>(stream, packet); // Protobuf serialization
            _outgoingMessage.Write(stream.ToArray());
        }

        /// <summary>
        /// Sends the outgoing message and clears it for the next send.
        /// </summary>
        /// <param name="recipient">The recipient of the packet</param>
        /// <param name="method">The delivery method</param>
        public void SendMessage(NetConnection recipient, NetDeliveryMethod method)
        {
            if (!Object.ReferenceEquals(recipient, null))
            {
                _outgoingMessage.Encrypt(Encryption);

                _peer.SendMessage(_outgoingMessage, recipient, method);
                _outgoingMessage = _peer.CreateMessage();
            }
        }

        /// <summary>
        /// Reads every message in the queue and returns a list of data messages.
        /// Other message types just write a Console note.
        /// </summary>
        /// <returns></returns>
        private List<NetIncomingMessage> CheckForMessages()
        {
            _incomingMessages.Clear();
            NetIncomingMessage incomingMessage;

            while ((incomingMessage = _peer.ReadMessage()) != null)
            {
                switch (incomingMessage.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        _peer.SendDiscoveryResponse(null, incomingMessage.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                    {

                        NetConnectionStatus status = (NetConnectionStatus)incomingMessage.ReadByte();
                        ConnectionStatusEventArgs e = new ConnectionStatusEventArgs 
                        { 
                            Connection = incomingMessage.SenderConnection 
                        };

                        if (status == NetConnectionStatus.Connected)
                        {
                            if (!Object.ReferenceEquals(OnConnected, null))
                            {
                                OnConnected(this, e);
                            }
                        }
                        else if (status == NetConnectionStatus.Disconnecting)
                        {
                            if (!Object.ReferenceEquals(OnDisconnecting, null))
                            {
                                OnDisconnecting(this, e);
                            }
                        }
                        else if (status == NetConnectionStatus.Disconnected)
                        {
                            if (!Object.ReferenceEquals(OnDisconnected, null))
                            {
                                OnDisconnected(this, e);
                            }
                        }


                        break;
                    }
                    case NetIncomingMessageType.Data:
                        incomingMessage.Decrypt(Encryption);
                        _incomingMessages.Add(incomingMessage);
                        break;
                    default:
                        // unknown message type
                        break;
                }
            }
            return _incomingMessages;
        }

        /// <summary>
        /// Checks for queued messages and deserializes them into packets.
        /// </summary>
        /// <returns></returns>
        public List<PacketBase> CheckForPackets()
        {
            List<NetIncomingMessage> messages = CheckForMessages();
            List<PacketBase> packets = new List<PacketBase>();
            MemoryStream stream = new MemoryStream();
            PacketBase currentPacket;

            foreach (NetIncomingMessage currentMessage in messages)
            {
                stream = new MemoryStream(currentMessage.ReadBytes(currentMessage.LengthBytes));
                currentPacket = Serializer.Deserialize<PacketBase>(stream); // Protobuf deserialization
                currentPacket.SenderConnection = currentMessage.SenderConnection;

                packets.Add(currentPacket);
            }

            return packets;
        }
        #endregion

    }
}
