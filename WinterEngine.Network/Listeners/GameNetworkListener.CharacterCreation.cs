using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Packets;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Network.Listeners
{
    public partial class GameNetworkListener
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        private void ProcessCharacterCreationInitializationRequest(RequestPacket packet)
        {
            List<Race> raceList;
            List<Gender> genderList;
            List<CharacterClass> classList;
            List<Ability> abilityList;
            List<Skill> skillList;

            using (RaceRepository repo = new RaceRepository())
            {
                raceList = repo.GetAll();
            }
            using (GenderRepository repo = new GenderRepository())
            {
                genderList = repo.GetAll();
            }

            using (CharacterClassRepository repo = new CharacterClassRepository())
            {
                classList = repo.GetAll();
            }

            using (AbilityRepository repo = new AbilityRepository())
            {
                abilityList = repo.GetAll();
            }

            using (SkillRepository repo = new SkillRepository())
            {
                skillList = repo.GetAll();
            }

            CharacterCreationInitializationPacket responsePacket = new CharacterCreationInitializationPacket
            {
                RaceList = raceList,
                GenderList = genderList,
                ClassList = classList,
                AbilityList = abilityList,
                SkillList = skillList
            };

            Agent.SendPacket(responsePacket, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        private void ProcessCharacterCreationRequest(NewCharacterPacket packet)
        {
            SuccessFailEnum success = SuccessFailEnum.Success;

            PlayerCharacter character = new PlayerCharacter
            {
                Age = packet.Age,
                CharacterClassID = packet.CharacterClassID,
                Constitution = packet.Constitution,
                Dexterity = packet.Dexterity,
                FirstName = packet.FirstName,
                GenderID = packet.GenderID,
                Intelligence = packet.Intelligence,
                IsDefault = false,
                IsGameMaster = false,
                IsSystemResource = false,
                LastName = packet.LastName,
                Level = 1,
                PortraitID = packet.PortraitID,
                RaceID = packet.RaceID,
                Strength = packet.Strength,
                Wisdom = packet.Wisdom
            };

            using (PlayerCharacterFileAccess access = new PlayerCharacterFileAccess())
            {
                access.SerializePlayerCharacterFile(character, Model.ConnectionUsernamesDictionary[packet.SenderConnection]);
            }

            CharacterCreationResponsePacket response = new CharacterCreationResponsePacket
            {
                SuccessType = success
            };

            Agent.SendPacket(response, packet.SenderConnection, NetDeliveryMethod.ReliableUnordered);
        }

        #endregion
    }
}
