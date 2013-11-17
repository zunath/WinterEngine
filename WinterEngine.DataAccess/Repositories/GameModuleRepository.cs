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
    public class GameModuleRepository : IGenericRepository<GameModule>, IGameModuleRepository
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

        private GameModule InternalSave(GameModule gameModule, bool saveChanges)
        {
            GameModule retGameModule;
            if (gameModule.ResourceID <= 0)
            {
                retGameModule = _context.GameModules.Add(gameModule);
            }
            else
            {
                retGameModule = _context.GameModules.SingleOrDefault(x => x.ResourceID == gameModule.ResourceID);
                if (retGameModule == null) return null;
                _context.Entry(retGameModule).CurrentValues.SetValues(gameModule);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retGameModule;
        }

        public GameModule Save(GameModule entity)
        {
            return InternalSave(entity, true);
        }

        public void Save(IEnumerable<GameModule> entityList)
        {
            if(entityList != null)
            {
                foreach( var gm in entityList)
                {
                    InternalSave(gm, false);
                }
                _context.SaveChanges();
            }
        }

        public void Update(GameModule module)
        {
            //todo: What do I do with this?
            GameModule dbModule = GetModule();
            if (dbModule == null) throw new NullReferenceException("Game module does not exist");

            foreach (LocalVariable variable in module.LocalVariables)
            {
                variable.GameObjectBaseID = module.ResourceID;
            }

            //_context.Entry(dbModule).CurrentValues.SetValues(module);
            _context.Entry(module).State = EntityState.Modified;
            _context.LocalVariables.RemoveRange(module.LocalVariables.ToList());
            _context.LocalVariables.AddRange(module.LocalVariables.ToList());
        }

        private void DeleteInternal(GameModule gameModule, bool saveChanges = true)
        {
            var dbGameModule = _context.GameModules.SingleOrDefault(x => x.ResourceID == gameModule.ResourceID);
            if (dbGameModule == null) return;

            _context.GameModules.Remove(dbGameModule);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(GameModule gameModule)
        {
            DeleteInternal(gameModule);
        }

        public void Delete(int resourceID)
        {
            var gameModule = _context.GameModules.Find(resourceID);
            DeleteInternal(gameModule);
        }

        public void Delete(IEnumerable<GameModule> gameModuleList)
        {
            foreach (var gameModule in gameModuleList)
            {
                DeleteInternal(gameModule, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<GameModule> GetAll()
        {
            var result = _context.GameModules.ToList();
            return result;
        }

        public GameModule GetByID(int resourceID)
        {
            var result = _context.GameModules.Find(resourceID);
            return result;
        }

        public GameModule GetModule()
        {
            return _context.GameModules.FirstOrDefault();
        }

        #endregion



        
    }
}
