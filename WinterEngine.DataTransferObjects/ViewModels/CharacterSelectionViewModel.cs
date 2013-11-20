using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataTransferObjects.ViewModels
{
    public class CharacterSelectionViewModel
    {
        public string ServerName { get; set; }
        public string Announcement { get; set; }
        public List<PlayerCharacter> Characters { get; set; }
        public int ActiveCharacterIndex { get; set; }
        public bool CanDeleteCharacters { get; set; }
        public List<string> CharacterPortraits { get; set; }
        public string DeleteCharacterResponseMessage { get; set; }

        [JsonIgnore]
        public PlayerCharacter ActiveCharacter
        {
            get
            {
                if (Characters.Count > 0)
                {
                    return Characters[ActiveCharacterIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public CharacterSelectionViewModel()
        {
            this.Characters = new List<PlayerCharacter>();
            this.CharacterPortraits = new List<string>();
        }

    }
}
