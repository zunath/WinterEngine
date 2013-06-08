using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.Network.Enums;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Listeners
{
    public partial class GameNetworkListener
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods


        /// <summary>
        /// Processes a request, sending data to sender if necessary.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessRequest(RequestPacket packet)
        {
            RaiseOnLogMessageEvent("Request Packet received: " + packet.RequestType);

            switch (packet.RequestType)
            {
                case RequestTypeEnum.CharacterSelection:
                    ProcessCharacterSelectionRequest();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sends a packet containing the list of file names to the sender of the received packet.
        /// </summary>
        /// <param name="receivedPacket"></param>
        private void SendContentPackageList(NetConnection connection)
        {
            ContentPackageListPacket packet = new ContentPackageListPacket
            {
                FileNames = ContentPackageFileNames
            };
            Agent.WriteMessage(packet);
            Agent.SendMessage(connection, NetDeliveryMethod.ReliableSequenced);

        }

        #endregion

    }
}
