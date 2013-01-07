using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using Ionic.Zip;
using WinterEngine.Toolset.DataLayer.Contexts;

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
        public void SaveModule(string tempDirectory, string modulePath)
        {
            using (ZipFile zip = new ZipFile(modulePath))
            {
                zip.AddDirectory(tempDirectory);
            }
        }

        /// <summary>
        /// Closes a module and removes the active database connection.
        /// Note that any unsaved changes by the user will be lost.
        /// </summary>
        /// <param name="tempDirectory">Directory containing temporary files pertaining to the module.</param>
        public void CloseModule(string tempDirectory)
        {
            Directory.Delete(tempDirectory);
        }

        public void OpenModule(string modulePath)
        {
            WinterConnectionInformation.ActiveModuleDirectoryPath = modulePath;
        }

        public void Dispose()
        {
        }

    }
}
