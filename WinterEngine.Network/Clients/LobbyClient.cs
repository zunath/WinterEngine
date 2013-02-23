using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Clients
{
    public class LobbyClient
    {
        #region Fields

        private ServerDetails _serverDetails;
        private NetworkAgent _masterAgent;
        private BackgroundWorker _connectionThread;
        private bool _isConnectionRunning;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the server information details.
        /// </summary>
        private ServerDetails ServerInformation
        {
            get { return _serverDetails; }
            set { _serverDetails = value; }
        }

        /// <summary>
        /// Gets or sets the master network agent.
        /// </summary>
        private NetworkAgent MasterAgent
        {
            get { return _masterAgent; }
            set { _masterAgent = value; }
        }

        /// <summary>
        /// Gets or sets the connection thread background worker
        /// </summary>
        private BackgroundWorker ConnectionThread
        {
            get { return _connectionThread; }
            set { _connectionThread = value; }
        }

        /// <summary>
        /// Gets or sets whether the client connection is running.
        /// </summary>
        private bool IsConnectionRunning
        {
            get { return _isConnectionRunning; }
            set { _isConnectionRunning = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new LobbyClient object
        /// </summary>
        public LobbyClient()
        {
            MasterAgent = new NetworkAgent(AgentRole.Client, LobbyServerConfiguration.ApplicationID, MasterServerConfiguration.Port);

            ConnectionThread = new BackgroundWorker();
            ConnectionThread.DoWork += ProcessConnection;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the connection to the master server's lobby,
        /// periodically sending server information updates.
        /// </summary>
        public void Start(ServerDetails serverDetails)
        {
            try
            {
                IsConnectionRunning = true;
                this.ServerInformation = serverDetails;
                MasterAgent.Connect(MasterServerConfiguration.MasterServerURL);
                ConnectionThread.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Unable to connect to master server. Method: Start() in LobbyClient", ex);
            }
        }

        /// <summary>
        /// Stops the connection to the master server's lobby.
        /// </summary>
        public void Shutdown()
        {
            try
            {
                IsConnectionRunning = false;
                MasterAgent.Shutdown();
            }
            catch(Exception ex)
            {
                throw new Exception("Error: Unable to shutdown connection to master server. Method: Shutdown() in LobbyClient", ex);
            }
        }

        /// <summary>
        /// Processes the connection to the master server's lobby, periodically sending server information updates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessConnection(object sender, EventArgs e)
        {
            try
            {
                SyncWithLobbyServer();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Unable to process connection to master server. Method: ProcessConnection in LobbyClient", ex);
            }
        }

        /// <summary>
        /// Syncs server information details with the master server.
        /// </summary>
        /// <param name="serverDetails"></param>
        private void SyncWithLobbyServer()
        {
            while (IsConnectionRunning)
            {
                // Only attempt to send data if the connection is established.
                if (MasterAgent.Connections.Count > 0)
                {
                    ServerDetailsPacket serverDetailsPacket = new ServerDetailsPacket(ServerInformation.Name, ServerInformation.Description, ServerInformation.MaxLevel);
                    MasterAgent.WriteMessage(serverDetailsPacket);
                    MasterAgent.SendMessage(MasterAgent.Connections[0]);
                }
                 
                Thread.Sleep(500);
            }
        }

        #endregion
    }
}
