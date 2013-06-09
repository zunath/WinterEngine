using System;
using System.Collections.Generic;
using System.Threading;
using Lidgren.Network;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Clients
{
    public partial class GameNetworkClient
    {
        #region Fields

        private NetworkAgent _agent;
        private List<PacketBase> _incomingPackets;
        private FileExtensionFactory _fileExtensionFactory;
        private string _username;

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the network agent which is used to send and receive packets to/from the server.
        /// </summary>
        private NetworkAgent Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }
        
        /// <summary>
        /// Gets or sets the list of packets which are waiting to be processed by the client.
        /// </summary>
        private List<PacketBase> IncomingPackets
        {
            get { return _incomingPackets; }
            set { _incomingPackets = value; }
        }

        /// <summary>
        /// Returns true if the network agent is connected.
        /// Returns false if the network agent is not connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (Agent != null && Agent.Connections.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns the connection to the server.
        /// </summary>
        private NetConnection ServerConnection
        {
            get
            {
                if (IsConnected)
                {
                    return Agent.Connections[0];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the factory used for file extension checking.
        /// </summary>
        private FileExtensionFactory FileExtensionFactory
        {
            get 
            {
                if (_fileExtensionFactory == null)
                {
                    _fileExtensionFactory = new FileExtensionFactory();
                }

                return _fileExtensionFactory; 
            }
        }

        /// <summary>
        /// Returns the status of the server connection.
        /// </summary>
        public NetConnectionStatus Status
        {
            get
            {
                if (Object.ReferenceEquals(ServerConnection, null))
                {
                    return NetConnectionStatus.None;
                }
                else
                {
                    return ServerConnection.Status;
                }
            }
        }

        /// <summary>
        /// Gets or sets the username of the client.
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new GameNetworkClient.
        /// </summary>
        /// <param name="address"></param>
        public GameNetworkClient()
        {
        }

        #endregion

        #region Methods - General

        /// <summary>
        /// Checks for new incoming packets and processes them.
        /// </summary>
        public void Process()
        {
            if (!Object.ReferenceEquals(Agent, null))
            {
                IncomingPackets = Agent.CheckForPackets();

                foreach (PacketBase packet in IncomingPackets)
                {
                    ProcessPacket(packet);
                }
            }
        }

        /// <summary>
        /// Connects the game network client to the specified address.
        /// </summary>
        /// <param name="address"></param>
        public void Connect(ConnectionAddress address)
        {
            if (Agent == null)
            {
                Agent = new NetworkAgent(AgentRoleEnum.Client, GameServerConfiguration.ApplicationID, address.ServerPort);
            }
            else
            {
                Agent.Port = address.ServerPort;
            }

            Agent.Connect(address.ServerIPAddress);
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public void Disconnect()
        {
            if (IsConnected)
            {
                Agent.Disconnect();
            }
        }

        /// <summary>
        /// Handles processing each individual packet based on its type.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessPacket(PacketBase packet)
        {
            Type packetType = packet.GetType();

            if (packetType == typeof(StreamingFilePacket))
            {
                ProcessStreamingFilePacket(packet as StreamingFilePacket);
            }
            else if (packetType == typeof(ContentPackageListPacket))
            {
                ProcessContentPackageListPacket(packet as ContentPackageListPacket);
            }
            else if (packetType == typeof(StreamingFileDetailsPacket))
            {
                ProcessStreamingFileDetailsPacket(packet as StreamingFileDetailsPacket);
            }
            else if (packetType == typeof(CharacterSelectionPacket))
            {
                ProcessCharacterSelectionPacket(packet as CharacterSelectionPacket);
            }
            else if (packetType == typeof(RequestPacket))
            {
                ProcessRequest(packet as RequestPacket);
            }
        }

        /// <summary>
        /// Sends a request packet to the currently connected server.
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="deliveryMethod"></param>
        public void SendRequest(RequestTypeEnum requestType)
        {
            RequestPacket packet = new RequestPacket(requestType);

            Agent.WriteMessage(packet);
            Agent.SendMessage(ServerConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion

    }
}
