using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum GameObjectTypeEnum
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
        Item = 6,
        [Description("Tilesets")]
        Tileset = 7,
        [Description("GameModules")]
        GameModule = 8,
        [Description("Unknown")]
        Unknown = 99
    }
}
