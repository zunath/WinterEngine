using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class FactionRepository : RepositoryBase, IResourceRepository<Faction>
    {
        #region Constructors

        public FactionRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<Faction> GetAll()
        {
            return Context.FactionRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects(bool includeDefault = false)
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.FactionRepository.Get()
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


        public Faction Add(Faction faction)
        {
            return Context.FactionRepository.Add(faction);
        }

        public void Add(List<Faction> factionList)
        {
            Context.FactionRepository.AddList(factionList);
        }

        public void Update(Faction faction)
        {
            Faction dbFaction = Context.FactionRepository.Get(x => x.ResourceID == faction.ResourceID).SingleOrDefault();
            if (dbFaction == null) return;

            Context.Context.Entry(dbFaction).CurrentValues.SetValues(faction);
        }

        public void Upsert(Faction faction)
        {
            if (faction.ResourceID <= 0)
            {
                Context.FactionRepository.Add(faction);
            }
            else
            {
                Update(faction);
            }
        }

        public Faction GetByID(int factionID)
        {
            return Context.FactionRepository.Get(x => x.ResourceID == factionID).SingleOrDefault();
        }

        public bool Exists(Faction faction)
        {
            Faction dbFaction = Context.FactionRepository.Get(x => x.ResourceID == faction.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbFaction, null);
        }

        public void Delete(Faction faction)
        {
            Context.FactionRepository.Delete(faction);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

    }
}
