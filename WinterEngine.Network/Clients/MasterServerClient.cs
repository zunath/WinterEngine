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
    public class MasterServerClient
    {
        #region Properties

        private WinterServer ActiveServer { get; set; }
        private WebServiceClientUtility WebServiceUtility { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new LobbyClient object
        /// </summary>
        public MasterServerClient()
        {
            WebServiceUtility = new WebServiceClientUtility();
        }

        #endregion

        #region Methods

        public void Process(WinterServer server)
        {
            ActiveServer = server;

            // Sync with the master server
            WebServiceUtility.SendServerDetails(ActiveServer);
            Thread.Sleep(MasterServerConfiguration.SyncDelaySeconds * 1000);
        }

        #endregion
    }
}
