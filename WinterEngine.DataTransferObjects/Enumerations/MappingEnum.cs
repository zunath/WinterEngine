using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum MappingEnum
    {
        TileWidth = 32,
        TileHeight = 32,
        // The amount to divide TileWidth and TileHeight by, to display the collision boxes
        // Must divide evenly into both values.
        CollisionBoxDivisor = 4, 
    }
}
