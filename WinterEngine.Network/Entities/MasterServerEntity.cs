using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading;
using Lidgren.Network;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;
using System.Linq;

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
        /// Returns the master server's IP Address.
        /// </summary>
        public IPAddress MasterServerIPAddress 
        { 
            get 
            { 
                return _masterServerIPAddress; 
            } 
        }

        /// <summary>
        /// Returns the master server's port number.
        /// </summary>
        public int MasterServerPort 
        {
            get
            {
                return _masterServerPort;
            }
        }

        /// <summary>
        /// Returns the master server's unique Application ID.
        /// </summary>
        public string MasterServerAppID 
        {
            get
            {
                return _masterServerAppID;
            }
        }

        /// <summary>
        /// Gets or sets the number of minutes before servers time out due to inactivity.
        /// </summary>
        public int ServerTimeoutMinutes
        {
            get
            {
                return _serverTimeoutMinutes;
            }
        }

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
        /// <param name="url">The URL of the master server.</param>
        /// <param name="port">The port used by the master server</param>
        /// <param name="appID">The unique Application ID used for client connections.</param>
        /// <param name="serverTimeoutMinutes">The number of minutes before a connected server times out.</param>
        public MasterServerEntity(IPAddress ipAddress, int port, string appID, int serverTimeoutMinutes)
        {
            _masterServerIPAddress = ipAddress;
            _masterServerPort = port;
            _masterServerAppID = appID;
            _serverTimeoutMinutes = serverTimeoutMinutes;

            ServerList = new Dictionary<ConnectionAddress, ServerDetails>();
            NetworkThread = new BackgroundWorker();
            Agent = new NetworkAgent(AgentRole.Server, appID);

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
            while (IsMasterServerRunning)
            {
                List<NetIncomingMessage> messages;
                PacketFactory factory = new PacketFactory();

                try
                {
                    messages = Agent.CheckForMessages();
                    foreach (NetIncomingMessage message in messages)
                    {
                        ProcessPacket(message, factory);
                    }
                    messages.Clear();

                    //RemoveTimedOutServers();
                    Thread.Sleep(5);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
                    int port = message.SenderEndPoint.Port;
                    IPAddress ipAddress = message.SenderEndPoint.Address;
                    float ping = message.SenderConnection.AverageRoundtripTime;
                    UpdateServerInformation(packet as ServerDetailsPacket, ipAddress, port, ping);
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
        private void UpdateServerInformation(ServerDetailsPacket packet, IPAddress ipAddress, int port, float ping)
        {
            ServerDetails details = new ServerDetails();
            details.Description = packet.Description;
            details.MaxLevel = packet.MaxLevel;
            details.MinLevel = packet.MinLevel;
            details.Name = packet.Name;
            details.Connection.IP = ipAddress;
            details.Connection.Port = port;
            details.Ping = ping;

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
            if (ServerList.ContainsKey(key))
            {
                ServerList[key] = details;
            }
            else
            {
                ServerList.Add(key, details);
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

            foreach (KeyValuePair<ConnectionAddress, ServerDetails> server in ServerList)
            {
                TimeSpan difference = currentTime.Subtract(server.Value.LastPacketReceived);
                if (difference.Minutes >= ServerTimeoutMinutes)
                {
                    ServerList.Remove(server.Key);
                }
            }

        }

        #endregion
    }
}
