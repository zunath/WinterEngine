using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Lidgren.Network;
using WinterEngine.Network.Clients;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Servers
{
    public class GameServer
    {
        #region Fields

        private NetworkAgent _agent;

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

        #endregion

        #region Constructors

        public GameServer()
        {
            Agent = new NetworkAgent(AgentRole.Server, ClientServerConfiguration.ApplicationID, ClientServerConfiguration.DefaultPort);
        }

        #endregion

        #region Methods - Main Thread

        public void Process()
        {
            CheckForMessages();
            Thread.Sleep(5);
        }

        #endregion

        #region Methods - Network Thread

        /// <summary>
        /// Checks for messages and processes them.
        /// </summary>
        private void CheckForMessages()
        {
            PacketFactory factory = new PacketFactory();
            List<NetIncomingMessage> messageList;
            messageList = Agent.CheckForMessages();

            foreach (NetIncomingMessage message in messageList)
            {
                ProcessPacket(message, factory);
            }
        }

        /// <summary>
        /// Handles processing packets depending on their type.
        /// If a packet recieved does not match one of the supported types,
        /// it will be dropped.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="factory"></param>
        private void ProcessPacket(NetIncomingMessage message, PacketFactory factory)
        {
            Packet packet = factory.BuildPacket(message);

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
        }

        #endregion

    }
}
