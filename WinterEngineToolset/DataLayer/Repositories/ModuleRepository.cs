using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using WinterEngine.Toolset.DataLayer.Database;
using System.Data.EntityClient;
using System.Configuration;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class. Handles creating new database files for modules 
    /// and generating the standard tables that the rest of the toolset uses.
    /// </summary>
    public class ModuleRepository : IDisposable
    {
        public void LoadModuleDatabaseToMemory(string filePath)
        {
            SQLiteCommand command = new SQLiteCommand();
            
            
        }

        public void CreateNewDatabase(string filePath)
        {
            using (WinterContext context = new WinterContext())
            {
                //context.Database.Initialize(true);

                Area area = new Area();
                area.Name = "Test area";
                area.Resref = "testresref";
                area.Tag = "testtag";

                context.AddToAreas(area);
                context.SaveChanges();
            }

            using (WinterContext context = new WinterContext())
            {
                System.Windows.Forms.MessageBox.Show("" +context.Areas.FirstOrDefault(x => x.Resref.Equals("testresref")));
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
