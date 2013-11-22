using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using ProtoBuf;

namespace WinterEngine.DataTransferObjects.Packets
{
    [ProtoContract]
    [ProtoInclude(100, typeof(RequestPacket))]
    [ProtoInclude(101, typeof(ContentPackageListPacket))]
    [ProtoInclude(102, typeof(FileRequestPacket))]
    [ProtoInclude(103, typeof(StreamingFilePacket))]
    [ProtoInclude(104, typeof(StreamingFileDetailsPacket))]
    [ProtoInclude(105, typeof(CharacterSelectionPacket))]
    [ProtoInclude(106, typeof(UsernamePacket))]
    [ProtoInclude(107, typeof(DeleteCharacterPacket))]
    [ProtoInclude(108, typeof(CharacterCreationPacket))]
    public class PacketBase
    {
        /// <summary>
        /// Gets or sets the sender's NetConnection
        /// </summary>
        public NetConnection SenderConnection { get; set; }

        public PacketBase()
        {
        }

    }
}
