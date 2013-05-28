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


#endif

namespace WinterEngine.Game.Entities
{
	public partial class ServerListUIEntity
    {
        #region Fields

        private GameNetworkClient _networkClient;

        #endregion

        #region Properties

        private GameNetworkClient NetworkClient
        {
            get { return _networkClient; }
            set { _networkClient = value; }
        }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            AwesomiumWebView.DocumentReady += OnDocumentReady;
		}

		private void CustomActivity()
		{
            if (!Object.ReferenceEquals(NetworkClient, null) && NetworkClient.IsConnected)
            {
                NetworkClient.Process();
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

            EntityJavascriptObject.Bind("GetServerList", true, GetServerList);
            EntityJavascriptObject.Bind("ConnectToServer", true, ConnectToServer);
            EntityJavascriptObject.Bind("GoToMainMenu", true, GoToMainMenu);
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
            WebServiceClientUtility utility = new WebServiceClientUtility();
            e.Result = utility.GetAllActiveServers();
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

            NetworkClient = new GameNetworkClient(address);            
        }

        private void GoToMainMenu(object sender, JavascriptMethodEventArgs e)
        {
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(MainMenuScreen)));
        }

        #endregion

    }
}
