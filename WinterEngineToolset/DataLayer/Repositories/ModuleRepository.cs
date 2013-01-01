using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using WinterEngine.Toolset.DataLayer.Database;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class. Handles creating new database files for modules 
    /// and generating the standard tables that the rest of the toolset uses.
    /// </summary>
    public class ModuleRepository : IDisposable
    {
        public void CreateNewDatabase(string filePath)
        {
            filePath += "testdb.s3db";
            // Create the file at the specified path
            SQLiteConnection.CreateFile(filePath);

            using (WinterContext context = new WinterContext())
            {
                //context.Database.Connection.ConnectionString = "metadata=res://*/DataLayer.Database.WinterEngineModel.csdl|res://*/DataLayer.Database.WinterEngineModel.ssdl|res://*/DataLayer.Database.WinterEngineModel.msl;provider=System.Data.SQLite;provider connection string='data source=&quot;" + filePath;
                //context.SaveChanges();
                context.Database.Connection.ChangeDatabase(filePath);
                context.SaveChanges();
            }

            using (WinterContext context = new WinterContext())
            {
                context.Database.Initialize(true);
                context.Database.Create();
                context.SaveChanges();
            }
        }


        public void Dispose()
        {
        }
    }
}
