using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.XMLObjects;

namespace WinterEngine.DataTransferObjects.ViewModels
{
    public class ServerViewModel: INotifyPropertyChanged
    {
        #region Fields

        private ServerSettingsXML _serverSettings;
        private string _serverMessage;
        private string _moduleFileName;
        private string _blackListUserName;
        private string _serverStatusMessage;
        private string _serverIPAddress;
        private ServerStatusEnum _serverStatus;

        #endregion

        #region Events / Delegates
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public ServerSettingsXML ServerSettings 
        {
            get
            {
                return _serverSettings;
            }
            set
            {
                _serverSettings = value;
                OnPropertyChanged("ServerSettings");
            }
        }
        public ServerStatusEnum ServerStatus 
        {
            get
            {
                return _serverStatus;
            }

            set
            {
                _serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        public List<ContentPackage> ContentPackageList { get; set; }
        public BindingList<string> ConnectedUsernames { get; set; }
        public string ServerMessage 
        { 
            get
            {
                return _serverMessage;
            }
            set
            {
                _serverMessage = value;
                OnPropertyChanged("ServerMessage");
            }
        }
        public string QueuedServerMessage { get; set; }
        public List<string> QueuedBootUserList { get; set; }
        public List<string> QueuedBanUserList { get; set; }

        public string ModuleFileName 
        {
            get
            {
                return _moduleFileName;
            }
            set
            {
                _moduleFileName = value;
                OnPropertyChanged("ModuleFileName");
            }
        }
        public string BlackListUserName 
        {
            get
            {
                return _blackListUserName;
            }
            set
            {
                _blackListUserName = value;
                OnPropertyChanged("BlackListUserName");
            }
        }
        public string ServerStatusMessage 
        {
            get
            {
                return _serverStatusMessage;
            }
            set
            {
                _serverStatusMessage = value;
                OnPropertyChanged("ServerStatusMessage");
            }
        }
        public string ServerIPAddress 
        {
            get
            {
                return _serverIPAddress;
            }
            set
            {
                _serverIPAddress = value;
                OnPropertyChanged("ServerIPAddress");
            }
        }

        #region Constructors
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
            this.ServerStatusMessage = "Stopped...";
            this.ServerIPAddress = "Checking...";
        }

        #endregion

        #region Methods

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
