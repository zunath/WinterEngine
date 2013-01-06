using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Contexts;
using System.Data.EntityClient;
using System.Configuration;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.Helpers;
using System.Data.SqlServerCe;
using System.Data.Entity;
using WinterEngine.Toolset.DataLayer.Initializers;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using WinterEngine.Toolset.Enumerations;
using System.Windows.Forms;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class. Handles creating new database files for modules 
    /// and generating the standard tables that the rest of the toolset uses.
    /// </summary>
    public class ModuleRepository : RepositoryBase
    {

        /// <summary>
        /// Creates a new database file at the specified path with the specified file name.
        /// The ".sdf" extension will be added to the file. Do not pass it in the file name.
        /// </summary>
        /// <param name="databaseFilePath">The path to the file, excluding the file's name</param>
        /// <param name="databaseFileName">The name of the new database file. Exclude the .sdf extension - it will be added automatically.</param>
        public void CreateNewDatabase(string databaseFilePath, string databaseFileName)
        {
            databaseFileName += ".sdf";

            // Create a new database file at the specified location with the specified name.
            using (SqlCeEngine db = new SqlCeEngine())
            {
                string path = databaseFilePath + "\\" + databaseFileName;
                db.LocalConnectionString = "Data Source=" + databaseFilePath + ";Persist Security Info=False;";
                db.CreateDatabase();

                // Change the active connection to point to this new database.
                ChangeDatabaseConnection(databaseFilePath);
            }
        }

        /// <summary>
        /// Changes the database connection to the specified path. All subsequent database calls
        /// will utilize this connection until changed.
        /// </summary>
        /// <param name="databaseFilePath">The full path to the database file to which the conection will be changed.</param>
        public void ChangeDatabaseConnection(string databaseFilePath)
        {
            // Change the active connection to point to this new database.
            WinterConnectionInformation.ActiveConnectionString = "Data Source=" + databaseFilePath + ";Persist Security Info=False;";
        }

    }
}
