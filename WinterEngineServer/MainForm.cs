using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Lidgren.Network;
using WinterEngine.Network;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Packets;

namespace WinterEngineServer
{
    public partial class MainForm : Form
    {
        NetworkAgent agent;
        
        public MainForm()
        {
            InitializeComponent();

            agent = new NetworkAgent(AgentRole.Client, MasterServerConfiguration.MasterServerApplicationIdentifier);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ServerDetailsPacket packet = new ServerDetailsPacket("TestName", "TestDesc", 2, 3);
            agent.WriteMessage(packet);
            agent.SendMessage(agent.Connections[0], true);
            
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            agent.Connect("localhost");

        }
    }
}
