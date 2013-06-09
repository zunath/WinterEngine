using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataTransferObjects.GameObjects;
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
        /// Returns packet to sender containing the server's announcement, name and the sender's character information.
        /// </summary>
        private void ProcessCharacterSelectionRequest(RequestPacket packet)
        {
            List<PlayerCharacter> characterList = new List<PlayerCharacter>();

            using (PlayerCharacterRepository repo = new PlayerCharacterRepository())
            {
            }

            CharacterSelectionPacket resultPacket = new CharacterSelectionPacket
            {
                ServerAnnouncement = ServerDetails.ServerAnnouncement,
                ServerName = ServerDetails.ServerName
            };

            Agent.SendPacket(resultPacket, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
