using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Lidgren.Network;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.Network.Configuration;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Clients
{
    public class GameNetworkClient
    {
        #region Fields

        private NetworkAgent _agent;
        private List<NetIncomingMessage> _incomingMessages;
        private NetConnection _serverConnection;

        #endregion

        #region Properties

        private NetworkAgent Agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        private List<NetIncomingMessage> IncomingMessages
        {
            get { return _incomingMessages; }
            set { _incomingMessages = value; }
        }

        public bool IsConnected
        {
            get
            {
                if (Agent.Connections.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private NetConnection ServerConnection
        {
            get
            {
                if (IsConnected)
                {
                    return Agent.Connections[0];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Constructors

        public GameNetworkClient(ConnectionAddress address)
        {
            Agent = new NetworkAgent(AgentRoleEnum.Client, GameServerConfiguration.ApplicationID, address.ServerPort);
            Agent.Connect(address.ServerIPAddress);
        }
        
        #endregion

        #region Public Methods

        public void Process()
        {
            IncomingMessages = Agent.CheckForMessages();

            foreach (NetIncomingMessage message in IncomingMessages)
            {
                ProcessPacket(message);
            }

            RequestPacket packet = new RequestPacket(RequestTypeEnum.ServerContentPackageList);
            Agent.WriteMessage(packet);
            Agent.SendMessage(ServerConnection, NetDeliveryMethod.ReliableSequenced);

            Thread.Sleep(5);
        }

        public void Disconnect()
        {
            Agent.Shutdown();
        }

        #endregion

        #region Private Methods

        private void ProcessPacket(NetIncomingMessage message)
        {
        }

        #endregion
    }
}
