using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Lidgren.Network;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.Network.BusinessObjects;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Listeners
{
    public class GameNetworkListener
    {
        #region Fields

        private NetworkAgent _agent;
        private List<ContentPackage> _contentPackages;
        private List<string> _contentPackageNames;
        private List<string> _contentPackagePaths;
        private List<PacketBase> _incomingPackets;
        private Dictionary<NetConnection, FileTransferProgress> _fileTransferClients;
        private FileExtensionFactory _fileExtensionFactory;

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
        /// Gets the list of content packages being streamed by the listener to clients.
        /// </summary>
        private List<ContentPackage> ContentPackageList
        {
            get { return _contentPackages; }
        }

        /// <summary>
        /// Gets the name of the content packages being streamed by the listener to clients.
        /// </summary>
        private List<string> ContentPackageFileNames
        {
            get { return _contentPackageNames; }
        }

        /// <summary>
        /// Gets the file paths of the content packages being streamed by the listener to clients.
        /// </summary>
        private List<string> ContentPackageFilePaths
        {
            get { return _contentPackagePaths; }
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
        /// Gets or sets the list of clients to which the listener is streaming files.
        /// </summary>
        private Dictionary<NetConnection, FileTransferProgress> FileTransferClients
        {
            get { return _fileTransferClients; }
            set { _fileTransferClients = value; }
        }

        /// <summary>
        /// The file extension factory used for file streaming.
        /// </summary>
        private FileExtensionFactory FileExtensionFactory
        {
            get { return _fileExtensionFactory; }
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

        public void Process()
        {
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
        }

        #endregion

        #region Methods - Request Processing

        /// <summary>
        /// Processes a request, sending data to sender if necessary.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessRequest(RequestPacket packet)
        {
            RaiseOnLogMessageEvent("Request Packet received: " + packet.RequestType);

            switch (packet.RequestType)
            {
                case RequestTypeEnum.Disconnect:
                    DisconnectClientFromServer(packet as RequestPacket);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sends a packet containing the list of file names to the sender of the received packet.
        /// </summary>
        /// <param name="receivedPacket"></param>
        private void SendContentPackageList(NetConnection connection)
        {
            ContentPackageListPacket packet = new ContentPackageListPacket
            {
                FileNames = ContentPackageFileNames
            };
            Agent.WriteMessage(packet);
            Agent.SendMessage(connection, NetDeliveryMethod.ReliableSequenced);

        }

        private void DisconnectClientFromServer(RequestPacket packet)
        {
            RaiseOnLogMessageEvent("Beginning to disconnect client: " 
                + packet.SenderConnection.RemoteEndPoint.Address + ":"  
                + packet.SenderConnection.RemoteEndPoint.Port);

            FileTransferClients.Remove(packet.SenderConnection);
            NetConnection connection = Agent.Connections.FirstOrDefault(x => x.RemoteEndPoint == packet.SenderConnection.RemoteEndPoint);

            if (!Object.ReferenceEquals(connection, null))
            {
                connection.Disconnect("Disconnecting from client");
            }

            RaiseOnLogMessageEvent("Disconnected client: "
                + packet.SenderConnection.RemoteEndPoint.Address + ":"
                + packet.SenderConnection.RemoteEndPoint.Port);
        }

        #endregion

        #region Methods - File transfer processing

        /// <summary>
        /// Receives a file request packet and either starts a file transfer or cancels an existing one.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessFileTransferRequest(FileRequestPacket packet)
        {
            if (packet.FileRequestType == FileRequestTypeEnum.StartFileRequest)
            {
                string serverFilePath = DirectoryPaths.ContentPackageDirectoryPath + packet.FileName;

                // Safety check - make sure the file exists and it has an appropriate extension.
                if (Path.GetExtension(packet.FileName) != FileExtensionFactory.GetFileExtension(FileTypeEnum.ContentPackage) ||
                    !File.Exists(serverFilePath))
                {
                    return; // EARLY EXIT
                }

                // Build a new client and add them to the list.
                FileTransferProgress client = new FileTransferProgress
                {
                    BytesSent = 0,
                    FilePath = serverFilePath
                };

                FileTransferClients.Add(packet.SenderConnection, client);

                // Send the size of the file back to client so they can track the download's progress.
                StreamingFileDetailsPacket fileDetails = new StreamingFileDetailsPacket
                {
                    FileSize = new FileInfo(serverFilePath).Length
                };

                Agent.WriteMessage(fileDetails);
                Agent.SendMessage(packet.SenderConnection, NetDeliveryMethod.ReliableOrdered);

            }
            else if (packet.FileRequestType == FileRequestTypeEnum.CancelFileRequest)
            {
                // Remove a client from the streaming list.
                FileTransferClients.Remove(packet.SenderConnection);
            }
        }

        /// <summary>
        /// Handles streaming the next segment of files to users downloading content packages.
        /// </summary>
        private void StreamFilesToClients()
        {
            List<NetConnection> connectionList = FileTransferClients.Keys.ToList();

            foreach (NetConnection currentConnection in connectionList)
            {
                FileTransferProgress clientProgress = FileTransferClients[currentConnection];

                if (File.Exists(clientProgress.FilePath))
                {
                    using (FileStream fileStream = new FileStream(clientProgress.FilePath, FileMode.Open))
                    {
                        bool isEndOfFile = false;
                        int numberOfBytesToSend = GameServerConfiguration.FileTransferBufferSize;
                        int numberOfBytesRemaining = (int)fileStream.Length - clientProgress.BytesSent;

                        if (numberOfBytesRemaining < GameServerConfiguration.FileTransferBufferSize)
                        {
                            numberOfBytesToSend = numberOfBytesRemaining;
                            isEndOfFile = true;
                        }

                        fileStream.Position = clientProgress.BytesSent;
                        byte[] bytesToSend = new byte[numberOfBytesToSend];
                        fileStream.Read(bytesToSend, 0, numberOfBytesToSend);

                        StreamingFilePacket packet = new StreamingFilePacket
                        {
                            FileName = Path.GetFileName(clientProgress.FilePath),
                            FileBytes = bytesToSend,
                            IsEndOfFile = isEndOfFile
                        };
                        Agent.WriteMessage(packet);
                        Agent.SendMessage(currentConnection, NetDeliveryMethod.ReliableOrdered);

                        clientProgress.BytesSent += numberOfBytesToSend;

                        // Remove client from streaming list if this was the last part of the file to be sent
                        if (clientProgress.BytesSent >= (int)fileStream.Length)
                        {
                            FileTransferClients.Remove(currentConnection);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
