using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.Network;
using Awesomium.Core;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.Game.Screens;
using WinterEngine.Network.Clients;
using System.ComponentModel;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Game.Services;
using WinterEngine.Network.BusinessObjects;
using WinterEngine.Network.Packets;
using System.IO;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.Network.Enums;
using Lidgren.Network;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class ServerListUIEntity
    {
        #region Fields

        private WebServiceClientUtility _webServiceUtility;
        private FileExtensionFactory _fileExtensionFactory;
        private List<string> _missingFiles;
        private long _fileSize;
        private string _lastReceivedFile;
        private FileStreamerStatusEnum _fileStreamStatus;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the web utility used for making calls to the master server.
        /// </summary>
        private WebServiceClientUtility WebUtility
        {
            get
            {
                if (_webServiceUtility == null)
                {
                    _webServiceUtility = new WebServiceClientUtility();
                }

                return _webServiceUtility;
            }
        }

        private FileExtensionFactory FileExtensionFactory
        {
            get
            {
                if (_fileExtensionFactory == null)
                {
                    _fileExtensionFactory = new FileExtensionFactory();
                }

                return _fileExtensionFactory;
            }
            
        }

        /// <summary>
        /// Gets or sets the list of missing files which are required by the server.
        /// </summary>
        private List<string> MissingFiles
        {
            get
            {
                if (_missingFiles == null)
                {
                    _missingFiles = new List<string>();
                }

                return _missingFiles;
            }
            set { _missingFiles = value; }
        }

        /// <summary>
        /// Gets the size of the file currently being downloaded by the file streamer.
        /// </summary>
        private long FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        /// <summary>
        /// Gets the last file received by the file streamer.
        /// </summary>
        public string LastReceivedFile
        {
            get { return _lastReceivedFile; }
            private set { _lastReceivedFile = value; }
        }

        public FileStreamerStatusEnum FileStreamStatus
        {
            get { return _fileStreamStatus; }
            private set { _fileStreamStatus = value; }
        }


        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            AwesomiumWebView.DocumentReady += OnDocumentReady;

            GameNetworkClient client = new GameNetworkClient();
            if (!Object.ReferenceEquals(WinterEngineService.ActiveUserProfile, null))
            {
                client.Username = WinterEngineService.ActiveUserProfile.UserName;
            }

            WinterEngineService.InitializeNetworkClient(client);
            WinterEngineService.NetworkClient.OnPacketReceived += NetworkClient_OnPacketReceived;
		}

		private void CustomActivity()
		{
            WinterEngineService.NetworkClient.Process();

            if (FileStreamStatus == FileStreamerStatusEnum.Downloading && !String.IsNullOrWhiteSpace(LastReceivedFile))
            {
                AsyncJavascriptCallback("UpdateDownloadProgressBar", GetFileStreamerPercentComplete(), LastReceivedFile);
            }
            else if (FileStreamStatus == FileStreamerStatusEnum.Complete)
            {
                // We've received all files and are ready to move to the character selection screen.
                RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(CharacterSelectScreen)));
            }
		}

		private void CustomDestroy()
		{
            WinterEngineService.NetworkClient.OnPacketReceived -= NetworkClient_OnPacketReceived;

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Awesomium Event Handling 

        /// <summary>
        /// Handles binding the global javascript object to C# methods.
        /// Enables javascript to call this entity's methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDocumentReady(object sender, EventArgs e)
        {
            AwesomiumWebView.DocumentReady -= OnDocumentReady;

            EntityJavascriptObject.Bind("GetServerList", false, GetServerList);
            EntityJavascriptObject.Bind("ConnectToServer", false, ConnectToServer);
            EntityJavascriptObject.Bind("GoToMainMenu", false, GoToMainMenu);
            EntityJavascriptObject.Bind("CancelConnectToServer", false, CancelConnectToServer);
        }

        #endregion

        #region UI Methods

        /// <summary>
        /// Retrieves the list of active servers from the master server and passes the
        /// JSON result to the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetServerList(object sender, JavascriptMethodEventArgs e)
        {
            string jsonServerList = WebUtility.GetAllActiveServers();
            AsyncJavascriptCallback("GetAllServers_Callback", jsonServerList);
        }

        /// <summary>
        /// Handles connecting to a specified game server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectToServer(object sender, JavascriptMethodEventArgs e)
        {
            ConnectionAddress address = new ConnectionAddress
            {
                ServerIPAddress = e.Arguments[0],
                ServerPort = (int)e.Arguments[1]
            };

            WinterEngineService.NetworkClient.Connect(address);
        }

        private void CancelConnectToServer(object sender, JavascriptMethodEventArgs e)
        {
            CancelRequestFileFromServer();
            WinterEngineService.NetworkClient.Disconnect();
        }

        private void GoToMainMenu(object sender, JavascriptMethodEventArgs e)
        {
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(MainMenuScreen)));
        }

        #endregion

        #region Network Methods

        private void NetworkClient_OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            Type packetType = e.Packet.GetType();

            if (packetType == typeof(StreamingFilePacket))
            {
                ProcessStreamingFilePacket(e.Packet as StreamingFilePacket);
            }
            else if (packetType == typeof(ContentPackageListPacket))
            {
                ProcessContentPackageListPacket(e.Packet as ContentPackageListPacket);
            }
            else if (packetType == typeof(StreamingFileDetailsPacket))
            {
                ProcessStreamingFileDetailsPacket(e.Packet as StreamingFileDetailsPacket);
            }
        }


        /// <summary>
        /// Processes a streaming file packet, building a file as bytes are received.
        /// Files received must be content packages for security reasons.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessStreamingFilePacket(StreamingFilePacket packet)
        {
            if (Path.GetExtension(packet.FileName) == FileExtensionFactory.GetFileExtension(FileTypeEnum.ContentPackage) && FileStreamStatus == FileStreamerStatusEnum.Downloading)
            {
                string filePath = DirectoryPaths.ContentPackageDirectoryPath + packet.FileName;
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                LastReceivedFile = packet.FileName;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Append))
                {
                    fileStream.Write(packet.FileBytes, 0, (int)packet.FileBytes.Length);
                }

                // If this is the end of the file, we need to remove it from the list
                // and request the next file from the server.
                if (packet.IsEndOfFile)
                {
                    MissingFiles.Remove(packet.FileName);

                    if (MissingFiles.Count > 0)
                    {
                        FileStreamStatus = FileStreamerStatusEnum.Stopped;
                        RequestFileFromServer(MissingFiles[0]);
                    }
                    else
                    {
                        FileStreamStatus = FileStreamerStatusEnum.Complete;
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
            FileStreamStatus = FileStreamerStatusEnum.Downloading;

            FileRequestPacket packet = new FileRequestPacket
            {
                FileRequestType = FileRequestTypeEnum.StartFileRequest,
                FileName = fileName
            };

            WinterEngineService.NetworkClient.SendPacket(packet, NetDeliveryMethod.ReliableSequenced);
        }

        /// <summary>
        /// Processes a packet containing the size of a file about to be streamed.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessStreamingFileDetailsPacket(StreamingFileDetailsPacket packet)
        {
            _fileSize = packet.FileSize;
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
            else
            {
                FileStreamStatus = FileStreamerStatusEnum.Complete;
            }
        }

        /// <summary>
        /// Returns the current file's download completion percentage.
        /// </summary>
        /// <returns></returns>
        private int GetFileStreamerPercentComplete()
        {
            string filePath = DirectoryPaths.ContentPackageDirectoryPath + LastReceivedFile;
            if (File.Exists(filePath))
            {
                FileInfo info = new FileInfo(DirectoryPaths.ContentPackageDirectoryPath + LastReceivedFile);
                float percentComplete = (float)info.Length / (float)FileSize;

                return Convert.ToInt32(percentComplete * 100.0f);
            }
            else
            {
                return 0;
            }

        }


        /// <summary>
        /// Sends a request to stop a file transfer.
        /// </summary>
        private void CancelRequestFileFromServer()
        {
            FileRequestPacket packet = new FileRequestPacket
            {
                FileRequestType = FileRequestTypeEnum.CancelFileRequest
            };
            FileStreamStatus = FileStreamerStatusEnum.Stopped;

            WinterEngineService.NetworkClient.SendPacket(packet, NetDeliveryMethod.ReliableSequenced);

            // Remove partially downloaded file, if it exists.
            string filePath = DirectoryPaths.ContentPackageDirectoryPath + LastReceivedFile;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            FileSize = 0;
            LastReceivedFile = "";
            MissingFiles.Clear();

        }

        private void ProcessRequest(RequestPacket packet)
        {
            switch (packet.RequestType)
            {
                case RequestTypeEnum.Username:
                    ProcessUsernameRequest();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Sends the username of the client when requested by the server.
        /// </summary>
        private void ProcessUsernameRequest()
        {
            UsernamePacket packet = new UsernamePacket
            {
                Username = WinterEngineService.ActiveUserProfile.UserName
            };
            WinterEngineService.NetworkClient.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
