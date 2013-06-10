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
        private const string PacketEncryptionKey = "supersecretpassword"; // Change for release

        #if DEBUG
        private const float LidgrenSimulatedPacketLoss = 0.0f;
        private const float LidgrenSimulatedDuplicates = 0.0f;
        private const float LidgrenSimulatedMinimumLatency = 0.4f;
        private const float LidgrenSimulatedRandomLatency = 0.0f;
        #endif

        /// <summary>
        /// Returns the default UDP port number used by the game server.
        /// This may be overridden by the end-user.
        /// </summary>
        public static int DefaultGamePort
        {
            get { return ClientServerPort; }
        }

        /// <summary>
        /// Returns the default UDP port number used by the file transfer listener.
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

        /// <summary>
        /// Returns the encryption key used for packet transfer.
        /// </summary>
        public static string EncryptionKey
        {
            get
            {
                return PacketEncryptionKey;
            }
        }

        #if DEBUG
        public static float SimulatedPacketLoss
        {
            get { return LidgrenSimulatedPacketLoss; }
        }

        public static float SimulatedDuplicates
        {
            get { return LidgrenSimulatedDuplicates; }
        }

        public static float SimulatedMinimumLatency
        {
            get { return LidgrenSimulatedMinimumLatency; }
        }

        public static float SimulatedRandomLatency
        {
            get { return LidgrenSimulatedRandomLatency; }
        }
        #endif
    }
}
