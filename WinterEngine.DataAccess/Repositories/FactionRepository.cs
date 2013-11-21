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
            return Context.Factions.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.Factions
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();
            return items;
        }


        public Faction Add(Faction faction)
        {
            return Context.Factions.Add(faction);
        }

        public void Add(List<Faction> factionList)
        {
            Context.Factions.AddRange(factionList);
        }

        public void Update(Faction faction)
        {
            Faction dbFaction = Context.Factions.SingleOrDefault(x => x.ResourceID == faction.ResourceID);
            if (dbFaction == null) return;

            Context.Entry(dbFaction).CurrentValues.SetValues(faction);
        }

        public void Upsert(Faction faction)
        {
            if (faction.ResourceID <= 0)
            {
                Context.Factions.Add(faction);
            }
            else
            {
                Update(faction);
            }
        }

        public Faction GetByID(int factionID)
        {
            return Context.Factions.SingleOrDefault(x => x.ResourceID == factionID);
        }

        public bool Exists(Faction faction)
        {
            Faction dbFaction = Context.Factions.SingleOrDefault(x => x.ResourceID == faction.ResourceID);
            return !Object.ReferenceEquals(dbFaction, null);
        }

        public void Delete(Faction faction)
        {
            Context.Factions.Remove(faction);
        }

        public int GetDefaultResourceID()
        {
            Faction defaultObject = Context.Factions.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

    }
}
