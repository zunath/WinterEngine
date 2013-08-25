using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess.Factories
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
                // Contains end-user's custom graphics, sounds, etc
                case FileTypeEnum.ContentPackage:
                    return ".cpak";
                // Contains database information specific to an end-user's module
                case FileTypeEnum.Module:
                    return ".wmod";
                // Contains transportable database information
                case FileTypeEnum.Erf:
                    return ".werf";
                // Contains the engine's built-in models, textures, etc
                case FileTypeEnum.WinterResource:
                    return ".wrsc";
                // Sprite Sheet graphics
                case FileTypeEnum.SpriteSheet:
                    return ".png";
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
                // Player character files
                case FileTypeEnum.PlayerCharacter:
                    return ".wpc";
                // "Deleted" player character files
                case FileTypeEnum.DeletedPlayerCharacter:
                    return ".dwpc";
                default:
                    return "";
            }
        }

        public FileTypeEnum GetFileType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".cpak":
                    return FileTypeEnum.ContentPackage;
                case ".wmod":
                    return FileTypeEnum.Module;
                case ".werf":
                    return FileTypeEnum.Erf;
                // Contains the engine's built-in models, textures, etc
                case ".wrsc":
                    return FileTypeEnum.WinterResource;
                // Sprite sheet graphics
                case ".png":
                    return FileTypeEnum.SpriteSheet;
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
                // Player character files
                case ".wpc":
                    return FileTypeEnum.PlayerCharacter;
                // Delete player character files
                case ".dwpc":
                    return FileTypeEnum.DeletedPlayerCharacter;
                default:
                    return FileTypeEnum.Invalid;
            }
        }

        /// <summary>
        /// Returns a string containing a filter for Open/Save File Dialogs for
        /// ONLY graphic files.
        /// </summary>
        /// <returns></returns>
        public string BuildGraphicFileFilter()
        {
            string filter = "All Available Types|*.png|" +
                            "Sprite Sheet Files|*.png";

            return filter;
        }

        /// <summary>
        /// Returns a string containing a filter for Open/Save File Dialogs for content package files.
        /// </summary>
        /// <returns></returns>
        public string BuildContentPackageFileFilter()
        {
            string filter = "All Available Types|*" + GetFileExtension(FileTypeEnum.ContentPackage);
            return filter;
        }

        /// <summary>
        /// Returns a string containing a filter for Open/Save File Dialogs for content package resources.
        /// </summary>
        /// <returns></returns>
        public string BuildContentPackageResourceFileFilter()
        {
            string filter = "All Available Types|*.png;*.mp3;*.wav|" +
                            "Graphic Files (.PNG)|*.png|" +
                            "BGM Files (.MP3)|*.mp3|" + 
                            "Sound Effect Files (.WAV)|*.wav";

            return filter;
        }

        /// <summary>
        /// Returns a string containing a filter for Open/Save File Dialogs for ERF files.
        /// </summary>
        /// <returns></returns>
        public string BuildERFFileFilter()
        {
            string filter = "All Available Types|*" + GetFileExtension(FileTypeEnum.Erf) +
                            "|Winter ERF Files (" + GetFileExtension(FileTypeEnum.Erf) + ")|*" + GetFileExtension(FileTypeEnum.Erf);

            return filter;
        }

        /// <summary>
        /// Returns a string containing a filter for Open/Save File Dialogs for modules.
        /// </summary>
        /// <returns></returns>
        public string BuildModuleFileFilter()
        {
            string fileExtension = GetFileExtension(FileTypeEnum.Module);
            string filter = "Winter Module Files (*" + fileExtension + ") | " + "*" + fileExtension; ;

            return filter;
        }
    }
}
