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
using Awesomium.Core;
using WinterEngine.Network.Clients;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.Game.Screens;
using WinterEngine.Game.Services;
using WinterEngine.Network.Enums;
using WinterEngine.Network.BusinessObjects;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.Network.Packets;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class CharacterSelectUIEntity
    {
        #region Fields

        private string _serverName;
        private string _serverAnnouncements;
        private List<PlayerCharacter> _playerCharacters;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the name of the server the user is currently connected to.
        /// </summary>
        public string ServerName
        {
            get { return _serverName; }
        }

        /// <summary>
        /// Returns the announcements specified by the server host.
        /// </summary>
        public string ServerAnnouncements
        {
            get { return _serverAnnouncements; }
        }

        /// <summary>
        /// Returns a list of player characters which exist on the active user's account.
        /// </summary>
        public List<PlayerCharacter> AccountPlayerCharacters
        {
            get
            {
                if (_playerCharacters == null)
                {
                    _playerCharacters = new List<PlayerCharacter>();
                }

                return _playerCharacters;
            }
        }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
        {
            AwesomiumWebView.DocumentReady += OnDocumentReady;
            WinterEngineService.NetworkClient.OnPacketReceived += NetworkClient_OnPacketReceived;
		}

		private void CustomActivity()
		{
            if (!Object.ReferenceEquals(WinterEngineService.NetworkClient, null))
            {
                WinterEngineService.NetworkClient.Process();
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

            // Page Initialization
            EntityJavascriptObject.Bind("InitializePage", false, InitializePage);

            // Button Functionality
            EntityJavascriptObject.Bind("NewCharacter", false, NewCharacter);
            EntityJavascriptObject.Bind("DeleteCharacter", false, DeleteCharacter);
            EntityJavascriptObject.Bind("JoinServer", false, JoinServer);
            EntityJavascriptObject.Bind("CancelCharacterSelection", false, CancelCharacterSelection);


            // There is a bug with Awesomium's rendering which is preventing the loading pop up from
            // displaying in the correct position when called from the Initialize() method in the JS file.
            AsyncJavascriptCallback("InitializeLoadingPopUpBox_Callback");
        }

        #endregion

        #region UI Methods

        private void InitializeLoadingPopUpBox(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void InitializePage(object sender, JavascriptMethodEventArgs e)
        {
            WinterEngineService.NetworkClient.SendRequest(RequestTypeEnum.CharacterSelection);
        }

        private void InitializeCharacterList(object sender, JavascriptMethodEventArgs e)
        {

            AsyncJavascriptCallback("InitializeServerInformation_Callback");
            AsyncJavascriptCallback("InitializeCharacterList_Callback");
        }

        private void NewCharacter(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void DeleteCharacter(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void JoinServer(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void CancelCharacterSelection(object sender, JavascriptMethodEventArgs e)
        {
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(ServerListScreen)));
        }

        #endregion

        #region Network Methods


        private void NetworkClient_OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            Type packetType = e.Packet.GetType();

            if (packetType == typeof(CharacterSelectionPacket))
            {
                ProcessCharacterSelectionPacket(e.Packet as CharacterSelectionPacket);
            }
        }

        /// <summary>
        /// Processes a packet containing data related to the character selection screen.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessCharacterSelectionPacket(CharacterSelectionPacket packet)
        {
            _playerCharacters = packet.PlayerList;
            _serverName = packet.ServerName;
            _serverAnnouncements = packet.ServerAnnouncement;
        }

        #endregion
    }
}
