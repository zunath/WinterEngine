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
            return Context.CharacterClassRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.CharacterClassRepository.Get()
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();

            return items;
        }


        public CharacterClass Add(CharacterClass characterClass)
        {
            return Context.CharacterClassRepository.Add(characterClass);
        }

        public void Add(List<CharacterClass> characterClassList)
        {
            Context.CharacterClassRepository.AddList(characterClassList);
        }

        public void Update(CharacterClass characterClass)
        {
            CharacterClass dbCharacterClass = Context.CharacterClassRepository.Get(x => x.ResourceID == characterClass.ResourceID).SingleOrDefault();
            if (dbCharacterClass == null) return;

            Context.Context.Entry(dbCharacterClass).CurrentValues.SetValues(characterClass);
        }

        public void Upsert(CharacterClass characterClass)
        {
            if (characterClass.ResourceID <= 0)
            {
                Context.CharacterClassRepository.Add(characterClass);
            }
            else
            {
                Update(characterClass);
            }
        }

        public CharacterClass GetByID(int characterClassID)
        {
            return Context.CharacterClassRepository.Get(x => x.ResourceID == characterClassID).SingleOrDefault();
        }

        public bool Exists(CharacterClass characterClass)
        {
            CharacterClass dbCharacterClass = Context.CharacterClassRepository.Get(x => x.ResourceID == characterClass.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbCharacterClass, null);
        }

        public void Delete(CharacterClass characterClass)
        {
            Context.CharacterClassRepository.Delete(characterClass);
        }

        public int GetDefaultResourceID()
        {
            CharacterClass defaultObject = Context.CharacterClassRepository.Get(x => x.IsDefault).FirstOrDefault();
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion


    }
}
