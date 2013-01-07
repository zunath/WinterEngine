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
        /// Changes the database connection to the specified path. All subsequent database calls
        /// will utilize this connection until changed.
        /// </summary>
        /// <param name="databaseFilePath">The full path to the database file to which the conection will be changed.</param>
        public void ChangeDatabaseConnection(string databaseFilePath)
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            // Change the active connection to point to this new database.
            WinterConnectionInformation.ActiveConnectionString = "Data Source=" + databaseFilePath + ";Persist Security Info=False;";
        }

        /// <summary>
        /// Creates a new database file at the specified path with the specified file name.
        /// The ".sdf" extension will be added to the file. Do not pass it in the file name.
        /// </summary>
        /// <param name="databaseFilePath">The path to the file, excluding the file's name</param>
        /// <param name="databaseFileName">The name of the new database file. Exclude the .sdf extension - it will be added automatically.</param>
        public void CreateNewDatabase(string databaseFilePath, string databaseFileName)
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

        public void Dispose()
        {
        }

    }
}
