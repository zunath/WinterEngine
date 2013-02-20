using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading;
using Lidgren.Network;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;
using System.Linq;
using WinterEngine.Network.Configuration;

namespace WinterEngine.Network.Entities
{
    public class MasterServerEntity
    {
        #region Fields

        private IPAddress _masterServerIPAddress;
        private int _masterServerPort;
        private string _masterServerAppID;
        private int _serverTimeoutMinutes;
        private Dictionary<ConnectionAddress, ServerDetails> _serverList;

        #endregion

        #region Properties

        /// <summary>
        /// Network thread handles receiving packets and processing them
        /// </summary>
        private BackgroundWorker NetworkThread { get; set; }

        /// <summary>
        /// Gets or sets the network agent used for connection handling.
        /// </summary>
        private NetworkAgent Agent { get; set; }

        /// <summary>
        /// Gets or sets whether the master server is currently running.
        /// </summary>
        private bool IsMasterServerRunning { get; set; }

        /// <summary>
        /// Gets or sets the list of active servers currently connected to the master server.
        /// </summary>
        private Dictionary<ConnectionAddress, ServerDetails> ServerList
        {
            get
            {
                return _serverList;
            }
            set
            {
                _serverList = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Builds a new master server object.
        /// </summary>
        public MasterServerEntity()
        {
            ServerList = new Dictionary<ConnectionAddress, ServerDetails>();
            NetworkThread = new BackgroundWorker();
            NetworkThread.WorkerSupportsCancellation = true;

            Agent = new NetworkAgent(AgentRole.Server, MasterServerConfiguration.MasterServerApplicationIdentifier);

            NetworkThread.DoWork += (sender, e) =>
            {
                CheckForMessages();
            };
        }

        #endregion

        #region Methods - Main Thread

        /// <summary>
        /// Starts the master server instance.
        /// </summary>
        public void StartMasterServer()
        {
            IsMasterServerRunning = true;
            NetworkThread.RunWorkerAsync();
        }

        /// <summary>
        /// Shuts down the master server instance.
        /// </summary>
        public void ShutdownMasterServer()
        {
            IsMasterServerRunning = false;
            NetworkThread.CancelAsync();
        }

        public List<ServerDetails> GetServerList()
        {
            return _serverList.Values.ToList();
        }

        #endregion

        #region Methods - Network Thread

        /// <summary>
        /// Checks for messages 
        /// </summary>
        private void CheckForMessages()
        {
            try
            {
                while (IsMasterServerRunning)
                {
                    List<NetIncomingMessage> messages;
                    PacketFactory factory = new PacketFactory();

                    messages = Agent.CheckForMessages();
                    foreach (NetIncomingMessage message in messages)
                    {
                        ProcessPacket(message, factory);
                    }
                    messages.Clear();

                    RemoveTimedOutServers();
                    Thread.Sleep(5);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Handles processing packets depending on their type.
        /// If a packet received does not match one of the supported types,
        /// it will be dropped.
        /// </summary>
        /// <param name="message"></param>
        private void ProcessPacket(NetIncomingMessage message, PacketFactory factory)
        {
            Packet packet = factory.BuildPacket(message);

            switch (packet.PacketType)
            {
                case PacketTypeEnum.Server:
                    UpdateServerInformation(packet as ServerDetailsPacket, message);
                    break;
                case PacketTypeEnum.Request:
                    ProcessRequest(packet as RequestPacket);
                    break;
                default:
                    // Invalid packet type.
                    break;
            }
        }

        /// <summary>
        /// Adds or updates a server object in the list box
        /// based on data received over the network.
        /// </summary>
        /// <param name="packet"></param>
        private void UpdateServerInformation(ServerDetailsPacket packet, NetIncomingMessage message)
        {
            int port = message.SenderEndPoint.Port;
            IPAddress ipAddress = message.SenderEndPoint.Address;
            float ping = message.SenderConnection.AverageRoundtripTime;

            ServerDetails details = new ServerDetails();
            details.Description = packet.Description;
            details.MaxLevel = packet.MaxLevel;
            details.MinLevel = packet.MinLevel;
            details.Name = packet.Name;
            details.Connection.IP = ipAddress;
            details.Connection.Port = port;
            details.Ping = ping;
            details.LastPacketReceived = DateTime.Now;
            
            UpsertServer(details.Connection, details);
        }

        /// <summary>
        /// Adds a server's details to the server list if it doesn't exist already.
        /// Updates a server's details if it already exists in the server list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="details"></param>
        private void UpsertServer(ConnectionAddress key, ServerDetails details)
        {
            if (!ServerList.ContainsKey(key))
            {
                ServerList.Add(key, details);
            }
            else
            {
                ServerList[key] = details;
            }
        }

        /// <summary>
        /// Processes a request, sending data to sender if necessary.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessRequest(RequestPacket packet)
        {
            switch (packet.RequestType)
            {
                case RequestTypeEnum.MasterServerList:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Removes servers from the server list if they have not been updated in
        /// a specified amount of time. (Default: 5 minutes)
        /// </summary>
        private void RemoveTimedOutServers()
        {
            DateTime currentTime = DateTime.Now;
            List<ConnectionAddress> keys = new List<ConnectionAddress>();

            // Determine which keys to remove from the server list.
            foreach (KeyValuePair<ConnectionAddress, ServerDetails> server in ServerList)
            {
                TimeSpan difference = currentTime.Subtract(server.Value.LastPacketReceived);
                if (difference.Minutes >= MasterServerConfiguration.ServerTimeoutMinutes)
                {
                    keys.Add(server.Key);
                }
            }

            // Actually remove the keys from the server list.
            foreach (ConnectionAddress currentKey in keys)
            {
                ServerList.Remove(currentKey);
            }

        }

        #endregion
    }
}
