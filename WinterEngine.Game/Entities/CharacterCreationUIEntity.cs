using Awesomium.Core;
using Lidgren.Network;
using Newtonsoft.Json;
using System;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Packets;
using WinterEngine.DataTransferObjects.ViewModels;
using WinterEngine.Game.Services;

namespace WinterEngine.Game.Entities
{
	public partial class CharacterCreationUIEntity
    {
        #region Fields

        #endregion

        #region Properties

        private CharacterCreationViewModel ViewModel { get; set; }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            ViewModel = new CharacterCreationViewModel();
            AwesomiumWebView.DocumentReady += OnDocumentReady;

            if (WinterEngineService.NetworkClient != null)
            {
                WinterEngineService.NetworkClient.OnPacketReceived += NetworkClient_OnPacketReceived;
                WinterEngineService.NetworkClient.SendRequest(PacketRequestTypeEnum.CharacterCreation, NetDeliveryMethod.ReliableUnordered);
            }
		}


		private void CustomActivity()
		{
            if (WinterEngineService.NetworkClient != null)
            {
                WinterEngineService.NetworkClient.Process();
            }

		}

		private void CustomDestroy()
		{
            if (WinterEngineService.NetworkClient != null)
            {
                WinterEngineService.NetworkClient.OnPacketReceived -= NetworkClient_OnPacketReceived;
            }
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Awesomium Event Handling

        private void OnDocumentReady(object sender, EventArgs e)
        {
            EntityJavascriptObject.Bind("GetModelJSON", true, GetModelJSON);

            RunJavaScriptMethod("Initialize();");
        }

        #endregion

        #region UI Methods 

        private void GetModelJSON(object sender, JavascriptMethodEventArgs e)
        {
            e.Result = JsonConvert.SerializeObject(ViewModel);
        }


        #endregion

        #region Network Methods

        private void NetworkClient_OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            Type packetType = e.Packet.GetType();

            if (packetType == typeof(CharacterCreationPacket))
            {
                ProcessCharacterCreationPacket(e.Packet as CharacterCreationPacket);
            }
        }

        private void ProcessCharacterCreationPacket(CharacterCreationPacket packet)
        {
            ViewModel.RaceList = packet.RaceList;
            ViewModel.GenderList = packet.GenderList;

            AsyncJavascriptCallback("RetrieveServerData_Callback");
        }

        #endregion

    }
}
