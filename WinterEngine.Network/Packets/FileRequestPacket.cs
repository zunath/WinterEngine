using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using WinterEngine.Network.Enums;

namespace WinterEngine.Network.Packets
{
    /// <summary>
    /// Used for requesting a particular file from the server.
    /// </summary>
    [ProtoContract]
    public class FileRequestPacket : PacketBase
    {
        #region Fields
        private string _fileName;

        #endregion

        #region Properties

        [ProtoMember(1)]
        public string FileName 
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        #endregion
    }
}
