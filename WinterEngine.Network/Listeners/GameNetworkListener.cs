using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Lidgren.Network;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.Network.BusinessObjects;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Listeners
{
    public partial class GameNetworkListener
    {
        #region Fields

        private NetworkAgent _agent;
        private List<PacketBase> _incomingPackets;
        private FileExtensionFactory _fileExtensionFactory;
        private WinterServer _serverDetails;
        private Dictionary<NetConnection, string> _connectionUsernames;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the network agent.
        /// </summary>
        private NetworkAgent Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        /// <summary>
        /// Gets or sets the list of incoming packets that need to be processed by the listener.
        /// </summary>
        private List<PacketBase> IncomingPackets
        {
            get { return _incomingPackets; }
            set { _incomingPackets = value; }
        }

        /// <summary>
        /// The file extension factory used for file streaming.
        /// </summary>
        private FileExtensionFactory FileExtensionFactory
        {
            get { return _fileExtensionFactory; }
        }

        /// <summary>
        /// Gets or sets the server information object.
        /// </summary>
        private WinterServer ServerDetails
        {
            get { return _serverDetails; }
            set { _serverDetails = value; }
        }

        /// <summary>
        /// Gets or sets the dictionary containing the link between clients' NetConnections and their usernames.
        /// </summary>
        private Dictionary<NetConnection, string> ConnectionUsernamesDictionary
        {
            get 
            {
                if (_connectionUsernames == null)
                {
                    _connectionUsernames = new Dictionary<NetConnection, string>();
                }

                return _connectionUsernames; 
            }
            set { _connectionUsernames = value; }
        }

        /// <summary>
        /// Gets the list of usernames currently connected to the server.
        /// </summary>
        public List<string> ConnectedUsernames
        {
            get { return ConnectionUsernamesDictionary.Values.ToList(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new GameNetworkListener object and initializes all values.
        /// </summary>
        /// <param name="customPort">The port to run the listener on</param>
        /// <param name="contentPackages">The content packages to be streamed to users on connection.</param>
        public GameNetworkListener(int customPort, List<ContentPackage> contentPackages)
        {
            if (customPort <= 0)
            {
                customPort = GameServerConfiguration.DefaultGamePort;
            }

            Agent = new NetworkAgent(AgentRoleEnum.Server, GameServerConfiguration.ApplicationID, customPort);
            Agent.OnConnected += Agent_OnConnectionEstablished;
            Agent.OnDisconnected += Agent_OnDisconnected;
            this._contentPackages = contentPackages;

            _contentPackageNames = new List<string>();
            _contentPackagePaths = new List<string>();
            foreach (ContentPackage package in contentPackages)
            {
                _contentPackageNames.Add(package.FileName);
                _contentPackagePaths.Add(DirectoryPaths.ContentPackageDirectoryPath + package.FileName);
            }

            _fileExtensionFactory = new FileExtensionFactory();
            _fileTransferClients = new Dictionary<NetConnection, FileTransferProgress>();
        }


        #endregion

        #region Events / Delegates

        public event EventHandler<NetworkLogMessageEventArgs> OnLogMessage;
        
        #endregion

        #region Methods - General

        public void Shutdown()
        {
            Agent.Shutdown();
        }

        public void Process(WinterServer serverDetails)
        {
            ServerDetails = serverDetails;
            // Checks for messages and processes them.
            IncomingPackets = Agent.CheckForPackets();

            foreach (PacketBase packet in IncomingPackets)
            {
                ProcessPacket(packet);
            }

            StreamFilesToClients();

            Thread.Sleep(5);
        }

        /// <summary>
        /// Handles processing packets depending on their type.
        /// If a packet recieved does not match one of the supported types,
        /// it will be dropped.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="factory"></param>
        private void ProcessPacket(PacketBase packet)
        {
            Type packetType = packet.GetType();

            if (packetType == typeof(RequestPacket))
            {
                ProcessRequest(packet as RequestPacket);
            }
            else if (packetType == typeof(FileRequestPacket))
            {
                ProcessFileTransferRequest(packet as FileRequestPacket);
            }
            else if (packetType == typeof(UsernamePacket))
            {
                ProcessUsernamePacket(packet as UsernamePacket);
            }
            else if (packetType == typeof(DeleteCharacterPacket))
            {
                ProcessDeleteCharacterRequest(packet as DeleteCharacterPacket);
            }
        }

        private void RaiseOnLogMessageEvent(string message)
        {
            if (!Object.ReferenceEquals(OnLogMessage, null))
            {
                NetworkLogMessageEventArgs e = new NetworkLogMessageEventArgs { Message = message };
                OnLogMessage(this, e);
            }
        }

        private void Agent_OnConnectionEstablished(object sender, ConnectionStatusEventArgs e)
        {
            SendContentPackageList(e.Connection);
            RaiseOnLogMessageEvent("Connection established: " + e.Connection.RemoteEndPoint.Address + ":" + e.Connection.RemoteEndPoint.Port);

            // Send a request for the user's username.
            RequestPacket packet = new RequestPacket(RequestTypeEnum.Username);
            Agent.SendPacket(packet, e.Connection, NetDeliveryMethod.ReliableUnordered);
        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {
            ConnectionUsernamesDictionary.Remove(e.Connection);
        }

        private void ProcessUsernamePacket(UsernamePacket packet)
        {
            if(!ConnectionUsernamesDictionary.ContainsKey(packet.SenderConnection))
            {
                ConnectionUsernamesDictionary.Add(packet.SenderConnection, packet.Username);
            }
        }

        #endregion
    }
}
