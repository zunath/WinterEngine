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

        private NetworkAgent Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        private List<PacketBase> IncomingPackets
        {
            get { return _incomingPackets; }
            set { _incomingPackets = value; }
        }

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

        private FileExtensionFactory FileExtensionFactory
        {
            get { return _fileExtensionFactory; }
        }

        private List<string> MissingFiles
        {
            get { return _missingFiles; }
            set { _missingFiles = value; }
        }

        #endregion

        #region Constructors

        public GameNetworkClient(ConnectionAddress address)
        {
            Agent = new NetworkAgent(AgentRoleEnum.Client, GameServerConfiguration.ApplicationID, address.ServerPort);
            Agent.Connect(address.ServerIPAddress);
            _fileExtensionFactory = new FileExtensionFactory();
            _missingFiles = new List<string>();
        }
        
        #endregion

        #region Methods - General

        public void Process()
        {
            IncomingPackets = Agent.CheckForPackets();

            foreach (PacketBase packet in IncomingPackets)
            {
                ProcessPacket(packet);
            }

            Thread.Sleep(5);
        }

        public void Disconnect()
        {
            Agent.Shutdown();
        }

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

        public void RequestServerContentPackageList()
        {
            RequestPacket packet = new RequestPacket(RequestTypeEnum.ServerContentPackageList);

            Agent.WriteMessage(packet);
            Agent.SendMessage(ServerConnection, NetDeliveryMethod.ReliableSequenced);
        }

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
