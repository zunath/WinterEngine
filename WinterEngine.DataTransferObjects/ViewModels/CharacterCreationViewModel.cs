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
        public List<Race> RaceList { get; set; }
        public List<Gender> GenderList { get; set; }

        public CharacterCreationViewModel()
        {
            this.FirstName = "";
            this.LastName = "";
            this.Age = 0;
            this.RaceID = 0;
            this.PortraitID = 0;
            this.GenderID = 0;
            this.CharacterClassID = 0;
            this.RaceList = new List<Race>();
            this.GenderList = new List<Gender>();
        }
    }
}
