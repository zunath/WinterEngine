using System;
using System.Collections.Generic;

using WinterEngine.Network;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.Game.Screens;
using WinterEngine.Network.Clients;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Game.Services;
using WinterEngine.DataTransferObjects.Packets;

using Awesomium.Core;
using System.IO;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataTransferObjects.Paths;
using Lidgren.Network;
using WinterEngine.DataTransferObjects.Enums;
using System.Threading.Tasks;

namespace WinterEngine.Game.Entities
{
	public partial class ServerListUIEntity
    {
        #region Properties

        private WebServiceClientUtility WebUtility { get; set; }
        private FileExtensionFactory FileExtensionFactory { get; set; }
        private List<string> MissingFiles { get; set; }
        private long FileSize { get; set; }
        private string LastReceivedFile { get; set; }
        private FileStreamerStatusEnum FileStreamStatus { get; set; }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            WebUtility = new WebServiceClientUtility();
            FileExtensionFactory = new FileExtensionFactory();
            MissingFiles = new List<string>();

            AwesomiumWebView.DocumentReady += OnDocumentReady;

            WinterEngineService.InitializeNetworkClient();
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

            EntityJavascriptObject.Bind("GetServerList", false, GetServerListAsync);
            EntityJavascriptObject.Bind("ConnectToServer", false, ConnectToServerAsync);
            EntityJavascriptObject.Bind("GoToMainMenu", false, GoToMainMenu);
            EntityJavascriptObject.Bind("CancelConnectToServer", false, CancelConnectToServerAsync);

            RunJavaScriptMethod("Initialize();");
        }

        #endregion

        #region UI Methods

        /// <summary>
        /// Retrieves the list of active servers from the master server and passes the
        /// JSON result to the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetServerListAsync(object sender, JavascriptMethodEventArgs e)
        {
            string jsonServerList = "";
            await TaskEx.Run(() =>
            {
                jsonServerList = WebUtility.GetAllActiveServers();
            });

            AsyncJavascriptCallback("GetAllServers_Callback", jsonServerList);
        }

        private async void ConnectToServerAsync(object sender, JavascriptMethodEventArgs e)
        {
            AuthorizationTypeEnum response = AuthorizationTypeEnum.Unknown;
            ConnectionAddress serverAddress = new ConnectionAddress
            {
                IPAddress = e.Arguments[0],
                Port = (int)e.Arguments[1]
            };

            await TaskEx.Run(() =>
            {
                // Authorize with master server for the specified server.
                response = WebUtility.RequestClientAuthorization(
                    serverAddress.IPAddress, 
                    serverAddress.Port, 
                    WinterEngineService.ActiveUserProfile.UserName, 
                    WinterEngineService.ActiveUserProfile.ActiveAuthorizationTokenGUID);

                if (response == AuthorizationTypeEnum.Success)
                {
                    WinterEngineService.NetworkClient.Connect(serverAddress);
                }
            });

            if(response != AuthorizationTypeEnum.Success)
            {
                AsyncJavascriptCallback("ConnectToServer_Callback", (int)response);
            }
        }

        private async void CancelConnectToServerAsync(object sender, JavascriptMethodEventArgs e)
        {
            await TaskEx.Run(() =>
            {
                CancelRequestFileFromServer();
                WinterEngineService.NetworkClient.Disconnect();
            });
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
            else if (packetType == typeof(RequestPacket))
            {
                ProcessRequest(e.Packet as RequestPacket);
            }
            else if (packetType == typeof(ClientDisconnectPacket))
            {
                ProcessClientDisconnectPacket(e.Packet as ClientDisconnectPacket);
            }
        }

        private void ProcessClientDisconnectPacket(ClientDisconnectPacket packet)
        {
            AsyncJavascriptCallback("DisconnectFromServerWithReason_Callback", packet.Reason);
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
            FileSize = packet.FileSize;
        }

        /// <summary>
        /// Processes a packet containing a list of content package files.
        /// Files which are not on the client's machine will be added to a list to be downloaded.
        /// The first file in this list is requested at the end of this method call.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessContentPackageListPacket(ContentPackageListPacket packet)
        {
            if (packet.FileNames != null)
            {
                foreach (string fileName in packet.FileNames)
                {
                    string filePath = DirectoryPaths.ContentPackageDirectoryPath + fileName;

                    if (!File.Exists(filePath))
                    {
                        MissingFiles.Add(fileName);
                    }
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
            if (FileStreamStatus == FileStreamerStatusEnum.Downloading)
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
        }

        private void ProcessRequest(RequestPacket packet)
        {
            switch (packet.RequestType)
            {
                case PacketRequestTypeEnum.Username:
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
            if (WinterEngineService.ActiveUserProfile != null)
            {
                UsernamePacket packet = new UsernamePacket
                {
                    Username = WinterEngineService.ActiveUserProfile.UserName
                };
                WinterEngineService.NetworkClient.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
            }
        }

        #endregion
    }
}
