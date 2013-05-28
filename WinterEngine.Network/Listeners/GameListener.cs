using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Lidgren.Network;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Listeners
{
    public class GameListener
    {
        #region Fields

        private NetworkAgent _agent;
        private List<ContentPackage> _contentPackages;
        private List<string> _contentPackageNames;
        private List<string> _contentPackagePaths;
        private List<Packet> _incomingPackets;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the network agent.
        /// </summary>
        private NetworkAgent Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        private List<ContentPackage> ContentPackageList
        {
            get { return _contentPackages; }
        }

        private List<string> ContentPackageFileNames
        {
            get { return _contentPackageNames; }
        }

        private List<string> ContentPackageFilePaths
        {
            get { return _contentPackagePaths; }
        }

        private List<Packet> IncomingPackets
        {
            get { return _incomingPackets; }
            set { _incomingPackets = value; }
        }

        #endregion

        #region Constructors

        public GameListener(int customPort, List<ContentPackage> contentPackages)
        {
            if (customPort <= 0)
            {
                customPort = GameServerConfiguration.DefaultGamePort;
            }

            Agent = new NetworkAgent(AgentRole.Server, GameServerConfiguration.ApplicationID, customPort);
            this._contentPackages = contentPackages;

            _contentPackageNames = new List<string>();
            _contentPackagePaths = new List<string>();
            foreach (ContentPackage package in contentPackages)
            {
                _contentPackageNames.Add(package.FileName);
                _contentPackagePaths.Add(DirectoryPaths.ContentPackageDirectoryPath + package.FileName);
            }

        }

        #endregion

        #region Methods

        public void Process()
        {
            // Checks for messages and processes them.
            IncomingPackets = Agent.CheckForPackets();

            foreach (Packet packet in IncomingPackets)
            {
                ProcessPacket(packet);
            }
            Thread.Sleep(5);
        }

        /// <summary>
        /// Handles processing packets depending on their type.
        /// If a packet recieved does not match one of the supported types,
        /// it will be dropped.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="factory"></param>
        private void ProcessPacket(Packet packet)
        {
            switch (packet.PacketType)
            {
                case PacketTypeEnum.Request:
                    ProcessRequest(packet as RequestPacket);
                    break;
                default:
                    // Invalid packet type.
                    break;
            }
        }

        /// <summary>
        /// Processes a request, sending data to sender if necessary.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessRequest(RequestPacket packet)
        {
            switch (packet.RequestType)
            {
                case RequestTypeEnum.ServerContentPackageList:
                    SendContentPackageList(packet);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Methods - Packet Processing

        private void SendContentPackageList(Packet receivedPacket)
        {
            ContentPackageListPacket packet = new ContentPackageListPacket
            {
                FileNames = ContentPackageFileNames
            };
            Agent.WriteMessage(packet);
            Agent.SendMessage(receivedPacket.SenderConnection, true);
            
        }

        #endregion
    }
}
