using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Repositories;
using System.Collections.Generic;
using WinterEngine.DataTransferObjects.UIObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Repository for accessing module-related data from the database.
    /// </summary>
    public class GameModuleRepository : RepositoryBase, IGameModuleRepository
    {
        #region Constructors

        public GameModuleRepository(string connectionString = "", bool autoSaveChanges = true) 
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public GameModule Add(GameModule gameModule)
        {
            return Context.GameModules.Add(gameModule);
        }

        public void Update(GameModule module)
        {
            GameModule dbModule = GetModule();
            if (dbModule == null) throw new Exception("Game module does not exist");

            foreach (LocalVariable variable in module.LocalVariables)
            {
                variable.GameObjectBaseID = module.ResourceID;
            }

            Context.Entry(dbModule).CurrentValues.SetValues(module);
            Context.LocalVariables.RemoveRange(dbModule.LocalVariables.ToList());
            Context.LocalVariables.AddRange(module.LocalVariables.ToList());
        }

        public void Delete(int resourceID)
        {
            GameModule module = Context.GameModules.FirstOrDefault(x => x.ResourceID == resourceID);
            Context.GameModules.Remove(module);
        }

        public bool Exists()
        {
            GameModule module = GetModule();
            return !Object.ReferenceEquals(module, null);
        }

        public GameModule GetModule()
        {
            return Context.GameModules.FirstOrDefault();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
