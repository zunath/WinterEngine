using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
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

        private void ProcessCharacterCreationRequest(RequestPacket packet)
        {
            List<Gender> genderList;
            List<Race> raceList;

            using (GenderRepository repo = new GenderRepository())
            {
                genderList = repo.GetAll();
            }

            using (RaceRepository repo = new RaceRepository())
            {
                raceList = repo.GetAll();
            }

            CharacterCreationPacket responsePacket = new CharacterCreationPacket
            {
                GenderList = genderList,
                RaceList = raceList
            };

            Agent.SendPacket(responsePacket, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
