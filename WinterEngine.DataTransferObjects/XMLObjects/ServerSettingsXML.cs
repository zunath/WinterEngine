using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.XMLObjects
{
    public class ServerSettingsXML: INotifyPropertyChanged
    {
        #region Fields

        private int _portNumber;
        private string _serverName;
        private string _serverDescription;
        private string _serverAnnouncement;
        private int _maxLevel;
        private int _maxPlayers;
        private PVPTypeEnum _pvpSetting;
        private GameTypeEnum _gameType;
        private bool _allowCharacterDeletion;
        private bool _allowFileAutoDownload;
        private string _playerPassword;
        private string _gmPassword;


        #endregion

        #region Properties

        public int PortNumber 
        {
            get
            {
                return _portNumber;
            }
            set
            {
                _portNumber = value;
                OnPropertyChanged("PortNumber");
            }
        }

        public string ServerName 
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
                OnPropertyChanged("ServerName");
            }
        }

        public string ServerDescription 
        {
            get
            {
                return _serverDescription;
            }
            set
            {
                _serverDescription = value;
                OnPropertyChanged("ServerDescription");
            }
        }
        public string ServerAnnouncement 
        {
            get
            {
                return _serverAnnouncement;
            }
            set
            {
                _serverAnnouncement = value;
                OnPropertyChanged("ServerAnnouncement");
            }
        }
        public int MaxLevel 
        {
            get
            {
                return _maxLevel;
            }
            set
            {
                _maxLevel = value;
                OnPropertyChanged("MaxLevel");
            }
        }
        public int MaxPlayers 
        {
            get
            {
                return _maxPlayers;
            }
            set
            {
                _maxPlayers = value;
                OnPropertyChanged("MaxPlayers");
            }
        }

        public PVPTypeEnum PVPSetting 
        {
            get
            {
                return _pvpSetting;
            }
            set
            {
                _pvpSetting = value;
                OnPropertyChanged("PVPSetting");
            }
        }
        public GameTypeEnum GameType 
        {
            get
            {
                return _gameType;
            }
            set
            {
                _gameType = value;
                OnPropertyChanged("GameType");
            }
        }
        public bool AllowCharacterDeletion 
        {
            get
            {
                return _allowCharacterDeletion;
            }
            set
            {
                _allowCharacterDeletion = value;
                OnPropertyChanged("AllowCharacterDeletion");
            }
        }
        public bool AllowFileAutoDownload 
        {
            get
            {
                return _allowFileAutoDownload;
            }
            set
            {
                _allowFileAutoDownload = value;
                OnPropertyChanged("AllowFileAutoDownload");
            }
        }
        public string PlayerPassword 
        {
            get
            {
                return _playerPassword;
            }
            set
            {
                _playerPassword = value;
                OnPropertyChanged("PlayerPassword");
            }
        }
        public string GMPassword 
        {
            get
            {
                return _gmPassword;
            }
            set
            {
                _gmPassword = value;
                OnPropertyChanged("GMPassword");
            }
        }
        public BindingList<string> BannedUserAccounts { get; set; }
        
        #endregion

        #region Events / Delegates

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public ServerSettingsXML()
        {
            this.PortNumber = 0;
            this.ServerName = "";
            this.ServerDescription = "";
            this.ServerAnnouncement = "";
            this.MaxLevel = 0;
            this.MaxPlayers = 0;
            this.PVPSetting = PVPTypeEnum.None;
            this.GameType = GameTypeEnum.Action;
            this.AllowCharacterDeletion = false;
            this.AllowFileAutoDownload = false;
            this.PlayerPassword = "";
            this.GMPassword = "";
            this.BannedUserAccounts = new BindingList<string>();
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
