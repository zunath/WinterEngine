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

        public CharacterClass Add(CharacterClass characterClass)
        {
            return _context.CharacterClasses.Add(characterClass);
        }

        public void Add(List<CharacterClass> characterClassList)
        {
            _context.CharacterClasses.AddRange(characterClassList);
        }


        public void Save(CharacterClass characterClass)
        {
            if (characterClass.ResourceID <= 0)
            {
                _context.CharacterClasses.Add(characterClass);
            }
            else
            {
                Update(characterClass);
            }
        }

        public void Update(CharacterClass characterClass)
        {
            CharacterClass dbCharacterClass = _context.CharacterClasses.Find(characterClass);
            if (dbCharacterClass == null) return;

            _context.Entry(dbCharacterClass).CurrentValues.SetValues(characterClass);
        }

        public void Delete(CharacterClass characterClass)
        {
            _context.CharacterClasses.Remove(characterClass);
        }

        public void Delete(int resourceID)
        {
            var charClass = _context.CharacterClasses.Find(resourceID);
            _context.CharacterClasses.Remove(charClass);
        }

        public List<CharacterClass> GetAll()
        {
            return _context.CharacterClasses.ToList();
        }

        public CharacterClass GetByID(int characterClassID)
        {
            return _context.CharacterClasses.Where(x => x.ResourceID == characterClassID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        //public bool Exists(CharacterClass characterClass)
        //{
        //    CharacterClass dbCharacterClass = _context.CharacterClasses.Where(x => x.ResourceID == characterClass.ResourceID).SingleOrDefault();
        //    return !Object.ReferenceEquals(dbCharacterClass, null);
        //}        

        //public int GetDefaultResourceID()
        //{
        //    CharacterClass defaultObject = _context.CharacterClasses.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}
                
        #endregion
    }
}
