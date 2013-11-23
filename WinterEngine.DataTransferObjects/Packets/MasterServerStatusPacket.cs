using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Packets
{
    /// <summary>
    /// This class is serialized to JSON and sent to the master server.
    /// Used for tracking a client-server's status
    /// </summary>
    public class MasterServerStatusPacket
    {
        public string ServerIPAddress { get; set; }
        public int ServerPort { get; set; }
        public string ServerName { get; set; }
        public string ServerDescription { get; set; }
        public int ServerMaxLevel { get; set; }
        public int ServerMaxPlayers { get; set; }
        public int ServerCurrentPlayers { get; set; }
        public byte GameTypeID { get; set; }
        public byte PVPTypeID { get; set; }
        public bool IsAutoDownloadEnabled { get; set; }
        public bool IsCharacterDeletionEnabled { get; set; }
        public bool IsActive { get; set; }
    }
}
