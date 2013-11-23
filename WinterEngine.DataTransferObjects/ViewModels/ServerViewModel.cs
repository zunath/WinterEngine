using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.XMLObjects;

namespace WinterEngine.DataTransferObjects.ViewModels
{
    public class ServerViewModel
    {
        public ServerSettingsXML ServerSettings { get; set; }
        public ServerStatusEnum ServerStatus { get; set; }
        public List<ContentPackage> ContentPackageList { get; set; }
        public BindingList<string> ConnectedUsernames { get; set; }
        public string ServerMessage { get; set; }
        public string QueuedServerMessage { get; set; }
        public List<string> QueuedBootUserList { get; set; }
        public List<string> QueuedBanUserList { get; set; }
        public string ModuleFileName { get; set; }
        public string BlackListUserName { get; set; }
        public string ServerStatusMessage { get; set; }
        public string ServerIPAddress { get; set; }

        public ServerViewModel()
        {
            this.ServerSettings = new ServerSettingsXML();
            this.ServerStatus = ServerStatusEnum.Stopped;
            this.ContentPackageList = new List<ContentPackage>();
            this.QueuedServerMessage = "";
            this.QueuedBootUserList = new List<string>();
            this.QueuedBanUserList = new List<string>();
            this.ConnectedUsernames = new BindingList<string>();
            this.ServerMessage = "";
            this.ModuleFileName = "";
            this.BlackListUserName = "";
            this.ServerStatusMessage = "";
            this.ServerIPAddress = "Checking...";
        }
    }
}
