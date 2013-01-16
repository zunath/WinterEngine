using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    /// <summary>
    /// Used by the custom User Controls. Refer to the values in the SQLite database for everything else.
    /// </summary>
    public enum ResourceTypeEnum
    {
        [Description("Areas")]
        Area = 1,
        [Description("Creatures")]
        Creature = 2,
        [Description("Placeables")]
        Placeable = 3,
        [Description("Conversations")]
        Conversation = 4,
        [Description("Scripts")]
        Script = 5,
        [Description("Items")]
        Item = 6
    }
}
