using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class GameNetworkListenerProcessEventArgs: EventArgs
    {
        public List<string> PlayerList { get; set; }
        public List<string> BanUserList { get; set; }
        public List<string> BootUserList { get; set; }
        public string ServerMessage { get; set; }
        public List<string> LogMessages { get; set; }
        public string ServerAnnouncement { get; set; }

        public GameNetworkListenerProcessEventArgs()
        {
            this.PlayerList = new List<string>();
            this.BanUserList = new List<string>();
            this.BootUserList = new List<string>();
            this.ServerMessage = "";
            this.LogMessages = new List<string>();
            this.ServerAnnouncement = "";
        }
    }
}
