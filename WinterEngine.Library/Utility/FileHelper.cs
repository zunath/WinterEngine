using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using Ionic.Zlib;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;

namespace WinterEngine.Library.Utility
{
    /// <summary>
    /// File handling and manipulation for Winter Engine.
    /// </summary>
    public class FileHelper : IDisposable
    {
        #region Fields

        private string _tempDirectoryPath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the temporary directory path.
        /// </summary>
        public string TemporaryDirectoryPath
        {
            get { return _tempDirectoryPath; }
            set { _tempDirectoryPath = value; }
        }

        #endregion

        #region Constructors

        public FileHelper()
        {
        }

        /// <summary>
        /// Creates a temporary directory in the same folder as the executable.
        /// New directory's name is "temp" + suffix + uniqueID
        /// </summary>
        /// <param name="suffix">The suffix to add on to the temporary directory.</param>
        public FileHelper(string tempDirectorySuffix)
        {
            CreateTemporaryDirectory(tempDirectorySuffix);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Internal class method to assist with common functionality among all file creation.
        /// </summary>
        /// <param name="inputDirectory">The directory to take files from.</param>
        /// <param name="outputDirectory">The directory to create the file in.</param>
        /// <param name="fileName">The name of the file without an extension</param>
        /// <param name="fileType">The type of file to create.</param>
        private bool CreateFileFromDirectory(string inputDirectory, string outputDirectory, string fileName, FileTypeEnum fileType, CompressionLevel compressionLevel)
        {
            FileExtensionFactory winterExtensions = new FileExtensionFactory();
            bool success = true;
            string filePath = outputDirectory + "\\" + fileName + winterExtensions.GetFileExtension(fileType);

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
        /// Finds and returns the path to the database file in a directory.
        /// Returns "" if no file is found.
        /// </summary>
        /// <param name="directoryPath">The path to the directory that this method will check.</param>
        /// <returns></returns>
        public string GetDatabaseFileInDirectory(string directoryPath)
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            FileInfo[] fileInfo = directoryInfo.GetFiles();
            string extension = factory.GetFileExtension(FileTypeEnum.Database);
            string databaseFilePath = "";

            foreach (FileInfo file in fileInfo)
            {
                if (file.Extension == extension)
                {
                    databaseFilePath = file.FullName;
                    break;
                }
            }

            return databaseFilePath;
        }

        /// <summary>
        /// Creates a temporary directory in the same folder as the executable.
        /// New directory's name is "temp" + suffix + uniqueID
        /// </summary>
        /// <param name="suffix">The suffix to add on to the temporary directory.</param>
        /// <returns></returns>
        public string CreateTemporaryDirectory(string suffix = "")
        {
            string directoryPath = "./temp" + suffix;
            int uniqueID = 0;

            try
            {
                // Generate a unique ID
                while(Directory.Exists(directoryPath + uniqueID))
                {
                    uniqueID++;
                }

                // Attach unique ID on to directory
                TemporaryDirectoryPath = Directory.CreateDirectory(directoryPath + uniqueID + "/").FullName;
                return TemporaryDirectoryPath;
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error creating temporary directory: ", ex);
                return "";
            }
        }

        public void Dispose()
        {
            if (Directory.Exists(TemporaryDirectoryPath))
            {
                Directory.Delete(TemporaryDirectoryPath, true);
            }
        }

        #endregion
    }
}
