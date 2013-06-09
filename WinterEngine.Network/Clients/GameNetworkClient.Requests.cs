using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Clients
{
    public partial class GameNetworkClient
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        private void ProcessRequest(RequestPacket packet)
        {
            switch (packet.RequestType)
            {
                case RequestTypeEnum.Username:
                    ProcessUsernameRequest();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sends the username of the client when requested by the server.
        /// </summary>
        private void ProcessUsernameRequest()
        {
            UsernamePacket packet = new UsernamePacket
            {
                Username = this.Username
            };
            Agent.SendPacket(packet, ServerConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
