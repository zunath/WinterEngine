﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;
using Lidgren.Network;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.Network.Configuration;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.Packets;
using WinterEngine.DataTransferObjects.EventArgsExtended;

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
                if (ConnectionStatus == NetConnectionStatus.Connected)
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
        /// Returns the connection status of the client's connection to the server.
        /// Returns NetConnectionStatus.None if no connections have been established or
        /// NetworkAgent has not been initialized.
        /// </summary>
        public NetConnectionStatus ConnectionStatus
        {
            get
            {
                if (Agent != null && Agent.Connections.Count > 0)
                {
                    return Agent.Connections[0].Status;
                }

                return NetConnectionStatus.None;
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

        #region Events / Delegates

        public event EventHandler<PacketReceivedEventArgs> OnPacketReceived;
        public event EventHandler<ConnectionStatusEventArgs> OnConnected;
        public event EventHandler<ConnectionStatusEventArgs> OnDisconnected;
        public event EventHandler<ConnectionStatusEventArgs> OnDisconnecting;

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

                if (!Object.ReferenceEquals(OnPacketReceived, null))
                {
                    foreach (PacketBase packet in IncomingPackets)
                    {
                        OnPacketReceived(this, new PacketReceivedEventArgs(packet));
                    }
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
                Agent = new NetworkAgent(NetworkAgentRoleEnum.Client, GameServerConfiguration.ApplicationID, address.Port);


                // Refresh event hooks
                Agent.OnConnected -= RaiseOnAgentConnectedEvent;
                Agent.OnDisconnected -= RaiseAgentOnDisconnectedEvent;
                Agent.OnDisconnecting -= RaiseAgentOnDisconnectingEvent;
                Agent.OnConnected += RaiseOnAgentConnectedEvent;
                Agent.OnDisconnected += RaiseAgentOnDisconnectedEvent;
                Agent.OnDisconnecting += RaiseAgentOnDisconnectingEvent;
            }
            else
            {
                Agent.Port = address.Port;
            }

            Agent.Connect(address.IPAddress);
        }

        private void RaiseAgentOnDisconnectingEvent(object sender, ConnectionStatusEventArgs e)
        {
            if (OnDisconnecting != null)
            {
                OnDisconnecting(this, e);
            }
        }

        private void RaiseAgentOnDisconnectedEvent(object sender, ConnectionStatusEventArgs e)
        {
            if (OnDisconnected != null)
            {
                OnDisconnected(this, e);
            }
        }

        private void RaiseOnAgentConnectedEvent(object sender, ConnectionStatusEventArgs e)
        {
            if (OnConnected != null)
            {
                OnConnected(this, e);
            }
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public void Disconnect()
        {
            Agent.Disconnect();
        }

        /// <summary>
        /// Sends a request packet to the currently connected server.
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="deliveryMethod"></param>
        public void SendRequest(PacketRequestTypeEnum requestType, NetDeliveryMethod deliveryMethod = NetDeliveryMethod.ReliableUnordered)
        {
            RequestPacket packet = new RequestPacket(requestType);
            Agent.SendPacket(packet, ServerConnection, deliveryMethod);
        }

        /// <summary>
        /// Sends a packet to the currently connected server.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="deliveryMethod"></param>
        public void SendPacket(PacketBase packet, NetDeliveryMethod deliveryMethod)
        {
            Agent.SendPacket(packet, ServerConnection, deliveryMethod);
        }

        #endregion

    }
}
