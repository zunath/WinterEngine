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
        [Description("Hakpak File")]
        Hakpak = 2,           // .whak - Contains end-user's custom models, textures, etc
        [Description("Encapsulated Resource File")]
        Erf = 3,              // .werf - Contains data for individual objects that may be imported to other modules
        [Description("Winter Resource File")]
        WinterResource = 4,   // .wrsc - Contains models, textures, etc that are built in to the engine
        [Description("Model File")]
        Model = 5,            // .fbx  - Model files
        [Description("Texture File")]
        Texture = 6,          // .tga  - Texture files
        [Description("XNA Compiled Resource File")]
        XNACompiledFile = 7,  // .xnb  - XNA framework's compiled resource files
        [Description("Uncompiled Hakpak File")]
        UncompiledHakpak = 8, // .wuch - Uncompiled hakpak. Used by the hakpak editor and contains uncompiled resources
        [Description("Music File")]
        Music = 9,            // .mp3  - Music files
        [Description("Sound File")]
        Sound = 10,           // .wav  - Sound files
        [Description("Database File")]
        Database = 11         // .sdf  - Database files  
    }
}
