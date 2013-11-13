using ProtoBuf;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Network.Packets
{
    [ProtoContract]
    public class DeleteCharacterPacket: PacketBase
    {
        [ProtoMember(1)]
        public string FileName { get; set; }

        [ProtoMember(2)]
        public DeleteCharacterTypeEnum DeleteRequestType { get; set; }

        public DeleteCharacterPacket()
        {
        }

        public DeleteCharacterPacket(string fileName, DeleteCharacterTypeEnum requestType)
        {
            this.FileName = fileName;
            this.DeleteRequestType = requestType;
        }
    }
}
