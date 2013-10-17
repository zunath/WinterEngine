using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServerCompact;
using System.Linq;
using System.Text;

namespace WinterEngine.DataAccess.Contexts
{
    // This class is automatically mapped to the ModuleDataContext by Entity Framework. 
    // It just needs to exist in order to connect to SqlServerCE
    class ModuleDataContextConfiguration : DbConfiguration
    {
        public ModuleDataContextConfiguration()
        {
            SetProviderServices(SqlCeProviderServices.ProviderInvariantName, SqlCeProviderServices.Instance);
        }

    }
}
