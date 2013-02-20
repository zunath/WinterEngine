using System;
using System.Configuration;
using System.Net;
using System.Windows.Forms;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;

namespace WinterEngine.Network.MasterServer
{
    public partial class MasterServerForm : Form
    {
        #region Fields

        private MasterServerEntity _masterServer;

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
        /// Returns the number of minutes it takes to remove a server from the list
        /// due to no status update.
        /// </summary>
        private int ServerTimeoutMinutes
        {
            get { return Convert.ToInt16(ConfigurationManager.AppSettings["ServerTimeoutMinutes"]); }
        }

        #endregion

        #region Constructors

        public MasterServerForm()
        {
            InitializeComponent();

            _masterServer = new MasterServerEntity(MasterServerConfiguration.MasterServerIPAddress, MasterServerConfiguration.Port, MasterServerConfiguration.MasterServerApplicationIdentifier, ServerTimeoutMinutes);
        }

        #endregion

        #region Event Handling - Main Thread

        /// <summary>
        /// Handles starting and stopping the master server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartMasterServer_Click(object sender, EventArgs e)
        {
            MasterServer.StartMasterServer();
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

        private void button1_Click(object sender, EventArgs e)
        {
            listBoxServers.DataSource = MasterServer.GetServerList();
        }

    }
}
