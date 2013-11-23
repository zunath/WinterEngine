using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataTransferObjects.Enums;
using WinterEngine.DataTransferObjects.Packets;

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
            Model.LogMessages.Add("Request Packet recieved: " + packet.RequestType);

            switch (packet.RequestType)
            {
                case PacketRequestTypeEnum.CharacterSelection:
                    ProcessCharacterSelectionRequest(packet);
                    break;
                case PacketRequestTypeEnum.CharacterCreation:
                    ProcessCharacterCreationRequest(packet);
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
            Agent.SendPacket(packet, connection, NetDeliveryMethod.ReliableSequenced);
        }

        #endregion

    }
}
