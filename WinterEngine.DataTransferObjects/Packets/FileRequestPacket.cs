using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using WinterEngine.DataTransferObjects.Enums;

namespace WinterEngine.DataTransferObjects.Packets
{
    /// <summary>
    /// Used for requesting a particular file from the server.
    /// </summary>
    [ProtoContract]
    public class FileRequestPacket : PacketBase
    {
        #region Fields
        private string _fileName;
        private FileRequestTypeEnum _fileRequestType;

        #endregion

        #region Properties

        [ProtoMember(1)]
        public string FileName 
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [ProtoMember(2)]
        public FileRequestTypeEnum FileRequestType
        {
            get { return _fileRequestType; }
            set { _fileRequestType = value; }
        }

        #endregion
    }
}
