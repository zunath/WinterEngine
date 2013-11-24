using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Packets
{
    public class UserValidationPacket
    {
        public string Username { get; set; }
        public string ActiveAuthorizationToken { get; set; }
        public string ServerIPAddress { get; set; }
        public int ServerPort { get; set; }

        public UserValidationPacket()
        {
            this.Username = "";
            this.ActiveAuthorizationToken = "";
            this.ServerIPAddress = "";
            this.ServerPort = 0;
        }
    }
}
