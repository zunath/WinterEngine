using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.XMLObjects
{
    public class ServerSettingsXML
    {
        public int PortNumber { get; set; }
        public string ServerName { get; set; }
        public string ServerDescription { get; set; }
        public string ServerAnnouncement { get; set; }
        public int MaxLevel { get; set; }
        public int MaxPlayers { get; set; }
        public PVPTypeEnum PVPSetting { get; set; }
        public bool AllowCharacterDeletion { get; set; }
        public bool AllowFileAutoDownload { get; set; }
        public string PlayerPassword { get; set; }
        public string GMPassword { get; set; }
        public List<string> BannedUserAccounts { get; set; }

        public ServerSettingsXML()
        {
            this.PortNumber = 0;
            this.ServerName = "";
            this.ServerDescription = "";
            this.ServerAnnouncement = "";
            this.MaxLevel = 0;
            this.MaxPlayers = 0;
            this.PVPSetting = PVPTypeEnum.None;
            this.AllowCharacterDeletion = false;
            this.AllowFileAutoDownload = false;
            this.PlayerPassword = "";
            this.GMPassword = "";
            this.BannedUserAccounts = new List<string>();
        }
    }
}
