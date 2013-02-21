using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Lidgren.Network;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Servers
{
    public class ClientServer
    {
        #region Fields

        private BackgroundWorker _networkThread;
        private NetworkAgent _agent;
        private bool _isServerRunning;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the network thread used for packet processing.
        /// </summary>
        private BackgroundWorker NetworkThread
        {
            get { return _networkThread; }
            set { _networkThread = value; }
        }

        /// <summary>
        /// Gets or sets the network agent.
        /// </summary>
        private NetworkAgent Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        /// <summary>
        /// Gets or sets whether the client server is running.
        /// </summary>
        private bool IsServerRunning
        {
            get { return _isServerRunning; }
            set { _isServerRunning = value; }
        }

        #endregion

        #region Constructors

        public ClientServer()
        {
            NetworkThread = new BackgroundWorker();
            NetworkThread.WorkerSupportsCancellation = true;

            Agent = new NetworkAgent(AgentRole.Server, ClientServerConfiguration.ApplicationID);
        }

        #endregion

        #region Methods - Main Thread

        /// <summary>
        /// Starts the client-server server instance.
        /// </summary>
        public void Start()
        {
            IsServerRunning = true;
            NetworkThread.RunWorkerAsync();
        }

        /// <summary>
        /// Shuts down the client-server server instance.
        /// </summary>
        public void Shutdown()
        {
            IsServerRunning = false;
            NetworkThread.CancelAsync();
        }

        #endregion

        #region Methods - Network Thread

        /// <summary>
        /// Checks for messages and processes them.
        /// </summary>
        private void CheckForMessages()
        {
            try
            {
                PacketFactory factory = new PacketFactory();
                List<NetIncomingMessage> messageList;
                while (IsServerRunning)
                {
                    messageList = Agent.CheckForMessages();

                    foreach (NetIncomingMessage message in messageList)
                    {
                        ProcessPacket(message, factory);
                    }

                    Thread.Sleep(5);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
