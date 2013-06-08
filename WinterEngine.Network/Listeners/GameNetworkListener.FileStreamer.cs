using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
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

        private List<ContentPackage> _contentPackages;
        private List<string> _contentPackageNames;
        private List<string> _contentPackagePaths;
        private Dictionary<NetConnection, FileTransferProgress> _fileTransferClients;

        #endregion

        #region Properties

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
        /// Gets or sets the list of clients to which the listener is streaming files.
        /// </summary>
        private Dictionary<NetConnection, FileTransferProgress> FileTransferClients
        {
            get { return _fileTransferClients; }
            set { _fileTransferClients = value; }
        }

        #endregion

        #region Methods


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
