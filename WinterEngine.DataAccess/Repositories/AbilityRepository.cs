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
    public class AbilityRepository : IGenericRepository<Ability>
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

        private Ability SaveInternal(Ability ability, bool saveChanges = true)
        {
            Ability retAbility;
            if (ability.ResourceID <= 0)
            {
                retAbility = _context.Abilities.Add(ability);
            }
            else
            {
                retAbility = _context.Abilities.SingleOrDefault(x => x.ResourceID == ability.ResourceID);
                if (retAbility == null) return null;
                _context.Entry(retAbility).CurrentValues.SetValues(ability);
                
            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retAbility;
        }

        public Ability Save(Ability ability)
        {
            return SaveInternal(ability, true);
        }

        public void Save(IEnumerable<Ability> abilities)
        {
            foreach (Ability current in abilities)
            {
                SaveInternal(current, false);
            }
            _context.SaveChanges();
        }

        private void DeleteInternal(Ability ability, bool saveChanges = true)
        {
            var dbAbility = _context.Abilities.SingleOrDefault(x => x.ResourceID == ability.ResourceID);
            if (dbAbility == null) return;

            _context.Abilities.Remove(dbAbility);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Ability ability)
        {
            DeleteInternal(ability);
        }

        public void Delete(int resourceID)
        {
            Ability ability = _context.Abilities.Find(resourceID);
            DeleteInternal(ability);
        }

        public void Delete(IEnumerable<Ability> abilities)
        {
            foreach (Ability ability in abilities)
            {
                DeleteInternal(ability, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Ability> GetAll()
        {
            return _context.Abilities;
        }

        public Ability GetByID(int resourceID)
        {
            return _context.Abilities.SingleOrDefault(x => x.ResourceID == resourceID);
        }

        #endregion
    }
}
