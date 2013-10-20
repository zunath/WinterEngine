using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class AbilityRepository : RepositoryBase, IResourceRepository<Ability>
    {
        #region Constructors

        public AbilityRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<Ability> GetAll()
        {
            return Context.AbilityRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects(bool includeDefault = false)
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.AbilityRepository.Get()
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();
            if (includeDefault)
            {
                items.Insert(0, new DropDownListUIObject(0, "(None)"));
            }

            return items;
        }


        public Ability Add(Ability ability)
        {
            return Context.AbilityRepository.Add(ability);
        }

        public void Add(List<Ability> abilityList)
        {
            Context.AbilityRepository.AddList(abilityList);
        }

        public void Update(Ability ability)
        {
            Faction dbFaction = Context.FactionRepository.Get(x => x.ResourceID == ability.ResourceID).SingleOrDefault();
            if (dbFaction == null) return;

            Context.Context.Entry(dbFaction).CurrentValues.SetValues(ability);
        }

        public void Upsert(Ability ability)
        {
            if (ability.ResourceID <= 0)
            {
                Context.AbilityRepository.Add(ability);
            }
            else
            {
                Update(ability);
            }
        }

        public Ability GetByID(int abilityID)
        {
            return Context.AbilityRepository.Get(x => x.ResourceID == abilityID).SingleOrDefault();
        }

        public bool Exists(Ability ability)
        {
            Ability dbAbility = Context.AbilityRepository.Get(x => x.ResourceID == ability.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbAbility, null);
        }

        public void Delete(Ability ability)
        {
            Context.AbilityRepository.Delete(ability);
        }

        public override void Dispose()
        {
            base.Dispose();
        }


        #endregion
    }
}
