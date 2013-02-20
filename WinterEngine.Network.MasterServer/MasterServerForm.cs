using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;
using System.Linq;

namespace WinterEngine.Network.MasterServer
{
    public partial class MasterServerForm : Form
    {
        #region Fields

        private MasterServerEntity _masterServer;
        private bool _serverRunning;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the master server entity.
        /// </summary>
        private MasterServerEntity MasterServer
        {
            get { return _masterServer; }
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

            _masterServer = new MasterServerEntity();
        }

        #endregion

        #region Events / Delegates

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
                IsServerRunning = false;
                MasterServer.ShutdownMasterServer();
                buttonStartMasterServer.Text = "Start Master Server";
            }
            else
            {
                IsServerRunning = true;
                MasterServer.StartMasterServer();
                backgroundWorkerServerPolling.RunWorkerAsync();
                buttonStartMasterServer.Text = "Shutdown Master Server";
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
                textBoxMinLevel.Text = String.Empty;
                textBoxPing.Text = String.Empty;
                textBoxPort.Text = String.Empty;
                textBoxServerIPAddress.Text = String.Empty;
                textBoxServerName.Text = String.Empty;
            }
            else
            {
                textBoxDescription.Text = details.Description;
                textBoxMinLevel.Text = details.MinLevel + " - " + details.MaxLevel;
                textBoxPing.Text = "" + details.Ping;
                textBoxPort.Text = "" + details.Connection.Port;
                textBoxServerIPAddress.Text = details.Connection.IP.ToString();
                textBoxServerName.Text = details.Name;
            }
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
                    List<ServerDetails> serverList = MasterServer.GetServerList(); 
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

        #region Methods - Server Polling Thread

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
                listBoxServers.Items.Add(currentServer);
            }

            foreach (ServerDetails currentServer in removedServers)
            {
                listBoxServers.Items.Remove(currentServer);
            }
        }

        #endregion

    }
}
