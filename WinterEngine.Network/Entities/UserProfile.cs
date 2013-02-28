using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Entities
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public DateTime UserBirthday { get; set; }
        public UserProfileGenderTypeEnum UserGender { get; set; }
    }
}
