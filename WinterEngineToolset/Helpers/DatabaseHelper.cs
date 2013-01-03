using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using WinterEngine.Toolset.DataLayer.Initializers;
using System.Data.Entity.Infrastructure;

namespace WinterEngine.Toolset.Helpers
{
    public class DatabaseHelper
    {
        // Switches the database connection to the specified file
        public void ChangeDatabase(string directory, string databaseFileName)
        {
            try
            {
                Database.SetInitializer(new WinterDatabaseInitializer());
                Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", directory, databaseFileName);
               
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error switching databases.", ex);
            }
        }
    }
}
