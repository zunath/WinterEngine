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
using Lidgren.Network;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.MasterServer
{
    public partial class MasterServerForm : Form
    {
        #region Fields

        private NetworkAgent _agent; 
        private List<NetIncomingMessage> _messages;
        private PacketFactory _packetFactory;
        private Dictionary<Tuple<IPAddress, int>, ServerDetails> _serverList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the server list.
        /// Key: IP Address + Port #
        /// </summary>
        private Dictionary<Tuple<IPAddress, int>, ServerDetails> ServerList
        {
            get { return _serverList; }
            set { _serverList = value; }
        }

        #endregion

        #region Constructors

        public MasterServerForm()
        {
            InitializeComponent();

            _packetFactory = new PacketFactory();

            _serverList = new Dictionary<Tuple<IPAddress, int>, ServerDetails>();
        }

        #endregion

        #region Event Handling - Main Thread

        private void buttonStartMasterServer_Click(object sender, EventArgs e)
        {
            _agent = new NetworkAgent(AgentRole.Server, MasterServerConfiguration.MasterServerApplicationIdentifier);
            backgroundWorkerNetwork.RunWorkerAsync();
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
            _messages = _agent.CheckForMessages();
            Thread.Sleep(5);
        }

        /// <summary>
        /// Packet processing is done on the main thread.
        /// Perform a recursive call to the background worker to check for messages again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerNetwork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (NetIncomingMessage message in _messages)
            {
                ProcessPacket(message);
            }
            _messages.Clear();

            backgroundWorkerNetwork.RunWorkerAsync();
        }

        #endregion

        #region Methods - Main Thread

        /// <summary>
        /// Handles processing packets depending on their type.
        /// If a packet received does not match one of the supported types,
        /// it will be dropped.
        /// </summary>
        /// <param name="message"></param>
        private void ProcessPacket(NetIncomingMessage message)
        {
            Packet packet = _packetFactory.BuildPacket(message);

            switch (packet.PacketType)
            {
                case PacketTypeEnum.Server:
                    int port = message.SenderEndPoint.Port;
                    IPAddress ipAddress= message.SenderEndPoint.Address;
                    float ping = message.SenderConnection.AverageRoundtripTime;
                    UpdateServerInformation(packet as ServerPacket, ipAddress, port, ping);
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
        private void UpdateServerInformation(ServerPacket packet, IPAddress ipAddress, int port, float ping)
        {
            ServerDetails details = new ServerDetails();
            details.Description = packet.Description;
            details.MaxLevel = packet.MaxLevel;
            details.MinLevel = packet.MinLevel;
            details.Name = packet.Name;

            details.IPAddress = ipAddress;
            details.Port = port;
            details.Ping = ping;

            Tuple<IPAddress, int> key = new Tuple<IPAddress,int>(ipAddress, port);
            if (ServerList.ContainsKey(key))
            {
                ServerList[key] = details;
            }
            else
            {
                ServerList.Add(key, details);
            }


            textBoxDescription.Text = details.Description;
            textBoxMinLevel.Text = details.MinLevel + " - " + details.MaxLevel;
            textBoxPing.Text = "" +details.Ping;
            textBoxPort.Text = "" + details.Port;
            textBoxServerIPAddress.Text = details.IPAddress.ToString();
            textBoxServerName.Text = details.Name;

        }

        #endregion

    }
}
