using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class UsernameListEventArgs : EventArgs
    {
        public List<string> Usernames { get; set; }

        public UsernameListEventArgs()
        {
            this.Usernames = new List<string>();
        }

        public UsernameListEventArgs(List<string> usernames)
        {
            this.Usernames = usernames;
        }
    }
}
