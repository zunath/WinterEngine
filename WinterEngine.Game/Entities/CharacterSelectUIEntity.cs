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

using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using WinterEngine.Network.Clients;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.Game.Screens;
using WinterEngine.Game.Services;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Packets;
using System.Web.Script.Serialization;
using Lidgren.Network;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.ViewModels;
using Newtonsoft.Json;

namespace WinterEngine.Game.Entities
{
	public partial class CharacterSelectUIEntity
    {
        #region Fields

        #endregion

        #region Properties

        private CharacterSelectionViewModel ViewModel { get; set; }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
        {
            ViewModel = new CharacterSelectionViewModel();
            AwesomiumWebView.DocumentReady += OnDocumentReady;

            // debugging
            ViewModel.ActiveCharacter = new PlayerCharacter();
            ViewModel.ActiveCharacter.FirstName = "Zunath";
            ViewModel.ActiveCharacter.LastName = "Zintuachi";
            ViewModel.ActiveCharacter.Race = new DataTransferObjects.Race
            {
                Name = "Race"
            };

            ViewModel.Characters = new List<PlayerCharacter>();
            ViewModel.Characters.Add(ViewModel.ActiveCharacter);
            ViewModel.Characters.Add(ViewModel.ActiveCharacter);
            ViewModel.Characters.Add(ViewModel.ActiveCharacter);

            // end debugging

            if (!Object.ReferenceEquals(WinterEngineService.NetworkClient, null))
            {
                WinterEngineService.NetworkClient.OnPacketReceived += NetworkClient_OnPacketReceived;
            }
		}


		private void CustomActivity()
		{
            if (!Object.ReferenceEquals(WinterEngineService.NetworkClient, null))
            {
                CheckForLostConnection();
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
            EntityJavascriptObject.Bind("RequestServerInformation", false, RequestServerInformation);

            // Model Data
            EntityJavascriptObject.Bind("GetModelJSON", true, GetModelJSON);

            // Button Functionality
            EntityJavascriptObject.Bind("NewCharacter", false, NewCharacter);
            EntityJavascriptObject.Bind("DeleteCharacter", false, DeleteCharacter);
            EntityJavascriptObject.Bind("JoinServer", false, JoinServer);
            EntityJavascriptObject.Bind("CancelCharacterSelection", false, CancelCharacterSelection);

            RunJavaScriptMethod("Initialize();");
        }

        #endregion

        #region UI Methods

        private void GetModelJSON(object sender, JavascriptMethodEventArgs e)
        {
            e.Result = JsonConvert.SerializeObject(ViewModel);
        }

        private void RequestServerInformation(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.NetworkClient, null))
            {
                WinterEngineService.NetworkClient.SendRequest(PacketRequestTypeEnum.CharacterSelection, NetDeliveryMethod.ReliableOrdered);
            }
        }

        private void NewCharacter(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void DeleteCharacter(object sender, JavascriptMethodEventArgs e)
        {
            string fileName = e.Arguments[0];
            DeleteCharacterPacket packet = new DeleteCharacterPacket(fileName, DeleteCharacterTypeEnum.Request);
            WinterEngineService.NetworkClient.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
        }

        private void JoinServer(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void CancelCharacterSelection(object sender, JavascriptMethodEventArgs e)
        {
            WinterEngineService.NetworkClient.Disconnect();
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(ServerListScreen)));
        }

        /// <summary>
        /// If a lost connection is detected, a pop up box will display.
        /// </summary>
        private void CheckForLostConnection()
        {
            if (WinterEngineService.NetworkClient.ConnectionStatus == NetConnectionStatus.Disconnected ||
                WinterEngineService.NetworkClient.ConnectionStatus == NetConnectionStatus.Disconnecting || 
                WinterEngineService.NetworkClient.ConnectionStatus == NetConnectionStatus.None)
            {
                AsyncJavascriptCallback("DisplayLostConnectionBox");
            }
        }


        #endregion

        #region Network Methods

        /// <summary>
        /// Processes packets as they are received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NetworkClient_OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            Type packetType = e.Packet.GetType();

            if (packetType == typeof(CharacterSelectionPacket))
            {
                ProcessCharacterSelectionPacket(e.Packet as CharacterSelectionPacket);
            }
            else if (packetType == typeof(DeleteCharacterPacket))
            {
                ProcessDeleteCharacterResponsePacket(e.Packet as DeleteCharacterPacket);
            }
        }

        /// <summary>
        /// Processes a packet containing data related to the character selection screen.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessCharacterSelectionPacket(CharacterSelectionPacket packet)
        {
            ViewModel.CanDeleteCharacters = packet.CanDeleteCharacters;
            ViewModel.Characters = packet.CharacterList;
            ViewModel.ServerName = packet.ServerName;
            ViewModel.Announcement = packet.ServerAnnouncement;

            if (packet.CharacterList.Count > 0)
            {
                ViewModel.ActiveCharacter = packet.CharacterList[0];
            }
            else
            {
                ViewModel.ActiveCharacter = new PlayerCharacter();
            }

            AsyncJavascriptCallback("RequestServerInformation_Callback");
        }

        /// <summary>
        /// Processes a packet containing the delete character response.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessDeleteCharacterResponsePacket(DeleteCharacterPacket packet)
        {
            AsyncJavascriptCallback("ConfirmDeleteCharacterButton_Callback", (int)packet.DeleteRequestType);
        }

        #endregion
    }
}
