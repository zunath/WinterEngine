using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Library.Enumerations;

namespace WinterEngine.Library.Factories
{
    public class FileExtensionFactory
    {
        /// <summary>
        /// Returns the extension for the specified file type, including the period.
        /// Example: .wmod
        /// </summary>
        /// <param name="fileType">The type of file to retrieve an extension for.</param>
        /// <returns></returns>
        public string GetFileExtension(FileType fileType)
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
                // Model graphics
                case FileType.Model:
                    return ".fbx";
                // Texture graphics
                case FileType.Texture:
                    return ".tga";
                // XNA compiled files
                case FileType.XNACompiledFile:
                    return ".xnb";
                // Uncompiled hakpak files
                case FileType.UncompiledHakpak:
                    return ".wuch";
                // Music files
                case FileType.Music:
                    return ".mp3";
                // Sound files
                case FileType.Sound:
                    return ".wav";
                default:
                    return "";
            }
        }

        public FileType GetFileType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".whak":
                    return FileType.Hakpak;
                case ".wmod":
                    return FileType.Module;
                case ".werf":
                    return FileType.Erf;
                // Contains the engine's built-in models, textures, etc
                case ".wrsc":
                    return FileType.WinterResource;
                // Model graphics
                case ".fbx":
                    return FileType.Model;
                // Texture graphics
                case ".tga":
                    return FileType.Texture;
                // XNA compiled files
                case ".xnb":
                    return FileType.XNACompiledFile;
                // Uncompiled hakpak files
                case ".wuch":
                    return FileType.UncompiledHakpak;
                // Music files
                case ".mp3":
                    return FileType.Music;
                // Sound files
                case ".wav":
                    return FileType.Sound;
                default:
                    return FileType.Invalid;
            }
        }
    }
}
