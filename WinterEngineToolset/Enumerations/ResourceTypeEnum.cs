using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Toolset.Enumerations
{
    // NOTE: These values should be pulled from the database in most cases.
    // However, I am unable to access the database through the VS2010 designer.
    // If anyone has a solution to this please let me know.
    public enum ResourceType
    {
        Area = 1,
        Creature = 2,
        Placeable = 3,
        Conversation = 4,
        Script = 5
    }
}
