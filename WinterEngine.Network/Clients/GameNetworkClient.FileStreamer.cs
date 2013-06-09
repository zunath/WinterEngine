using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Clients
{
    public partial class GameNetworkClient
    {
        #region Fields

        private List<string> _fileStreamerMissingFiles;
        private long _fileStreamerFileSize;
        private string _fileStreamerLastReceivedFile;
        private FileStreamerStatusEnum _fileStreamStatus;
        

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the list of missing files which are required by the server.
        /// </summary>
        private List<string> FileStreamerMissingFiles
        {
            get 
            {
                if (_fileStreamerMissingFiles == null)
                {
                    _fileStreamerMissingFiles = new List<string>();
                }

                return _fileStreamerMissingFiles; 
            }
            set { _fileStreamerMissingFiles = value; }
        }

        /// <summary>
        /// Gets the size of the file currently being downloaded by the file streamer.
        /// </summary>
        private long FileStreamerFileSize
        {
            get { return _fileStreamerFileSize; }
        }

        /// <summary>
        /// Gets the last file received by the file streamer.
        /// </summary>
        public string FileStreamerLastReceivedFile
        {
            get { return _fileStreamerLastReceivedFile; }
            private set { _fileStreamerLastReceivedFile = value; }
        }

        public FileStreamerStatusEnum FileStreamerStatus
        {
            get { return _fileStreamStatus; }
            private set { _fileStreamStatus = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes a streaming file packet, building a file as bytes are received.
        /// Files received must be content packages for security reasons.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessStreamingFilePacket(StreamingFilePacket packet)
        {
            if (Path.GetExtension(packet.FileName) == FileExtensionFactory.GetFileExtension(FileTypeEnum.ContentPackage) && FileStreamerStatus == FileStreamerStatusEnum.Downloading)
            {
                string filePath = DirectoryPaths.ContentPackageDirectoryPath + packet.FileName;
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                FileStreamerLastReceivedFile = packet.FileName;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Append))
                {
                    fileStream.Write(packet.FileBytes, 0, (int)packet.FileBytes.Length);
                }

                // If this is the end of the file, we need to remove it from the list
                // and request the next file from the server.
                if (packet.IsEndOfFile)
                {
                    FileStreamerMissingFiles.Remove(packet.FileName);

                    if (FileStreamerMissingFiles.Count > 0)
                    {
                        FileStreamerStatus = FileStreamerStatusEnum.Stopped;
                        RequestFileFromServer(FileStreamerMissingFiles[0]);
                    }
                    else
                    {
                        FileStreamerStatus = FileStreamerStatusEnum.Complete;
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
            FileStreamerStatus = FileStreamerStatusEnum.Downloading;

            FileRequestPacket packet = new FileRequestPacket
            {
                FileRequestType = FileRequestTypeEnum.StartFileRequest,
                FileName = fileName
            };

            Agent.SendPacket(packet, ServerConnection, NetDeliveryMethod.ReliableSequenced);
        }

        /// <summary>
        /// Sends a request to stop a file transfer.
        /// </summary>
        public void CancelRequestFileFromServer()
        {
            FileRequestPacket packet = new FileRequestPacket
            {
                FileRequestType = FileRequestTypeEnum.CancelFileRequest
            };
            FileStreamerStatus = FileStreamerStatusEnum.Stopped;

            Agent.SendPacket(packet, ServerConnection, NetDeliveryMethod.ReliableSequenced);

            // Remove partially downloaded file, if it exists.
            string filePath = DirectoryPaths.ContentPackageDirectoryPath + FileStreamerLastReceivedFile;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _fileStreamerFileSize = 0;
            FileStreamerLastReceivedFile = "";
            FileStreamerMissingFiles.Clear();

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
                    FileStreamerMissingFiles.Add(fileName);
                }
            }

            if (FileStreamerMissingFiles.Count > 0)
            {
                RequestFileFromServer(FileStreamerMissingFiles[0]);
            }
            else
            {
                FileStreamerStatus = FileStreamerStatusEnum.Complete;
            }
        }

        /// <summary>
        /// Processes a packet containing the size of a file about to be streamed.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessStreamingFileDetailsPacket(StreamingFileDetailsPacket packet)
        {
            _fileStreamerFileSize = packet.FileSize;
        }

        public int GetFileStreamerPercentComplete()
        {
            string filePath = DirectoryPaths.ContentPackageDirectoryPath + FileStreamerLastReceivedFile;
            if (File.Exists(filePath))
            {
                FileInfo info = new FileInfo(DirectoryPaths.ContentPackageDirectoryPath + FileStreamerLastReceivedFile);
                float percentComplete = (float)info.Length / (float)FileStreamerFileSize;

                return Convert.ToInt32(percentComplete * 100.0f);
            }
            else
            {
                return 0;
            }

        }

        #endregion
    }
}
