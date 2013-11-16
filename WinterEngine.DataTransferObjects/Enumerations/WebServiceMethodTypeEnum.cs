using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enums
{
    public enum WebServiceMethodTypeEnum
    {
        [Description("utility")]
        Utility = 1,
        [Description("user")]
        User = 2,
        [Description("server")]
        Server = 3
    }
}
