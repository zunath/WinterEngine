using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.Network.Packets;

namespace WinterEngine.Network.Clients
{
    public partial class GameNetworkClient
    {
        #region Fields

        private string _serverName;
        private string _serverAnnouncements;
        private List<PlayerCharacter> _playerCharacters;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the name of the server the user is currently connected to.
        /// </summary>
        public string ServerName
        {
            get { return _serverName; }
        }

        /// <summary>
        /// Returns the announcements specified by the server host.
        /// </summary>
        public string ServerAnnouncements
        {
            get { return _serverAnnouncements; }
        }

        /// <summary>
        /// Returns a list of player characters which exist on the active user's account.
        /// </summary>
        public List<PlayerCharacter> AccountPlayerCharacters
        {
            get
            {
                if (_playerCharacters == null)
                {
                    _playerCharacters = new List<PlayerCharacter>();
                }

                return _playerCharacters;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes a packet containing data related to the character selection screen.
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessCharacterSelectionPacket(CharacterSelectionPacket packet)
        {
            _playerCharacters = packet.PlayerList;
            _serverName = packet.ServerName;
            _serverAnnouncements = packet.ServerAnnouncement;
        }

        #endregion

    }
}
