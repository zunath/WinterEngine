using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class Scripts : IGameObjectRepository<Script>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;
        #region Constructors

        public Scripts(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Script InternalSave(Script script, bool saveChanges)
        {
            Script retScript;
            if (script.ResourceID <= 0)
            {
                retScript = _context.Scripts.Add(script);
            }
            else
            {
                retScript = _context.Scripts.SingleOrDefault(x => x.ResourceID == script.ResourceID);
                if (retScript == null) return null;
                _context.Entry(retScript).CurrentValues.SetValues(script);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retScript;
        }

        public Script Save(Script script)
        {
            return InternalSave(script, true);
        }

        public void Save(IEnumerable<Script> entityList)
        {
            if (entityList != null)
            {
                foreach (var script in entityList)
                {
                    InternalSave(script, false);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing script in the database with new values.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newScript">The new script that will replace the script with the matching resref.</param>
        public void Update(Script newScript)
        {
            Script dbScript;
            if (newScript.ResourceID <= 0)
            {
                dbScript = _context.Scripts.Where(x => x.Resref == newScript.Resref).SingleOrDefault();
            }
            else
            {
                dbScript = _context.Scripts.Where(x => x.ResourceID == newScript.ResourceID).SingleOrDefault();
            }
            if (dbScript == null) return;

            foreach (LocalVariable variable in newScript.LocalVariables)
            {
                variable.GameObjectBaseID = newScript.ResourceID;
            }

            _context.Entry(dbScript).CurrentValues.SetValues(newScript);
            _context.LocalVariables.RemoveRange(dbScript.LocalVariables.ToList());
            _context.LocalVariables.AddRange(newScript.LocalVariables.ToList());
            _context.Entry(dbScript).CurrentValues.SetValues(newScript);
        }


        private void DeleteInternal(Script script, bool saveChanges = true)
        {
            var dbScript = _context.Scripts.SingleOrDefault(x => x.ResourceID == script.ResourceID);
            if (dbScript == null) return;

            _context.LocalVariables.RemoveRange(dbScript.LocalVariables.ToList());
            _context.Scripts.Remove(dbScript);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Script script)
        {
            DeleteInternal(script);
        }

        public void Delete(int resourceID)
        {
            var script = _context.Scripts.Find(resourceID);
            DeleteInternal(script);
        }

        public void Delete(IEnumerable<Script> scriptList)
        {
            foreach (var script in scriptList)
            {
                DeleteInternal(script, false);
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// Returns all of the scripts from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Script> GetAll()
        {
            return _context.Scripts.ToList();
        }

        public Script GetByID(int resourceID)
        {
            return _context.Scripts.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all of the scripts in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Script> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Scripts.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the script with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Script GetByResref(string resref)
        {
            return _context.Scripts.Where(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Script script = _context.Scripts.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(script, null);
        }       

        #endregion

    }
}
