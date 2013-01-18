using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Repository for accessing module-related data from the database.
    /// </summary>
    public class ModuleRepository : IDisposable
    {
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
