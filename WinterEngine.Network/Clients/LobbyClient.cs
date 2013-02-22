using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Entities;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Clients
{
    public class LobbyClient
    {
        #region Fields

        private NetworkAgent _masterAgent;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the master network agent.
        /// </summary>
        private NetworkAgent MasterAgent
        {
            get { return _masterAgent; }
            set { _masterAgent = value; }
        }


        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new LobbyClient object
        /// </summary>
        public LobbyClient()
        {
            MasterAgent = new NetworkAgent(AgentRole.Client, LobbyServerConfiguration.ApplicationID, MasterServerConfiguration.Port);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects this client to the master server.
        /// </summary>
        public void Connect()
        {
            try
            {
                MasterAgent.Connect(MasterServerConfiguration.MasterServerURL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Syncs server information details with the master server.
        /// </summary>
        /// <param name="serverDetails"></param>
        public void SyncWithLobbyServer(ServerDetails serverDetails)
        {
            ServerDetailsPacket serverDetailsPacket = new ServerDetailsPacket(serverDetails.Name, serverDetails.Description, serverDetails.MaxLevel);
            MasterAgent.WriteMessage(serverDetailsPacket);
            MasterAgent.SendMessage(MasterAgent.Connections[0]);
        }

        #endregion
    }
}
