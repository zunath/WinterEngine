using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class RaceRepository : RepositoryBase, IResourceRepository<Race>
    {
        #region Constructors

        public RaceRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<Race> GetAll()
        {
            return Context.RaceRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects(bool includeDefault = false)
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.RaceRepository.Get()
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


        public Race Add(Race race)
        {
            return Context.RaceRepository.Add(race);
        }

        public void Add(List<Race> raceList)
        {
            Context.RaceRepository.AddList(raceList);
        }

        public void Update(Race race)
        {
            Race dbRace = Context.RaceRepository.Get(x => x.ResourceID == race.ResourceID).SingleOrDefault();
            if (dbRace == null) return;

            Context.Context.Entry(dbRace).CurrentValues.SetValues(race);
        }

        public void Upsert(Race race)
        {
            if (race.ResourceID <= 0)
            {
                Context.RaceRepository.Add(race);
            }
            else
            {
                Update(race);
            }
        }

        public Race GetByID(int raceID)
        {
            return Context.RaceRepository.Get(x => x.ResourceID == raceID).SingleOrDefault();
        }

        public bool Exists(Race race)
        {
            Race dbRace = Context.RaceRepository.Get(x => x.ResourceID == race.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbRace, null);
        }

        public void Delete(Race race)
        {
            Context.RaceRepository.Delete(race);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

    }
}
