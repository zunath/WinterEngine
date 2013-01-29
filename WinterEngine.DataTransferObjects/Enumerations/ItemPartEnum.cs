using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum ItemPartEnum
    {
        [Description("None")]
        None = 1,
        [Description("Single")]
        Single = 2,
        [Description("Top")]
        Top = 3,
        [Description("Middle")]
        Middle = 4,
        [Description("Bottom")]
        Bottom = 5
    }
}
