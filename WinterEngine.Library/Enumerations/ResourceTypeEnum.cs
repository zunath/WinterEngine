using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Toolset.Enumerations
{
    /// <summary>
    /// Used by the custom User Controls. Refer to the values in the SQLite database for everything else.
    /// </summary>
    public enum ResourceTypeEnum
    {
        Area = 1,
        Creature = 2,
        Placeable = 3,
        Conversation = 4,
        Script = 5,
        Item = 6
    }
}
