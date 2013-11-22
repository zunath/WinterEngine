using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.ViewModels
{
    public class CharacterCreationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int RaceID { get; set; }
        public int PortraitID { get; set; }
        public int GenderID { get; set; }
        public int CharacterClassID { get; set; }
        public List<Race> Races { get; set; }
        public List<Gender> Genders { get; set; }
    }
}
