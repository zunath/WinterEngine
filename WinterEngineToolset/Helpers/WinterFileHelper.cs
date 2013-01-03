﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using WinterEngine.Toolset.Enumerations;
using Ionic.Zlib;
using System.IO;
using WinterEngine.Toolset.DataLayer.Contexts;

namespace WinterEngine.Toolset.Helpers
{
    /// <summary>
    /// File handling and manipulation for Winter Engine.
    /// </summary>
    public class WinterFileHelper
    {
        #region Fields
        #endregion

        #region Properties

        #endregion

        #region Methods

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

        /// <summary>
        /// Internal class method to assist with common functionality among all file creation.
        /// </summary>
        /// <param name="inputDirectory">The directory to take files from.</param>
        /// <param name="outputDirectory">The directory to create the file in.</param>
        /// <param name="fileName">The name of the file without an extension</param>
        /// <param name="fileType">The type of file to create.</param>
        private bool CreateFileFromDirectory(string inputDirectory, string outputDirectory, string fileName, FileType fileType, CompressionLevel compressionLevel)
        {
            bool success = true;
            string filePath = outputDirectory + "\\" + fileName + getFileExtension(fileType);

            try
            {
                using (ZipFile file = new ZipFile(filePath))
                {
                    file.CompressionLevel = compressionLevel;
                    file.AddDirectory(inputDirectory);
                    file.Save();
                }
            }
            catch (Exception ex)
            {
                success = false;
                ErrorHelper.ShowErrorDialog("Error creating file from directory: " + filePath, ex);
            }

            return success;
        }

        /// <summary>
        /// Takes all files in a directory and places them in an uncompressed zip file.
        /// Files are not compressed to speed up loading in the toolset and game.
        /// </summary>
        /// <param name="fileName">The name of the file without an extension.</param>
        /// <param name="inputDirectory">The directory of files to add.</param>
        /// <param name="outputDirectory">The directory to place the hakpak in.</param>
        public bool CreateHakPak(string inputDirectory, string outputDirectory, string fileName)
        {
            return CreateFileFromDirectory(inputDirectory, outputDirectory, fileName, FileType.Hakpak, CompressionLevel.None);
        }

        /// <summary>
        /// Takes a hakpak file and decompresses it to the specified directory.
        /// Files are not compressed to speed up loading in the toolset and game.
        /// </summary>
        /// <param name="hakPakPath">The path to the hakpak file, including the file's extension</param>
        /// <param name="outputDirectory">The directory to decompress to.</param>
        public bool DecompressHakPak(string hakPakPath, string outputDirectory)
        {
            bool success = true;

            try
            {
                using (ZipFile file = new ZipFile(hakPakPath))
                {
                    file.CompressionLevel = CompressionLevel.None;
                    file.ExtractAll(outputDirectory);
                }
            }
            catch (Exception ex)
            {
                success = false;
                ErrorHelper.ShowErrorDialog("Error decompressing hakpak to directory: " + outputDirectory, ex);
            }

            return success;
        }

        /// <summary>
        /// Encapsulates a database file and any additional files into a compressed file with the .wmod extension.
        /// </summary>
        /// <param name="databaseFilePath">The path to the database file, excluding the extension</param>
        /// <param name="outputDirectory">The directory to output the module file to</param>
        /// <param name="additionalFiles">List of file paths, including file extensions, to add to the module.</param>
        public bool CreateModule(string databaseFilePath, string outputDirectory, string fileName, IEnumerable<string> additionalFiles = null)
        {
            bool success = true;
            string filePath = outputDirectory + "\\" + fileName + "." + getFileExtension(FileType.Module);
            try
            {
                using (ZipFile file = new ZipFile(filePath))
                {
                    file.AddFile(databaseFilePath);

                    // Add additional files, if any
                    if (additionalFiles != null)
                    {
                        file.AddFiles(additionalFiles);
                    }

                    file.Save();
                }
            }
            catch (Exception ex)
            {
                success = false;
                ErrorHelper.ShowErrorDialog("Error creating module file: " + filePath, ex);
            }

            return success;
        }

        /// <summary>
        /// Decompresses a module to the specified directory.
        /// </summary>
        /// <param name="moduleFilePath">The path to the module, including the file's extension.</param>
        /// <param name="outputDirectory">The directory to decompress the files to.</param>
        public bool DecompressModule(string moduleFilePath, string outputDirectory)
        {
            bool success = true;

            try
            {
                using (ZipFile file = new ZipFile(moduleFilePath))
                {
                    file.ExtractAll(outputDirectory);
                }
            }
            catch (Exception ex)
            {
                success = false;
                ErrorHelper.ShowErrorDialog("Error decompressing module file to directory: " + outputDirectory, ex);
            }
            return success;
        }

        /// <summary>
        /// Creates a temporary directory in the same folder as the executable.
        /// New directory's name is "temp" + uniqueID
        /// </summary>
        /// <param name="directoryUniqueName"></param>
        /// <returns></returns>
        public string CreateTemporaryDirectory()
        {
            string directoryPath = "./temp";
            int uniqueID = 0;

            try
            {
                // Generate a unique ID
                while(Directory.Exists(directoryPath + uniqueID))
                {
                    uniqueID++;
                }

                // Attach unique ID on to directory
                return Directory.CreateDirectory(directoryPath + uniqueID + "/").FullName;
                
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error creating temporary directory: ", ex);
                return "";
            }
        }

        #endregion
    }
}
