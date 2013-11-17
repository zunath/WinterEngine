using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class RaceRepository : IGenericRepository<Race>
    {
        #region Constructors

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        public RaceRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Race InternalSave(Race race, bool saveChanges)
        {
            Race retRace;
            if (race.ResourceID <= 0)
            {
                retRace = _context.Races.Add(race);
            }
            else
            {
                retRace = _context.Races.SingleOrDefault(x => x.ResourceID == race.ResourceID);
                if (retRace == null) return null;
                _context.Entry(retRace).CurrentValues.SetValues(race);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retRace;
        }

        public Race Save(Race race)
        {
            return InternalSave(race, true);
        }

        public void Save(IEnumerable<Race> entityList)
        {
            if (entityList != null)
            {
                foreach (var race in entityList)
                {
                    InternalSave(race, false);
                }
                _context.SaveChanges();
            }
        }

        public void Update(Race race)
        {
            Race dbRace = _context.Races.Where(x => x.ResourceID == race.ResourceID).SingleOrDefault();
            if (dbRace == null) return;

            _context.Entry(dbRace).CurrentValues.SetValues(race);
        }

        private void DeleteInternal(Race race, bool saveChanges = true)
        {
            var dbRace = _context.Abilities.SingleOrDefault(x => x.ResourceID == race.ResourceID);
            if (dbRace == null) return;

            _context.Abilities.Remove(dbRace);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Race race)
        {
            DeleteInternal(race);
        }

        public void Delete(int resourceID)
        {
            var race = _context.Races.Find(resourceID);
            DeleteInternal(race);
        }

        public void Delete(IEnumerable<Race> raceList)
        {
            foreach (var race in raceList)
            {
                DeleteInternal(race, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Race> GetAll()
        {
            return _context.Races.ToList();
        }

        public Race GetByID(int raceID)
        {
            return _context.Races.Where(x => x.ResourceID == raceID).SingleOrDefault();
        }


        #endregion

    }
}
