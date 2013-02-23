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
        public bool IsServerRunning
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
            NetworkThread.DoWork += RunNetworkThread;

            Agent = new NetworkAgent(AgentRole.Server, ClientServerConfiguration.ApplicationID, ClientServerConfiguration.DefaultPort);
        }

        #endregion

        #region Events / Delegates

        public event EventHandler OnServerStart;
        public event EventHandler OnServerShutdown;

        #endregion

        #region Methods - Main Thread

        /// <summary>
        /// Starts the client-server server instance.
        /// </summary>
        public void Start()
        {
            try
            {
                IsServerRunning = true;
                NetworkThread.RunWorkerAsync();

                if (!Object.ReferenceEquals(OnServerStart, null))
                {
                    OnServerStart(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        /// <summary>
        /// Shuts down the client-server server instance.
        /// </summary>
        public void Shutdown()
        {
            try
            {
                IsServerRunning = false;
                NetworkThread.CancelAsync();

                if (!Object.ReferenceEquals(OnServerShutdown, null))
                {
                    OnServerShutdown(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Methods - Network Thread

        /// <summary>
        /// Handles checking for new messages from clients, processing them, and updating the game state.
        /// Also syncs with the master server 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunNetworkThread(object sender, DoWorkEventArgs e)
        {
            try
            {

                while (IsServerRunning)
                {
                    CheckForMessages();
                    Thread.Sleep(5);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Method: RunNetworkThread() in ClientServer.cs", ex);
            }
        }

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
