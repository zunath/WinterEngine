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
            EntityJavascriptObject.Bind("SelectAbility", false, SelectAbility);
            EntityJavascriptObject.Bind("SelectClass", false, SelectClass);
            EntityJavascriptObject.Bind("SelectPortrait", false, SelectPortrait);
            EntityJavascriptObject.Bind("SelectRace", false, SelectRace);
            EntityJavascriptObject.Bind("SelectSkill", false, SelectSkill);
            
            RunJavaScriptMethod("Initialize();");
        }

        #endregion

        #region UI Methods 

        private void GetModelJSON(object sender, JavascriptMethodEventArgs e)
        {
            e.Result = JsonConvert.SerializeObject(ViewModel);
        }

        private void SelectRace(object sender, JavascriptMethodEventArgs e)
        {

            AsyncJavascriptCallback("SelectRace_Callback");
        }
        private void SelectClass(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("SelectClass_Callback");
        }
        private void SelectAbility(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("SelectAbility_Callback");
        }

        private void SelectSkill(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("SelectSkill_Callback");
        }

        private void SelectPortrait(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("SelectPortrait_Callback");
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
