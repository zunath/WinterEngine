using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using System.Data.Entity.Migrations;

namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Data access class. Handles creating new database files for modules 
    /// and generating the standard tables that the rest of the toolset uses.
    /// </summary>
    public class DatabaseRepository : IDatabaseRepository
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
            WinterConnectionInformation.ActiveConnectionString = BuildConnectionString(databaseFilePath);
        }

        /// <summary>
        /// Builds a connection string using the specified database file.
        /// </summary>
        /// <param name="databaseFilePath"></param>
        /// <returns></returns>
        public string BuildConnectionString(string databaseFilePath)
        {
            return "Data Source=" + databaseFilePath + ";Persist Security Info=False;";
        }

        /// <summary>
        /// Creates a new database file at the specified path with the specified file name.
        /// The ".sdf" extension will be added to the file. Do not pass it in the file name.
        /// </summary>
        /// <param name="databaseFilePath">The path to the file, excluding the file's name</param>
        /// <param name="databaseFileName">The name of the new database file. Exclude the .sdf extension - it will be added automatically.</param>
        /// <param name="changeApplicationConnection">If true, the application's connection will be changed to this new database.</param>
        /// <returns>Returns the full path to the database file.</returns>
        public string CreateNewDatabase(string databaseFilePath, string databaseFileName, bool changeApplicationConnection)
        {
            string fullPath = databaseFilePath + "\\" + databaseFileName + ".sdf";
            string connectionString;

            // Update connection settings and the active module directory path
            if (changeApplicationConnection)
            {
                ChangeDatabaseConnection(fullPath);
                WinterConnectionInformation.ActiveModuleDirectoryPath = fullPath;
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }
            // Otherwise we're simply creating a new database file. Build a connection string.
            else
            {
                connectionString = BuildConnectionString(fullPath);
            }

            // Initialize the database - will create the database file at the specified location.
            // Also creates tables based on the code-first model.
            using (ModuleDataContext context = new ModuleDataContext(connectionString))
            {
                DbMigrator migrator = new DbMigrator(new ModuleDataContextMigrationConfiguration());
                migrator.Update();
            }

            return fullPath;
        }

    }
}
