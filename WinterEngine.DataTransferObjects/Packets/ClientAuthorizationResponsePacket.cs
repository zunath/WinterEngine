using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.Packets
{
    public class ClientAuthorizationResponsePacket
    {
        public AuthorizationTypeEnum AuthorizationResponse { get; set; }

        public ClientAuthorizationResponsePacket()
        {
            AuthorizationResponse = AuthorizationTypeEnum.Unknown;
        }
    }
}
