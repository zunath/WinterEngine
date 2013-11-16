﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.Network.Configuration;
using WinterEngine.DataTransferObjects.Packets;

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
