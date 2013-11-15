using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class AbilityRepository : IResourceRepository<Ability>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public AbilityRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        public Ability Add(Ability ability)
        {
            return _context.Abilities.Add(ability);
        }

        public void Add(List<Ability> abilityList)
        {
            _context.Abilities.AddRange(abilityList);
        }

        public void Save(Ability ability)
        {
            //todo need to return an id somehow
            if (ability.ResourceID <= 0)
            {
                _context.Abilities.Add(ability);
            }
            else
            {
                Update(ability);
            }
        }       

        public void Update(Ability ability)
        {
            Faction dbFaction = _context.Factions.Where(x => x.ResourceID == ability.ResourceID).SingleOrDefault();
            if (dbFaction == null) return;

            _context.Entry(dbFaction).CurrentValues.SetValues(ability);
        }

        public void Delete(Ability ability)
        {
            _context.Abilities.Remove(ability);
        }

        public void Delete(int resourceID)
        {
            throw new NotImplementedException();
        }

        public List<Ability> GetAll()
        {
            return _context.Abilities.ToList();
        }

        public Ability GetByID(int abilityID)
        {
            return _context.Abilities.Where(x => x.ResourceID == abilityID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            throw new NotImplementedException();
        }

        //public bool Exists(Ability ability)
        //{
        //    Ability dbAbility = _context.Abilities.Where(x => x.ResourceID == ability.ResourceID).SingleOrDefault();
        //    return !Object.ReferenceEquals(dbAbility, null);
        //}

        

        //public int GetDefaultResourceID()
        //{
        //    Ability defaultObject = _context.Abilities.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

        

        //public int GetDefaultResourceID(GameObjectTypeEnum resourceType)
        //{
        //    Category defaultObject = _context.Abilities.Where(x => x.IsDefault && x.ResourceType == resourceType).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}


        #endregion
    }
}
