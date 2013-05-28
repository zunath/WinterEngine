using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Lidgren.Network;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.Network.BusinessObjects;
using WinterEngine.Network.Clients;
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

        private List<ContentPackage> ContentPackageList
        {
            get { return _contentPackages; }
        }

        private List<string> ContentPackageFileNames
        {
            get { return _contentPackageNames; }
        }

        private List<string> ContentPackageFilePaths
        {
            get { return _contentPackagePaths; }
        }

        private List<PacketBase> IncomingPackets
        {
            get { return _incomingPackets; }
            set { _incomingPackets = value; }
        }

        private Dictionary<NetConnection, FileTransferProgress> FileTransferClients
        {
            get { return _fileTransferClients; }
            set { _fileTransferClients = value; }
        }

        private FileExtensionFactory FileExtensionFactory
        {
            get { return _fileExtensionFactory; }
        }

        #endregion

        #region Constructors

        public GameNetworkListener(int customPort, List<ContentPackage> contentPackages)
        {
            if (customPort <= 0)
            {
                customPort = GameServerConfiguration.DefaultGamePort;
            }

            Agent = new NetworkAgent(AgentRoleEnum.Server, GameServerConfiguration.ApplicationID, customPort);
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
                ProcessFileTransfer(packet as FileRequestPacket);
            }
        }


        #endregion

        #region Methods - Request Processing

        /// <summary>
        /// Processes a request, sending data to sender if necessary.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessRequest(RequestPacket packet)
        {
            switch (packet.RequestType)
            {
                case RequestTypeEnum.ServerContentPackageList:
                    SendContentPackageList(packet);
                    break;
                default:
                    break;
            }
        }

        private void SendContentPackageList(PacketBase receivedPacket)
        {
            ContentPackageListPacket packet = new ContentPackageListPacket
            {
                FileNames = ContentPackageFileNames
            };
            Agent.WriteMessage(packet);
            Agent.SendMessage(receivedPacket.SenderConnection, NetDeliveryMethod.ReliableSequenced);
            
        }

        #endregion

        #region Methods - File transfer processing

        /// <summary>
        /// Receives a file request packet and either starts a file transfer or cancels an existing one.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessFileTransfer(FileRequestPacket packet)
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
            foreach (KeyValuePair<NetConnection, FileTransferProgress> client in FileTransferClients)
            {
                if (File.Exists(client.Value.FilePath))
                {
                    using (FileStream fileStream = new FileStream(client.Value.FilePath, FileMode.Open))
                    {
                        bool isEndOfFile = false;
                        int numberOfBytesToSend = GameServerConfiguration.FileTransferBufferSize;
                        int numberOfBytesRemaining = (int)fileStream.Length - client.Value.BytesSent;

                        if (numberOfBytesRemaining < GameServerConfiguration.FileTransferBufferSize)
                        {
                            numberOfBytesToSend = numberOfBytesRemaining;
                            isEndOfFile = true;
                        }

                        byte[] bytesToSend = new byte[numberOfBytesToSend];
                        fileStream.Read(bytesToSend, (int)client.Value.BytesSent, numberOfBytesToSend); // TO-DO: Fix this run time error 2013-05-27

                        StreamingFilePacket packet = new StreamingFilePacket
                        {
                            FileName = Path.GetFileName(client.Value.FilePath),
                            FileBytes = bytesToSend,
                            IsEndOfFile = isEndOfFile
                        };
                        Agent.WriteMessage(packet);
                        Agent.SendMessage(client.Key, NetDeliveryMethod.ReliableOrdered);

                        client.Value.BytesSent += numberOfBytesToSend;

                        // Remove client from streaming list if this was the last part of the file to be sent
                        if (client.Value.BytesSent >= (int)fileStream.Length)
                        {
                            FileTransferClients.Remove(client.Key);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
