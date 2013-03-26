using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;

namespace WinterEngine.FileAccess
{
    public class FileArchiveManager : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Constructs a new FileArchiveManager object.
        /// </summary>
        public FileArchiveManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zips a specified inputDirectory and outputs it to the specified outputDirectory
        /// </summary>
        /// <param name="inputDirectory"></param>
        /// <param name="outputDirectory"></param>
        public void ArchiveDirectory(string inputDirectoryPath, string outputDirectoryPath)
        {
            using (ZipFile zipFile = new ZipFile(outputDirectoryPath))
            {
                // Change compression level to none (speeds up loading in-game and toolset)
                // Add the directory and save the zip file.
                zipFile.CompressionLevel = CompressionLevel.None;
                zipFile.AddDirectory(inputDirectoryPath, "");
                zipFile.Save();
            }
        }

        /// <summary>
        /// Extracts the contents of the input archive and outputs its contents to the specified outputDirectoryPath
        /// </summary>
        public void ExtractArchive(string inputArchivePath, string outputDirectoryPath)
        {
            using (ZipFile zipFile = new ZipFile(inputArchivePath))
            {
                zipFile.ExtractAll(outputDirectoryPath);
            }
        }


        /// <summary>
        /// Creates a unique directory and returns the new directory's path.
        /// </summary>
        public string CreateUniqueDirectory()
        {
            string temporaryDirectoryPath = String.Empty;

            // Remove the existing temporary directory, if it exists.
            if (Directory.Exists(temporaryDirectoryPath))
            {
                Directory.Delete(temporaryDirectoryPath, true);
            }

            temporaryDirectoryPath = GenerateUniqueDirectoryName(Path.GetFullPath("./temp"));

            // Create the temporary directory
            Directory.CreateDirectory(temporaryDirectoryPath);

            return temporaryDirectoryPath;
        }

        /// <summary>
        /// Returns a path name with a unique ID number attached to the end of the file.
        /// This is used to prevent issues with copying/moving files.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GenerateUniqueFileName(string path)
        {
            int index = 0;

            while (File.Exists(path + index))
            {
                index++;
            }

            return path + index;
        }


        /// <summary>
        /// Returns a path name with a unique ID number attached to the end of the file.
        /// This is used to prevent issues with copying/moving directories.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GenerateUniqueDirectoryName(string path)
        {
            int index = 0;

            while (Directory.Exists(path + index))
            {
                index++;
            }

            return path + index;
        }


        public void Dispose()
        {
        }

        #endregion
    }
}
