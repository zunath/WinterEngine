using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum ResourceTypeEnum
    {
        [Description("ContentPackages")]
        ContentPackage = 1,
        [Description("Abilities")]
        Ability = 2,
        [Description("Categories")]
        Category = 3,
        [Description("CharacterClasses")]
        CharacterClass = 4,
        [Description("ItemProperties")]
        ItemProperty = 5,
        [Description("Races")]
        Race = 6,
        [Description("ContentPackageResources")]
        ContentPackageResource = 7,
        [Description("GameObject")]
        GameObject = 8,
        [Description("Gender")]
        Gender = 9
    }
}
