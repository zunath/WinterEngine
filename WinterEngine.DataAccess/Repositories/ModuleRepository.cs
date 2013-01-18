using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess
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

        /// <summary>
        /// Adds a game module to the database. Note that there should only ever be one module
        /// added to the database.
        /// </summary>
        /// <param name="gameModule"></param>
        public void Add(GameModule gameModule)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.Modules.Add(gameModule);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the module object from the database. Note that there should only ever be one module
        /// in the database.
        /// </summary>
        /// <returns></returns>
        public GameModule GetModule()
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from module
                            in context.Modules
                            select module;
                return query.ToList()[0];
            }
        }

        /// <summary>
        /// Updates the module object in the database. Note that there should only ever be on module
        /// in the database.
        /// </summary>
        /// <param name="module"></param>
        public void UpdateModule(GameModule module)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from mod
                            in context.Modules
                            select mod;

                GameModule dbModule = query.ToList()[0];

                if (Object.ReferenceEquals(dbModule, null))
                {
                    throw new NullReferenceException("Unable to find module object.");
                }
                else
                {
                    context.Modules.Remove(dbModule);
                    context.Modules.Add(module);
                    context.SaveChanges();
                }
            }
        }

        public void Dispose()
        {
        }

    }
}
