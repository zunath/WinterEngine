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

        public Race Add(Race race)
        {
            return _context.Races.Add(race);
        }

        public void Add(List<Race> raceList)
        {
            _context.Races.AddRange(raceList);
        }

        public Race Save(Race race)
        {
            if (race.ResourceID <= 0)
            {
                _context.Races.Add(race);
            }
            else
            {
                Update(race);
            }
            return race;
        }

        public void Save(IEnumerable<Race> entityList)
        {
            throw new NotImplementedException();
        }

        public void Update(Race race)
        {
            Race dbRace = _context.Races.Where(x => x.ResourceID == race.ResourceID).SingleOrDefault();
            if (dbRace == null) return;

            _context.Entry(dbRace).CurrentValues.SetValues(race);
        }

        public void Delete(Race race)
        {
            _context.Races.Remove(race);
        }

        public void Delete(int resourceID)
        {
            var race = _context.Races.Find(resourceID);
            Delete(race);
        }

        public void Delete(IEnumerable<Race> entityList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Race> GetAll()
        {
            return _context.Races.ToList();
        }

        //todo: move logic somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from item
        //                                        in Context.RaceRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = item.Name,
        //                                            ResourceID = item.ResourceID
        //                                        }).ToList();
        //    return items;
        //}



        public Race GetByID(int raceID)
        {
            return _context.Races.Where(x => x.ResourceID == raceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        //public bool Exists(Race race)
        //{
        //    Race dbRace = _context.Races.Where(x => x.ResourceID == race.ResourceID).SingleOrDefault();
        //    return !Object.ReferenceEquals(dbRace, null);
        //}

        //public int GetDefaultResourceID()
        //{
        //    Race defaultObject = _context.Races.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

        #endregion

    }
}
