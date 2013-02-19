using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using Lidgren.Network;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.MasterServer
{
    public partial class MasterServerForm : Form
    {
        #region Fields

        private NetworkAgent _agent; 
        private Dictionary<ConnectionAddress, ServerDetails> _serverList;
        private bool _masterServerIsRunning;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Network Agent.
        /// </summary>
        private NetworkAgent Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        /// <summary>
        /// Gets or sets whether the master server is running.
        /// </summary>
        private bool IsMasterServerRunning
        {
            get { return _masterServerIsRunning; }
            set { _masterServerIsRunning = value; }
        }

        /// <summary>
        /// Gets or sets the server list.
        /// Key: IP Address + Port #
        /// </summary>
        private Dictionary<ConnectionAddress, ServerDetails> ServerList
        {
            get { return _serverList; }
            set { _serverList = value; }
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

            ServerList = new Dictionary<ConnectionAddress, ServerDetails>();
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
            if (IsMasterServerRunning)
            {
                bool shuttingDown = true;
                IsMasterServerRunning = false;
                buttonStartMasterServer.Text = "Shutting Down...";
                buttonStartMasterServer.Enabled = false;
                backgroundWorkerNetwork.CancelAsync();
                backgroundWorkerStatusTracker.CancelAsync();
                Agent.Shutdown();

                while (shuttingDown)
                {
                    if (!backgroundWorkerNetwork.IsBusy && !backgroundWorkerStatusTracker.IsBusy)
                    {
                        shuttingDown = false;
                    }
                }

                buttonStartMasterServer.Text = "Start Master Server";
                buttonStartMasterServer.Enabled = true;
            }
            else
            {
                IsMasterServerRunning = true;
                Agent = new NetworkAgent(AgentRole.Server, MasterServerConfiguration.MasterServerApplicationIdentifier);
                backgroundWorkerNetwork.RunWorkerAsync();
                backgroundWorkerStatusTracker.RunWorkerAsync();
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

        #region Event Handling - Network Processing Thread
        
        /// <summary>
        /// Handles checking for messages each cycle.
        /// The thread is put to sleep temporarily to prevent CPU usage spikes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerNetwork_DoWork(object sender, DoWorkEventArgs e)
        {
            List<NetIncomingMessage> messages;
            PacketFactory factory = new PacketFactory();

            while (IsMasterServerRunning)
            {
                messages = Agent.CheckForMessages();
                foreach (NetIncomingMessage message in messages)
                {
                    ProcessPacket(message, factory);
                }
                messages.Clear();

                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// Packet processing is done on the main thread.
        /// Perform a recursive call to the background worker to check for messages again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerNetwork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            

            if (IsMasterServerRunning)
            {
                //backgroundWorkerNetwork.RunWorkerAsync();
            }
        }

        #endregion

        #region Event Handling - Server Status Tracking Thread

        /// <summary>
        /// Handles updating server status and removing servers that have not responded
        /// after a configurable amount of time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerStatusTracker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsMasterServerRunning)
            {
                List<ServerDetails> servers = ServerList.Values.ToList();
                DateTime currentTime = DateTime.Now;

                for (int index = servers.Count; index > 0; index--)
                {
                    ServerDetails server = servers[index];
                    // Check if the server hasn't been updated in a specific amount of time.
                    TimeSpan difference = currentTime.Subtract(server.LastPacketReceived);
                    if (difference.Minutes >= ServerTimeoutMinutes)
                    {
                        servers.RemoveAt(index);
                        ServerList.Remove(server.Connection);
                    }
                }

                listBoxServers.DataSource = ServerList.ToList();
                Thread.Sleep(5000);
            }
            e.Cancel = true;
        }

        private void backgroundWorkerStatusTracker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (IsMasterServerRunning)
            {
                //backgroundWorkerStatusTracker.RunWorkerAsync();
            }
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

        /// <summary>
        /// Adds a server's details to the server list if it doesn't exist already.
        /// Updates a server's details if it already exists in the server list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="details"></param>
        private void UpsertServer(ConnectionAddress key, ServerDetails details)
        {
            if (ServerList.ContainsKey(key))
            {
                ServerList[key] = details;
            }
            else
            {
                ServerList.Add(key, details);
                listBoxServers.Items.Add(details);
            }
        }

        #endregion

        #region Packet Processing

        /// <summary>
        /// Handles processing packets depending on their type.
        /// If a packet received does not match one of the supported types,
        /// it will be dropped.
        /// </summary>
        /// <param name="message"></param>
        private void ProcessPacket(NetIncomingMessage message, PacketFactory factory)
        {
            Packet packet = factory.BuildPacket(message);

            switch (packet.PacketType)
            {
                case PacketTypeEnum.Server:
                    int port = message.SenderEndPoint.Port;
                    IPAddress ipAddress = message.SenderEndPoint.Address;
                    float ping = message.SenderConnection.AverageRoundtripTime;
                    UpdateServerInformation(packet as ServerDetailsPacket, ipAddress, port, ping);
                    break;
                case PacketTypeEnum.Request:
                    ProcessRequest(packet as RequestPacket);
                    break;
                default:
                    // Invalid packet type.
                    break;
            }
        }

        /// <summary>
        /// Adds or updates a server object in the list box
        /// based on data received over the network.
        /// </summary>
        /// <param name="packet"></param>
        private void UpdateServerInformation(ServerDetailsPacket packet, IPAddress ipAddress, int port, float ping)
        {
            ServerDetails details = new ServerDetails();
            details.Description = packet.Description;
            details.MaxLevel = packet.MaxLevel;
            details.MinLevel = packet.MinLevel;
            details.Name = packet.Name;
            details.Connection.IP = ipAddress;
            details.Connection.Port = port;
            details.Ping = ping;
            
            UpsertServer(details.Connection, details);
        }

        /// <summary>
        /// Processes a request, sending data to sender if necessary.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessRequest(RequestPacket packet)
        {
            switch (packet.RequestType)
            {
                case RequestTypeEnum.MasterServerList:
                    break;

                default:
                    break;
            }
        }

        #endregion


    }
}
