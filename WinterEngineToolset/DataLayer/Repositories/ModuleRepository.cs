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
using DejaVu;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class. Handles creating new database files for modules 
    /// and generating the standard tables that the rest of the toolset uses.
    /// </summary>
    public class ModuleRepository : IDisposable
    {

        public void CreateNewDatabase(string databaseFilePath, string databaseFileName)
        {
            databaseFileName += ".sdf";
            using (WinterContext context = new WinterContext())
            {
                // This part works correctly - it creates the file in the correct directory.
                SqlCeEngine db = new SqlCeEngine();
                db.LocalConnectionString = "Data Source=" + databaseFilePath + "\\" + databaseFileName + ";Persist Security Info=False;";
                db.CreateDatabase();
                db.Dispose();
                


                // This is where things get sketchy - I've tried a bunch of different things but none have worked.
                SqlCeConnectionStringBuilder builder = new SqlCeConnectionStringBuilder();
                
                builder.DataSource = databaseFilePath + "\\" + databaseFileName;

                context.Database.Connection.ConnectionString = builder.ConnectionString;

                Database.SetInitializer(new WinterDatabaseInitializer());
                Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseFilePath, databaseFileName);
                Database.DefaultConnectionFactory.CreateConnection(builder.ConnectionString);
                
            }

            // Attempting to add data to the new connection doesn't work...
            using (WinterContext context = new WinterContext())
            {
                context.Areas.Add(new Area { Name = "Test", ResourceCategoryID = 1, Resref = "testresref", Tag = "testag" });
                context.SaveChanges();
            }


        }


        /// <summary>
        /// Opens an in-memory database connection
        /// </summary>
        public void OpenSQLiteConnection()
        {
            EntityConnectionStringBuilder ee = new System.Data.EntityClient.EntityConnectionStringBuilder();
            ee.Provider = "System.Data.SQLite";
            ee.Metadata = @"res://*/DataLayer.Database.WinterEngineModel.csdl|res://*/DataLayer.Database.WinterEngineModel.ssdl|res://*/DataLayer.Database.WinterEngineModel.msl";
            ee.ProviderConnectionString = @"data source=:memory:;";

            using (WinterContext context = new WinterContext())
            {
                //context.Database.Connection.ConnectionString = ee.ToString();
                //context.Database.Connection.Open();
            }
        }

        /// <summary>
        /// Closes the SQLite connection to the in-memory database.
        /// Once closed, ALL data in memory will be lost!
        /// </summary>
        public void CloseSQLiteConnection()
        {
            using (WinterContext context = new WinterContext())
            {
                //context.Database.Connection.Close();
            }
        }


        public void Dispose()
        {
        }
    }
}
