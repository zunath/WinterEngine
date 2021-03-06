﻿using System;
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

            return Context.Abilities.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.Abilities
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();
            return items;
        }


        public Ability Add(Ability ability)
        {
            return Context.Abilities.Add(ability);
        }

        public void Add(List<Ability> abilityList)
        {
            Context.Abilities.AddRange(abilityList);
        }

        public void Update(Ability ability)
        {
            Ability dbAbility = Context.Abilities.SingleOrDefault(x => x.ResourceID == ability.ResourceID);
            if (dbAbility == null) return;

            Context.Entry(dbAbility).CurrentValues.SetValues(ability);
        }

        public void Upsert(Ability ability)
        {
            if (ability.ResourceID <= 0)
            {
                Context.Abilities.Add(ability);
            }
            else
            {
                Update(ability);
            }
        }

        public Ability GetByID(int abilityID)
        {
            return Context.Abilities.SingleOrDefault(x => x.ResourceID == abilityID);
        }

        public bool Exists(Ability ability)
        {
            Ability dbAbility = Context.Abilities.SingleOrDefault(x => x.ResourceID == ability.ResourceID);
            return !Object.ReferenceEquals(dbAbility, null);
        }

        public void Delete(Ability ability)
        {
            Context.Abilities.Remove(ability);
        }

        public int GetDefaultResourceID()
        {
            Ability defaultObject = Context.Abilities.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }


        #endregion
    }
}
