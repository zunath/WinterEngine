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
        public string GetFileExtension(FileTypeEnum fileType)
        {
            switch (fileType)
            {
                // Contains end-user's custom models, textures, etc
                case FileTypeEnum.Hakpak:
                    return ".whak";
                // Contains database information specific to an end-user's module
                case FileTypeEnum.Module:
                    return ".wmod";
                // Contains transportable database information
                case FileTypeEnum.Erf:
                    return ".werf";
                // Contains the engine's built-in models, textures, etc
                case FileTypeEnum.WinterResource:
                    return ".wrsc";
                // Model graphics
                case FileTypeEnum.Model:
                    return ".fbx";
                // Texture graphics
                case FileTypeEnum.Texture:
                    return ".tga";
                // XNA compiled files
                case FileTypeEnum.XNACompiledFile:
                    return ".xnb";
                // Uncompiled hakpak files
                case FileTypeEnum.UncompiledHakpak:
                    return ".wuch";
                // Music files
                case FileTypeEnum.Music:
                    return ".mp3";
                // Sound files
                case FileTypeEnum.Sound:
                    return ".wav";
                // Database files
                case FileTypeEnum.Database:
                    return ".sdf";
                default:
                    return "";
            }
        }

        public FileTypeEnum GetFileType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".whak":
                    return FileTypeEnum.Hakpak;
                case ".wmod":
                    return FileTypeEnum.Module;
                case ".werf":
                    return FileTypeEnum.Erf;
                // Contains the engine's built-in models, textures, etc
                case ".wrsc":
                    return FileTypeEnum.WinterResource;
                // Model graphics
                case ".fbx":
                    return FileTypeEnum.Model;
                // Texture graphics
                case ".tga":
                    return FileTypeEnum.Texture;
                // XNA compiled files
                case ".xnb":
                    return FileTypeEnum.XNACompiledFile;
                // Uncompiled hakpak files
                case ".wuch":
                    return FileTypeEnum.UncompiledHakpak;
                // Music files
                case ".mp3":
                    return FileTypeEnum.Music;
                // Sound files
                case ".wav":
                    return FileTypeEnum.Sound;
                // Database files
                case ".sdf":
                    return FileTypeEnum.Database;
                default:
                    return FileTypeEnum.Invalid;
            }
        }
    }
}
