using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WinterEngine.DataTransferObjects.Enumerations
{
    public enum FileTypeEnum
    {
        [Description("Invalid File Type")]
        Invalid = 0,          // Invalid file type
        [Description("Module File")]
        Module = 1,           // .wmod - Contains database file(s)
        [Description("Content Package File")]
        ContentPackage = 2,   // .cpak - Contains end-user's custom graphics, sounds, etc
        [Description("Encapsulated Resource File")]
        Erf = 3,              // .werf - Contains data for individual objects that may be imported to other modules
        [Description("Winter Resource File")]
        WinterResource = 4,   // .wrsc - Contains models, textures, etc that are built in to the engine
        [Description("Sprite Sheet File")]
        SpriteSheet = 5,          // .tga  - Sprite Sheet files
        [Description("XNA Compiled Resource File")]
        XNACompiledFile = 6,  // .xnb  - XNA framework's compiled resource files
        [Description("Uncompiled Hakpak File")]
        UncompiledHakpak = 7, // .wuch - Uncompiled hakpak. Used by the hakpak editor and contains uncompiled resources
        [Description("Music File")]
        Music = 8,            // .mp3  - Music files
        [Description("Sound File")]
        Sound = 9,           // .wav  - Sound files
        [Description("Database File")]
        Database = 10,         // .sdf  - Database files  
        [Description("Player Character File")]
        PlayerCharacter = 11

    }
}
