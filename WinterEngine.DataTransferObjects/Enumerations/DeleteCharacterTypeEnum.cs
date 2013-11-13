using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum DeleteCharacterTypeEnum : byte
    {
        Request = 1,
        Accepted = 2,
        Denied = 3,
        DeniedDisabled = 4,
        FileNotFound = 5,
        Error = 6
    }
}
