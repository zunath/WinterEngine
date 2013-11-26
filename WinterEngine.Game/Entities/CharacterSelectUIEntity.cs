using System;
using System.Collections.Generic;
using Awesomium.Core;
using Lidgren.Network;
using Newtonsoft.Json;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Packets;
using WinterEngine.DataTransferObjects.ViewModels;
using WinterEngine.Game.Screens;
using WinterEngine.Game.Services;
using WinterEngine.Library.Extensions;

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
            if (!Object.ReferenceEquals(WinterEngineService.NetworkClient, null))
            {
                WinterEngineService.NetworkClient.OnPacketReceived -= NetworkClient_OnPacketReceived;
            }
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

            EntityJavascriptObject.Bind("RequestServerInformation", false, RequestServerInformation);
            EntityJavascriptObject.Bind("GetModelJSON", true, GetModelJSON);

            EntityJavascriptObject.Bind("NewCharacter", false, NewCharacter);
            EntityJavascriptObject.Bind("DeleteCharacter", false, DeleteCharacter);
            EntityJavascriptObject.Bind("JoinServer", false, JoinServer);
            EntityJavascriptObject.Bind("CancelCharacterSelection", false, CancelCharacterSelection);
            EntityJavascriptObject.Bind("LoadCharacter", false, LoadCharacter);

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
            RaiseChangeScreenEvent(new TypeOfEventArgs(typeof(CharacterCreationScreen)));
        }

        private void LoadCharacter(object sender, JavascriptMethodEventArgs e)
        {
            int index = (int)e.Arguments[0];
            ViewModel.ActiveCharacterIndex = index;

            AsyncJavascriptCallback("LoadCharacterInformation_Callback");
        }

        private void DeleteCharacter(object sender, JavascriptMethodEventArgs e)
        {
            if (ViewModel.ActiveCharacter != null)
            {
                string fileName = ViewModel.ActiveCharacter.FileName;
                DeleteCharacterPacket packet = new DeleteCharacterPacket(fileName, DeleteCharacterTypeEnum.Request);

                if (!Object.ReferenceEquals(WinterEngineService.NetworkClient, null))
                {
                    WinterEngineService.NetworkClient.SendPacket(packet, NetDeliveryMethod.ReliableUnordered);
                }
            }
        }

        private void JoinServer(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void CancelCharacterSelection(object sender, JavascriptMethodEventArgs e)
        {
            if (!Object.ReferenceEquals(WinterEngineService.NetworkClient, null))
            {
                WinterEngineService.NetworkClient.Disconnect();
            }

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
            ViewModel.Characters = packet.CharacterList == null ? new List<PlayerCharacter>() : packet.CharacterList;
            ViewModel.ServerName = packet.ServerName;
            ViewModel.Announcement = packet.ServerAnnouncement;

            // Generate portraits for each character
            
            for (int index = 0; index < ViewModel.Characters.Count; index++)
            {
                string base64Portrait = ViewModel.Characters[index].Portrait.ToBase64String();
                ViewModel.CharacterPortraits.Insert(index, base64Portrait);
            }

            ViewModel.ActiveCharacterIndex = 0;

            AsyncJavascriptCallback("RequestServerInformation_Callback");
        }

        /// <summary>
        /// Processes a packet containing the delete character response.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessDeleteCharacterResponsePacket(DeleteCharacterPacket packet)
        {
            if(packet.DeleteRequestType == DeleteCharacterTypeEnum.Accepted)
            {
                ViewModel.DeleteCharacterResponseMessage = "Your character has been deleted successfully.";
            }
            else if (packet.DeleteRequestType == DeleteCharacterTypeEnum.Denied)
            {
                ViewModel.DeleteCharacterResponseMessage = "Your request to delete your character has been denied by the server.";
            }
            else if (packet.DeleteRequestType == DeleteCharacterTypeEnum.DeniedDisabled)
            {
                ViewModel.DeleteCharacterResponseMessage = "The server has disabled character deletion.";
            }
            else if (packet.DeleteRequestType == DeleteCharacterTypeEnum.Error)
            {
                ViewModel.DeleteCharacterResponseMessage = "An error has occurred. Your character has not been deleted.";
            }
            else if (packet.DeleteRequestType == DeleteCharacterTypeEnum.FileNotFound)
            {
                ViewModel.DeleteCharacterResponseMessage = "Character does not exist.";
            }

            AsyncJavascriptCallback("DeleteCharacter_Callback", (int)packet.DeleteRequestType);
        }

        #endregion
    }
}
