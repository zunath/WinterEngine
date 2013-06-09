using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Network.Configuration
{
    public static class GameServerConfiguration
    {
        private const string ClientServerAppID = "29273776-C76F-40E2-A6AD-7C7E53582485";
        private const int ClientServerPort = 5121;
        private const int FileTransferListenerPort = 20;
        private const int FileTransferBufferSizeBytes = 8192;
        private const string PacketEncryptionKey = "";

        /// <summary>
        /// Returns the default UDP port number used by the game server.
        /// This may be overridden by the end-user.
        /// </summary>
        public static int DefaultGamePort
        {
            get { return ClientServerPort; }
        }

        /// <summary>
        /// Returns the default TCP port number used by the file transfer listener.
        /// This may be overridden by the end-user.
        /// </summary>
        public static int DefaultFileTransferPort
        {
            get { return FileTransferListenerPort; }
        }

        /// <summary>
        /// Returns the buffer size of file transfers.
        /// </summary>
        public static int FileTransferBufferSize
        {
            get { return FileTransferBufferSizeBytes; }
        }

        /// <summary>
        /// Returns the unique application ID used by the game server
        /// </summary>
        public static string ApplicationID
        {
            get
            {
                return ClientServerAppID;
            }
        }

        public static string EncryptionKey
        {
            get
            {
                return PacketEncryptionKey;
            }
        }
    }
}
