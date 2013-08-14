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
        ObjectResrefAlreadyExists = 1
    }
}
