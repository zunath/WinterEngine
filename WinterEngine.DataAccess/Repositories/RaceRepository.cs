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
            return Context.Races.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.Races
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();
            return items;
        }


        public Race Add(Race race)
        {
            return Context.Races.Add(race);
        }

        public void Add(List<Race> raceList)
        {
            Context.Races.AddRange(raceList);
        }

        public void Update(Race race)
        {
            Race dbRace = Context.Races.SingleOrDefault(x => x.ResourceID == race.ResourceID);
            if (dbRace == null) return;

            Context.Entry(dbRace).CurrentValues.SetValues(race);
        }

        public void Upsert(Race race)
        {
            if (race.ResourceID <= 0)
            {
                Context.Races.Add(race);
            }
            else
            {
                Update(race);
            }
        }

        public Race GetByID(int raceID)
        {
            return Context.Races.SingleOrDefault(x => x.ResourceID == raceID);
        }

        public bool Exists(Race race)
        {
            Race dbRace = Context.Races.SingleOrDefault(x => x.ResourceID == race.ResourceID);
            return !Object.ReferenceEquals(dbRace, null);
        }

        public void Delete(Race race)
        {
            Context.Races.Remove(race);
        }

        public int GetDefaultResourceID()
        {
            Race defaultObject = Context.Races.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

    }
}
