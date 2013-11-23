using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataTransferObjects.Packets;

namespace WinterEngine.DataTransferObjects.Models
{
    public class GameNetworkListenerModel
    {
        public List<string> LogMessages { get; set; }
        public List<string> BannedUsersList { get; set; }
        public List<string> QueuedBootUsersList { get; set; }
        public string QueuedServerMessage { get; set; }
        public List<PacketBase> IncomingPackets { get; set; }
        public Dictionary<NetConnection, string> ConnectionUsernamesDictionary { get; set; }
        public List<string> ConnectedUsernames
        {
            get { return ConnectionUsernamesDictionary.Values.ToList(); }
        }
        public string ServerAnnouncement { get; set; }
        public string ServerName { get; set; }
        public bool IsCharacterDeletionEnabled { get; set; }

        public GameNetworkListenerModel()
        {
            this.LogMessages = new List<string>();
            this.BannedUsersList = new List<string>();
            this.QueuedBootUsersList = new List<string>();
            this.QueuedServerMessage = "";
            this.IncomingPackets = new List<PacketBase>();
            this.ConnectionUsernamesDictionary = new Dictionary<NetConnection, string>();
            this.ServerAnnouncement = "";
            this.ServerName = "";
            this.IsCharacterDeletionEnabled = false;
        }
    }
}
