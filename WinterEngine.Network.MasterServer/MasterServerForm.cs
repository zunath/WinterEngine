using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;
using System.Linq;
using WinterEngine.Network.Servers;

namespace WinterEngine.Network.MasterServer
{
    public partial class MasterServerForm : Form
    {
        #region Fields

        private LobbyServer _lobbyServer;
        private bool _serverRunning;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the master server entity.
        /// </summary>
        private LobbyServer Lobby
        {
            get { return _lobbyServer; }
        }

        /// <summary>
        /// Gets or sets whether the master server is running.
        /// </summary>
        private bool IsServerRunning
        {
            get { return _serverRunning; }
            set { _serverRunning = value; }
        }

        #endregion

        #region Constructors

        public MasterServerForm()
        {
            InitializeComponent();

            _lobbyServer = new LobbyServer();
        }

        #endregion

        #region Events / Delegates

        /// <summary>
        /// Delegate used to run RefreshServerList() on the main GUI thread.
        /// </summary>
        /// <param name="serverList">The latest version of the server list.</param>
        private delegate void ServerListUpdaterHandler(List<ServerDetails> serverList);

        #endregion

        #region Event Handling - Main Thread

        /// <summary>
        /// Handles starting and stopping the master server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartMasterServer_Click(object sender, EventArgs e)
        {
            if (IsServerRunning)
            {
                AddLogMessage("Stopping Master Server");
                IsServerRunning = false;
                Lobby.Shutdown();
                listBoxServers.Items.Clear();
                buttonStartMasterServer.Text = "Start Master Server";
                AddLogMessage("Master Server Stopped");
            }
            else
            {
                AddLogMessage("Starting Master Server");
                IsServerRunning = true;
                Lobby.Start();
                backgroundWorkerServerPolling.RunWorkerAsync();
                buttonStartMasterServer.Text = "Shutdown Master Server";
                AddLogMessage("Master Server Started");
            }
        }

        /// <summary>
        /// Handles loading the selected server's details to the controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxServers_SelectedValueChanged(object sender, EventArgs e)
        {
            ServerDetails serverDetails = listBoxServers.SelectedItem as ServerDetails;
            LoadServerDetails(serverDetails);
        }

        #endregion

        #region Methods - Main Thread

        /// <summary>
        /// Loads a server's details into the form's controls.
        /// If details is null, all fields will be cleared.
        /// </summary>
        /// <param name="details"></param>
        private void LoadServerDetails(ServerDetails details)
        {
            if (Object.ReferenceEquals(details, null))
            {
                textBoxDescription.Text = String.Empty;
                textBoxLastUpdateTime.Text = String.Empty;
                textBoxMaxLevel.Text = String.Empty;
                textBoxPing.Text = String.Empty;
                textBoxPort.Text = String.Empty;
                textBoxServerIPAddress.Text = String.Empty;
                textBoxServerName.Text = String.Empty;
            }
            else
            {
                textBoxDescription.Text = details.Description;
                textBoxMaxLevel.Text = Convert.ToString(details.MaxLevel);
                textBoxPing.Text = "" + details.Ping;
                textBoxPort.Text = "" + details.Connection.Port;
                textBoxServerIPAddress.Text = details.Connection.IP.ToString();
                textBoxServerName.Text = details.Name;
            }
        }

        /// <summary>
        /// Compares the servers in the list box against the latest server list.
        /// Removes old servers and adds any new ones. Existing servers are not modified.
        /// </summary>
        /// <param name="serverList"></param>
        private void RefreshServerList(List<ServerDetails> serverList)
        {
            // Converts all items in the list box to ServerDetails and generates a list out of them.
            List<ServerDetails> existingServers = listBoxServers.Items.Cast<ServerDetails>().ToList();

            List<ServerDetails> newServers = serverList.Except(existingServers).ToList();
            List<ServerDetails> removedServers = existingServers.Except(serverList).ToList();

            foreach (ServerDetails currentServer in newServers)
            {
                AddLogMessage("Adding server " + currentServer.Name);
                listBoxServers.Items.Add(currentServer);
            }

            foreach (ServerDetails currentServer in removedServers)
            {
                AddLogMessage("Removing server " + currentServer.Name);
                listBoxServers.Items.Remove(currentServer);
            }
        }

        /// <summary>
        /// Adds a message to the log screen.
        /// </summary>
        /// <param name="message"></param>
        private void AddLogMessage(string message)
        {
            textBoxLog.Text += message + Environment.NewLine;
        }

        #endregion

        #region Event Handling - Server Polling Thread

        /// <summary>
        /// Handles polling the master server object and refreshing the visible server
        /// list in the GUI periodically.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerServerPolling_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                while (IsServerRunning)
                {
                    List<ServerDetails> serverList = Lobby.GetServerList(); 

                    // Invoke the RefreshServerList method on the main list.
                    listBoxServers.Invoke(new ServerListUpdaterHandler(RefreshServerList), serverList);
                    
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
