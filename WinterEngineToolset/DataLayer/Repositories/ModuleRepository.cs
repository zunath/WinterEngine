using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using Ionic.Zip;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.Helpers;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Enumerations;
using Ionic.Zlib;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class. Handles creating new database files for modules 
    /// and generating the standard tables that the rest of the toolset uses.
    /// </summary>
    public class ModuleRepository : IDisposable
    {

        /// <summary>
        /// Creates a new database file at the specified path with the specified file name.
        /// The ".sdf" extension will be added to the file. Do not pass it in the file name.
        /// </summary>
        /// <param name="databaseFilePath">The path to the file, excluding the file's name</param>
        /// <param name="databaseFileName">The name of the new database file. Exclude the .sdf extension - it will be added automatically.</param>
        private void CreateNewDatabase(string databaseFilePath, string databaseFileName)
        {
            string fullPath = databaseFilePath + "\\" + databaseFileName + ".sdf";
                
            // Update connection settings and the active module directory path
            ChangeDatabaseConnection(fullPath);
            WinterConnectionInformation.ActiveModuleDirectoryPath = fullPath;

            // Initialize the database - will create the database file at the specified location.
            // Also creates tables based on the code-first model.
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.Database.Initialize(true);
            }
        }

        /// <summary>
        /// Changes the database connection to the specified path. All subsequent database calls
        /// will utilize this connection until changed.
        /// </summary>
        /// <param name="databaseFilePath">The full path to the database file to which the conection will be changed.</param>
        private void ChangeDatabaseConnection(string databaseFilePath)
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            // Change the active connection to point to this new database.
            WinterConnectionInformation.ActiveConnectionString = "Data Source=" + databaseFilePath + ";Persist Security Info=False;";
        }

        /// <summary>
        /// Creates a new module at the specified directory with the specified name.
        /// Note that this module is temporary until SaveModule() is called.
        /// </summary>
        /// <param name="directory">Path of the directory in which the module will be created.</param>
        /// <param name="databaseName">Name of the database file used to store data.</param>
        public void CreateNewModule(string directory, string databaseName = "TempDB")
        {
            CreateNewDatabase(directory, databaseName);
        }

        /// <summary>
        /// Converts a temporary module directory into a module file. 
        /// If the module file does not exist, it will be created.
        /// If the module file already exists, it will be overwritten.
        /// </summary>
        /// <param name="tempDirectory">Directory path containing the database and additional information used for the module.</param>
        /// <param name="modulePath">Path of the permanent module file.</param>
        public void SaveModule(string temporaryDirectory, string modulePath)
        {
            int index = 0;
            try
            {
                // Generate a unique file name just in case another one already exists.
                // This is used just in case something goes wrong during the new save.
                while (File.Exists(modulePath + index))
                {
                    index++;
                }

                // Make a back up of the module file just in case something goes wrong.
                if (File.Exists(modulePath))
                {
                    File.Copy(modulePath, modulePath + index);
                }

                File.Delete(modulePath);

                using (ZipFile zipFile = new ZipFile(modulePath))
                {
                    // Change compression level to none (speeds up loading in-game and toolset)
                    // Add the directory and save the zip file.
                    zipFile.CompressionLevel = CompressionLevel.None;
                    zipFile.AddDirectory(temporaryDirectory, "");
                    zipFile.Save();

                    // Delete the backup since the new save was successful.
                    File.Delete(modulePath + index);
                }
            }
            catch (Exception ex)
            {
                // Something screwed up during the save. Delete the new zip file, if any
                // and then move the backup back to where it was.
                if (File.Exists(modulePath + index))
                {
                    File.Delete(modulePath);
                    File.Move(modulePath + index, modulePath);
                }

                ErrorHelper.ShowErrorDialog("Error saving module: ", ex);
            }
        }

        /// <summary>
        /// Handles creating a temporary directory to stored extracted files,
        /// changing the active module directory path, and other file-related issues
        /// when opening a module file from disk.
        /// </summary>
        /// <param name="modulePath"></param>
        public void OpenModule(string modulePath)
        {
            WinterConnectionInformation.ActiveModuleDirectoryPath = modulePath;
            

            // Create temporary directory to decompress files to
            if(Directory.Exists("./WE_Temp"))
            {
                Directory.Delete("./WE_Temp");
            }

            DirectoryInfo directoryInfo = Directory.CreateDirectory("./WE_Temp");

            FileExtensionFactory factory = new FileExtensionFactory();
            WinterFileHelper fileHelper = new WinterFileHelper();

            FileInfo[] fileInfo = directoryInfo.GetFiles();
            string extension = factory.GetFileExtension(FileType.Database);
            string databaseFilePath = "";

            foreach(FileInfo file in fileInfo)
            {
                if (file.Extension == extension)
                {
                    databaseFilePath = file.FullName;
                    break;
                }
            }

            // Change the database connection to the file located in the extracted module folder.
            ChangeDatabaseConnection(databaseFilePath);

        }

        public void Dispose()
        {
        }

    }
}
