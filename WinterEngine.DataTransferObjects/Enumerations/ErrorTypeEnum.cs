using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum ErrorTypeEnum : byte
    {
        [Description("No errors.")]
        None,
        [Description("This resref already exists.")]
        ObjectResrefAlreadyExists = 1,
        [Description("This resref does not exist.")]
        ObjectResrefDoesNotExist = 2,
        [Description("System resources cannot be modified.")]
        CannotChangeSystemResource = 3
    }
}
