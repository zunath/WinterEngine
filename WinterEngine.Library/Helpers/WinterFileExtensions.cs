using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Library.Enumerations;

namespace WinterEngine.Library.Helpers
{
    public class WinterFileExtensions
    {
        /// <summary>
        /// Returns the extension for the specified file type, including the period.
        /// Example: .wmod
        /// </summary>
        /// <param name="fileType">The type of file to retrieve an extension for.</param>
        /// <returns></returns>
        public string getFileExtension(FileType fileType)
        {
            switch (fileType)
            {
                // Contains end-user's custom models, textures, etc
                case FileType.Hakpak:
                    return ".whak";
                // Contains database information specific to an end-user's module
                case FileType.Module:
                    return ".wmod";
                // Contains transportable database information
                case FileType.Erf:
                    return ".werf";
                // Contains the engine's built-in models, textures, etc
                case FileType.WinterResource:
                    return ".wrsc";
                default:
                    return "";
            }
        }
    }
}
