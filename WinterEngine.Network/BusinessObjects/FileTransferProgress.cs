using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace WinterEngine.Network.BusinessObjects
{
    public class FileTransferProgress
    {
        public int BytesSent { get; set; }
        public string FilePath { get; set; }
    }
}
