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
    public class GameModuleRepository
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public GameModuleRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        public GameModule Add(GameModule gameModule)
        {
            return _context.Modules.Add(gameModule);
        }

        public void Update(GameModule module)
        {
            GameModule dbModule = GetModule();
            if (dbModule == null) throw new Exception("Game module does not exist");

            foreach (LocalVariable variable in module.LocalVariables)
            {
                variable.GameObjectBaseID = module.ResourceID;
            }

            _context.Entry(dbModule).CurrentValues.SetValues(module);
            _context.LocalVariables.RemoveRange(dbModule.LocalVariables.ToList());
            _context.LocalVariables.AddRange(module.LocalVariables.ToList());
        }

        public void Delete(int resourceID)
        {
            GameModule module = _context.Modules.Where(x => x.ResourceID == resourceID).FirstOrDefault();
            _context.Modules.Remove(module);
        }

        public bool Exists()
        {
            GameModule module = GetModule();
            return !Object.ReferenceEquals(module, null);
        }

        public GameModule GetModule()
        {
            return _context.Modules.FirstOrDefault();
        }

        #endregion
    }
}
