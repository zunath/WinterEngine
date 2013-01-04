using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Library.Enumerations
{
    public enum FileType
    {
        Module = 1,           // .wmod - Contains database file(s)
        Hakpak = 2,           // .whak - Contains end-user's custom models, textures, etc
        Erf = 3,              // .werf - Contains data for individual objects that may be imported to other modules
        WinterResource = 4,   // .wrsc - Contains models, textures, etc that are built in to the engine
        Model = 5,            // .fbx  - Model files
        Texture = 6,          // .tga  - Texture files
        XNACompiledFile = 7,  // .xnb  - XNA framework's compiled resource files
        UncompiledHakpak = 8  // .wuch - Uncompiled hakpak. Used by the hakpak editor and contains uncompiled resources
    }
}
