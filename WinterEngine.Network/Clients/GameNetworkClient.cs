using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Lidgren.Network;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Paths;
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
                if (Agent.Connections.Count > 0)
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


        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new GameNetworkClient and connects to the specified network address.
        /// </summary>
        /// <param name="address"></param>
        public GameNetworkClient(ConnectionAddress address)
        {
            Agent = new NetworkAgent(AgentRoleEnum.Client, GameServerConfiguration.ApplicationID, address.ServerPort);
            Agent.Connect(address.ServerIPAddress);
        }

        #endregion

        #region Methods - General

        /// <summary>
        /// Checks for new incoming packets and processes them.
        /// </summary>
        public void Process()
        {
            IncomingPackets = Agent.CheckForPackets();

            foreach (PacketBase packet in IncomingPackets)
            {
                ProcessPacket(packet);
            }

            Thread.Sleep(5);
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public void Disconnect()
        {
            Agent.Shutdown();
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
        }

        #endregion

    }
}
