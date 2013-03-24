using System;
using System.Collections.Generic;
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

        public void Dispose()
        {
        }

        #endregion
    }
}
