using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.BusinessObjects
{
    public class UserProfile
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public DateTime UserDOB { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
