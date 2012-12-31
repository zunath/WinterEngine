using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Toolset.Enumerations
{
    public enum FileType
    {
        Module = 1,        // .wmod - Contains database file(s)
        Hakpak = 2,        // .whak - Contains end-user's custom models, textures, etc
        Erf = 3,           // .werf - Contains data for individual objects that may be imported to other modules
        WinterResource = 4 // .wrsc - Contains models, textures, etc that are built in to the engine
    }
}
