﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Paths;
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
        /// Returns packet to sender containing the server's announcement, name and the sender's character information.
        /// </summary>
        private void ProcessCharacterSelectionRequest(RequestPacket packet)
        {
            List<PlayerCharacter> characterList = new List<PlayerCharacter>();

            if (Model.ConnectionUsernamesDictionary.ContainsKey(packet.SenderConnection))
            {
                string username = Model.ConnectionUsernamesDictionary[packet.SenderConnection];
                using (PlayerCharacterFileAccess repo = new PlayerCharacterFileAccess())
                {
                    characterList = repo.GetCharactersByUsername(username);
                }

                CharacterSelectionPacket responsePacket = new CharacterSelectionPacket
                {
                    ServerAnnouncement = Model.ServerAnnouncement,
                    ServerName = Model.ServerName,
                    CharacterList = characterList,
                    CanDeleteCharacters = Model.IsCharacterDeletionEnabled
                };

                Agent.SendPacket(responsePacket, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
            }
        }

        /// <summary>
        /// Processes a delete character request and returns a response.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessDeleteCharacterRequest(DeleteCharacterPacket packet)
        {
            DeleteCharacterTypeEnum responseType = DeleteCharacterTypeEnum.Denied;

            if (Model.IsCharacterDeletionEnabled)
            {
                string filePath = DirectoryPaths.CharacterVaultDirectoryPath + Model.ConnectionUsernamesDictionary[packet.SenderConnection] + "/" + packet.FileName;

                using(PlayerCharacterFileAccess repo = new PlayerCharacterFileAccess())
                {
                    responseType = repo.DeletePlayerCharacterFile(filePath);
                }
            }
            else
            {
                responseType = DeleteCharacterTypeEnum.DeniedDisabled;
            }

            DeleteCharacterPacket response = new DeleteCharacterPacket(packet.FileName, responseType);
            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
