using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum AuthorizationTypeEnum
    {
        Unknown = 0,
        UserDoesNotExist = 1,
        ServerDoesNotExist = 2,
        TokenMismatch = 3,
        Success = 4,
        Error = 5
    }
}
