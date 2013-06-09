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


#endif

namespace WinterEngine.Game.Entities
{
	public partial class ServerListUIEntity
    {
        #region Fields

        private WebServiceClientUtility _webServiceUtility;

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
		}

		private void CustomActivity()
		{
            WinterEngineService.NetworkClient.Process();

            if (WinterEngineService.NetworkClient.FileStreamerStatus == FileStreamerStatusEnum.Downloading
                && !String.IsNullOrWhiteSpace(WinterEngineService.NetworkClient.FileStreamerLastReceivedFile))
            {
                AsyncJavascriptCallback("UpdateDownloadProgressBar", WinterEngineService.NetworkClient.GetFileStreamerPercentComplete(), WinterEngineService.NetworkClient.FileStreamerLastReceivedFile);
            }
            else if (WinterEngineService.NetworkClient.FileStreamerStatus == FileStreamerStatusEnum.Complete)
            {
                // We've received all files and are ready to move to the character selection screen.
                RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(CharacterSelectScreen)));
            }
		}

		private void CustomDestroy()
		{


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
            WinterEngineService.NetworkClient.CancelRequestFileFromServer();
            WinterEngineService.NetworkClient.Disconnect();
        }

        private void GoToMainMenu(object sender, JavascriptMethodEventArgs e)
        {
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(MainMenuScreen)));
        }

        #endregion

    }
}
