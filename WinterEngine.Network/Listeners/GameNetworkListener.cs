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
using WinterEngine.Network.Configuration;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.Packets;
using WinterEngine.DataTransferObjects.Models;

namespace WinterEngine.Network.Listeners
{
    public partial class GameNetworkListener
    {
        #region Fields

        private Dictionary<NetConnection, string> _connectionUsernames;

        #endregion

        #region Properties

        private NetworkAgent Agent { get; set; }
        private List<PacketBase> IncomingPackets { get; set; }
        private FileExtensionFactory FileExtensionFactory { get; set; }
        private GameNetworkListenerModel Model { get; set; }
        
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

            Agent = new NetworkAgent(NetworkAgentRoleEnum.Server, GameServerConfiguration.ApplicationID, customPort);
            Agent.OnConnected += Agent_OnConnectionEstablished;
            Agent.OnDisconnected += Agent_OnDisconnected;
            this._contentPackages = contentPackages;

            _contentPackageNames = new List<string>();
            _contentPackagePaths = new List<string>();

            if (contentPackages != null)
            {
                foreach (ContentPackage package in contentPackages)
                {
                    _contentPackageNames.Add(package.FileName);
                    _contentPackagePaths.Add(DirectoryPaths.ContentPackageDirectoryPath + package.FileName);
                }
            }

            FileExtensionFactory = new FileExtensionFactory();
            _fileTransferClients = new Dictionary<NetConnection, FileTransferProgress>();
            Model = new GameNetworkListenerModel();
        }


        #endregion

        #region Events / Delegates

        public event EventHandler<GameNetworkListenerProcessEventArgs> OnProcessingCycleComplete;
        
        #endregion

        #region Methods - General

        public void Shutdown()
        {
            Agent.Shutdown();
        }

        public void Process()
        {
            // Checks for messages and processes them.
            IncomingPackets = Agent.CheckForPackets();

            foreach (PacketBase packet in IncomingPackets)
            {
                ProcessPacket(packet);
            }

            StreamFilesToClients();
            SendServerMessage();
            BootUsers();

            RaiseOnProcessingCycleCompleteEvent();
            CleanUpCycleData();
        }

        public void ProcessCycleBegin(object sender, GameNetworkListenerProcessEventArgs e)
        {
            Model.BannedUsersList = e.BanUserList;
            Model.QueuedBootUsersList = e.BootUserList;
            Model.QueuedServerMessage = e.ServerMessage;
            Model.ServerAnnouncement = e.ServerAnnouncement;
        }

        private void CleanUpCycleData()
        {
            Model.LogMessages.Clear();
            Model.QueuedBootUsersList.Clear();
            Model.QueuedServerMessage = "";
        }

        private void SendServerMessage()
        {
            if (!string.IsNullOrWhiteSpace(Model.QueuedServerMessage))
            {
                ServerMessagePacket packet = new ServerMessagePacket(Model.QueuedServerMessage);

                foreach (NetConnection user in Agent.Connections)
                {
                    Agent.SendPacket(packet, user, NetDeliveryMethod.ReliableUnordered);
                }

                Model.QueuedServerMessage = string.Empty;
            }
        }
        
        private void BootUsers()
        {
            // Any users logged in that are on the ban list will automatically be booted.
            Model.QueuedBootUsersList.AddRange((from username
                                                in Model.BannedUsersList
                                                where Model.ConnectionUsernamesDictionary.ContainsValue(username)
                                                select username).ToList());

            if (Model.QueuedBootUsersList != null && Model.QueuedBootUsersList.Count > 0)
            {
                foreach (string userName in Model.QueuedBootUsersList)
                {
                    NetConnection connection = Model.ConnectionUsernamesDictionary.SingleOrDefault(x => x.Value == userName).Key;
                    if (connection != null)
                    {
                        connection.Disconnect("You have been booted.");
                    }
                }
            }
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

        private void RaiseOnProcessingCycleCompleteEvent()
        {
            if (OnProcessingCycleComplete != null)
            {
                GameNetworkListenerProcessEventArgs e = new GameNetworkListenerProcessEventArgs
                {
                    PlayerList = Model.ConnectedUsernames,
                    LogMessages = Model.LogMessages
                };

                OnProcessingCycleComplete(this, e);
            }
        }

        private void Agent_OnConnectionEstablished(object sender, ConnectionStatusEventArgs e)
        {
            SendContentPackageList(e.Connection);
            Model.LogMessages.Add("Connection established: " + e.Connection.RemoteEndPoint.Address + ":" + e.Connection.RemoteEndPoint.Port);

            // TODO: Check master server for player's user name, based on IP address

            // Send a request for the user's username.
            RequestPacket packet = new RequestPacket(PacketRequestTypeEnum.Username);
            Agent.SendPacket(packet, e.Connection, NetDeliveryMethod.ReliableUnordered);
        }

        private void Agent_OnDisconnected(object sender, ConnectionStatusEventArgs e)
        {
            Model.ConnectionUsernamesDictionary.Remove(e.Connection);
        }

        private void ProcessUsernamePacket(UsernamePacket packet)
        {
            if (!Model.ConnectionUsernamesDictionary.ContainsKey(packet.SenderConnection))
            {
                Model.ConnectionUsernamesDictionary.Add(packet.SenderConnection, packet.Username);
            }
        }

        #endregion
    }
}
