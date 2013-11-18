using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataTransferObjects.ViewModels
{
    public class CharacterSelectionViewModel
    {
        public string ServerName { get; set; }
        public string Announcement { get; set; }
        public List<PlayerCharacter> Characters { get; set; }
        public PlayerCharacter ActiveCharacter { get; set; }
        public bool CanDeleteCharacters { get; set; }

    }
}
