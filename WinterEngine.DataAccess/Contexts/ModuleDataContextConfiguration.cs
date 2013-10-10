using System;
using System.Collections.Generic;
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
        }
    }
}
