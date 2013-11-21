using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class CharacterClassRepository : RepositoryBase, IResourceRepository<CharacterClass>
    {
        
        #region Constructors

        public CharacterClassRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<CharacterClass> GetAll()
        {
            return Context.CharacterClasses.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.CharacterClasses
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();

            return items;
        }


        public CharacterClass Add(CharacterClass characterClass)
        {
            return Context.CharacterClasses.Add(characterClass);
        }

        public void Add(List<CharacterClass> characterClassList)
        {
            Context.CharacterClasses.AddRange(characterClassList);
        }

        public void Update(CharacterClass characterClass)
        {
            CharacterClass dbCharacterClass = Context.CharacterClasses.SingleOrDefault(x => x.ResourceID == characterClass.ResourceID);
            if (dbCharacterClass == null) return;

            Context.Entry(dbCharacterClass).CurrentValues.SetValues(characterClass);
        }

        public void Upsert(CharacterClass characterClass)
        {
            if (characterClass.ResourceID <= 0)
            {
                Context.CharacterClasses.Add(characterClass);
            }
            else
            {
                Update(characterClass);
            }
        }

        public CharacterClass GetByID(int characterClassID)
        {
            return Context.CharacterClasses.SingleOrDefault(x => x.ResourceID == characterClassID);
        }

        public bool Exists(CharacterClass characterClass)
        {
            CharacterClass dbCharacterClass = Context.CharacterClasses.SingleOrDefault(x => x.ResourceID == characterClass.ResourceID);
            return !Object.ReferenceEquals(dbCharacterClass, null);
        }

        public void Delete(CharacterClass characterClass)
        {
            Context.CharacterClasses.Remove(characterClass);
        }

        public int GetDefaultResourceID()
        {
            CharacterClass defaultObject = Context.CharacterClasses.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion


    }
}
