using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Contexts;
using System.Data.Entity;

namespace WinterEngine.Toolset.DataLayer.Initializers
{
    class WinterDatabaseInitializer : IDatabaseInitializer<WinterContext>
    {
        public void InitializeDatabase(WinterContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                }
            }
            context.Database.Create();
            
        }
    }
}
