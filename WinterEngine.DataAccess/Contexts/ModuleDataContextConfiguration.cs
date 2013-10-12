using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace WinterEngine.DataAccess.Contexts
{
    class ModuleDataContextConfiguration : DbMigrationsConfiguration<ModuleDataContext>
    {
        public ModuleDataContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            // Check is made just in case the model is built using the Entity Framework powershell script.
            if (!string.IsNullOrWhiteSpace(WinterConnectionInformation.ActiveConnectionString))
            {
                TargetDatabase = new DbConnectionInfo(WinterConnectionInformation.ActiveConnectionString, "System.Data.SqlServerCe.4.0");
            }
        }
    }
}
