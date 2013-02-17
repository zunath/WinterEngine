using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lidgren.Network;

namespace WinterEngine.Network.MasterServer
{
    public partial class MasterServerForm : Form
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Returns the IPEndPoint of the Master Server.
        /// </summary>
        private IPEndPoint MasterServerIPEndPoint
        {
            get 
            {
                string url = ConfigurationManager.AppSettings["MasterServerIPAddress"];
                string port = ConfigurationManager.AppSettings["MasterServerPort"];

                IPAddress ipAddress = Dns.GetHostAddresses(url)[0];
                IPEndPoint endPoint = new IPEndPoint(ipAddress, Convert.ToInt16(port));

                return endPoint;
            }
        }

        #endregion

        #region Constructors

        public MasterServerForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling - GUI Thread

        #endregion

        #region Event Handling - Network Processing Thread

        private void backgroundWorkerNetwork_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorkerNetwork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorkerNetwork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        #endregion
    }
}
