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
    public class GameNetworkClient
    {
        #region Fields

        private NetworkAgent _agent;
        private List<PacketBase> _incomingPackets;
        private FileExtensionFactory _fileExtensionFactory;
        private List<string> _missingFiles;

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
            get { return _fileExtensionFactory; }
        }

        /// <summary>
        /// Gets or sets the list of missing files which are required by the server.
        /// </summary>
        private List<string> MissingFiles
        {
            get { return _missingFiles; }
            set { _missingFiles = value; }
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
            _fileExtensionFactory = new FileExtensionFactory();
            _missingFiles = new List<string>();
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
        }

        #endregion

        #region Methods - File Streaming

        /// <summary>
        /// Sends a request packet to the server asking for the list of content packages.
        /// </summary>
        public void RequestServerContentPackageList()
        {
            RequestPacket packet = new RequestPacket(RequestTypeEnum.ServerContentPackageList);

            Agent.WriteMessage(packet);
            Agent.SendMessage(ServerConnection, NetDeliveryMethod.ReliableSequenced);
        }

        /// <summary>
        /// Processes a streaming file packet, building a file as bytes are received.
        /// Files received must be content packages for security reasons.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessStreamingFilePacket(StreamingFilePacket packet)
        {
            if (Path.GetExtension(packet.FileName) == FileExtensionFactory.GetFileExtension(FileTypeEnum.ContentPackage))
            {
                string filePath = DirectoryPaths.ContentPackageDirectoryPath + packet.FileName;
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                
                using (FileStream fileStream = new FileStream(filePath, FileMode.Append))
                {
                    fileStream.Write(packet.FileBytes, (int)fileStream.Length, (int)packet.FileBytes.Length);

                    fileStream.Close();
                }

                // If this is the end of the file, we need to remove it from the list
                // and request the next file from the server.
                if (packet.IsEndOfFile)
                {
                    MissingFiles.Remove(packet.FileName);

                    if (MissingFiles.Count > 0)
                    {
                        RequestFileFromServer(MissingFiles[0]);
                    }
                }
            }
        }

        /// <summary>
        /// Sends a request to start initiating a specific file's data transfer
        /// </summary>
        /// <param name="fileName"></param>
        private void RequestFileFromServer(string fileName)
        {
            FileRequestPacket packet = new FileRequestPacket
            {
                FileRequestType = FileRequestTypeEnum.StartFileRequest,
                FileName = fileName
            };

            Agent.WriteMessage(packet);
            Agent.SendMessage(ServerConnection, NetDeliveryMethod.ReliableSequenced);
        }

        /// <summary>
        /// Processes a packet containing a list of content package files.
        /// Files which are not on the client's machine will be added to a list to be downloaded.
        /// The first file in this list is requested at the end of this method call.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessContentPackageListPacket(ContentPackageListPacket packet)
        {
            foreach (string fileName in packet.FileNames)
            {
                string filePath = DirectoryPaths.ContentPackageDirectoryPath + fileName;

                if (!File.Exists(filePath))
                {
                    MissingFiles.Add(fileName);
                }
            }

            if (MissingFiles.Count > 0)
            {
                RequestFileFromServer(MissingFiles[0]);
            }
        }

        #endregion
    }
}
