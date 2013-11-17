using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class CharacterClassRepository : IGenericRepository<CharacterClass>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public CharacterClassRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private CharacterClass InternalSave(CharacterClass charClass, bool saveChanges)
        {
            CharacterClass retCharClass;
            if (charClass.ResourceID <= 0)
            {
                retCharClass = _context.CharacterClasses.Add(charClass);
            }
            else
            {
                retCharClass = _context.CharacterClasses.SingleOrDefault(x => x.ResourceID == charClass.ResourceID);
                if (retCharClass == null) return null;
                _context.Entry(retCharClass).CurrentValues.SetValues(charClass);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retCharClass;
        }

        public CharacterClass Save(CharacterClass characterClass)
        {
            return InternalSave(characterClass, true);
        }

        public void Save(IEnumerable<CharacterClass> entityList)
        {
            if(entityList != null)
            {
                foreach(var cc in entityList)
                {
                    InternalSave(cc, false);
                }
                _context.SaveChanges();
            }
        }

        private void DeleteInternal(CharacterClass charClass, bool saveChanges = true)
        {
            var dbAbility = _context.CharacterClasses.SingleOrDefault(x => x.ResourceID == charClass.ResourceID);
            if (dbAbility == null) return;

            _context.CharacterClasses.Remove(charClass);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(CharacterClass charClass)
        {
            DeleteInternal(charClass);
        }

        public void Delete(int resourceID)
        {
            var charClass = _context.CharacterClasses.Find(resourceID);
            DeleteInternal(charClass);
        }

        public void Delete(IEnumerable<CharacterClass> charClassList)
        {
            foreach (var charClass in charClassList)
            {
                DeleteInternal(charClass, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<CharacterClass> GetAll()
        {
            return _context.CharacterClasses.ToList();
        }

        public CharacterClass GetByID(int characterClassID)
        {
            return _context.CharacterClasses.Where(x => x.ResourceID == characterClassID).SingleOrDefault();
        }
                                
        #endregion
    }
}
