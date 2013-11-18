using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.BusinessObjects
{
    public class LoginCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginCredentials(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }

    }
}
