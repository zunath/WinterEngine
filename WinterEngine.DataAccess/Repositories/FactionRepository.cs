using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class FactionRepository : IGenericRepository<Faction>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public FactionRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Faction InternalSave(Faction faction, bool saveChanges)
        {
            Faction retFaction;
            if (faction.ResourceID <= 0)
            {
                retFaction = _context.Factions.Add(faction);
            }
            else
            {
                retFaction = _context.Factions.SingleOrDefault(x => x.ResourceID == faction.ResourceID);
                if (retFaction == null) return null;
                _context.Entry(retFaction).CurrentValues.SetValues(faction);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retFaction;
        }

        public Faction Save(Faction faction)
        {
            return InternalSave(faction, true);
        }

        public void Save(IEnumerable<Faction> entityList)
        {
            if(entityList != null)
            {
                foreach(var faction in entityList)
                {
                    InternalSave(faction, false);
                }
                _context.SaveChanges();
            }
        }

        public void Update(Faction faction)
        {
            Faction dbFaction = _context.Factions.Where(x => x.ResourceID == faction.ResourceID).SingleOrDefault();
            if (dbFaction == null) return;

            _context.Entry(dbFaction).CurrentValues.SetValues(faction);
        }

        private void DeleteInternal(Faction faction, bool saveChanges = true)
        {
            var dbFaction = _context.Factions.SingleOrDefault(x => x.ResourceID == faction.ResourceID);
            if (dbFaction == null) return;

            _context.Factions.Remove(dbFaction);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Faction faction)
        {
            DeleteInternal(faction);
        }

        public void Delete(int resourceID)
        {
            var faction = _context.Factions.Find(resourceID);
            DeleteInternal(faction);
        }

        public void Delete(IEnumerable<Faction> factionList)
        {
            foreach (var faction in factionList)
            {
                DeleteInternal(faction, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Faction> GetAll()
        {
            return _context.Factions.ToList();
        }

        //todo: Move this logic somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from item
        //                                        in Context.FactionRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = item.Name,
        //                                            ResourceID = item.ResourceID
        //                                        }).ToList();
        //    return items;
        //}


        public Faction GetByID(int factionID)
        {
            return _context.Factions.Where(x => x.ResourceID == factionID).SingleOrDefault();
        }
        
        //public bool Exists(Faction faction)
        //{
        //    Faction dbFaction = _context.Factions.Where(x => x.ResourceID == faction.ResourceID).SingleOrDefault();
        //    return !Object.ReferenceEquals(dbFaction, null);
        //}

        //public int GetDefaultResourceID()
        //{
        //    Faction defaultObject = _context.Factions.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

        #endregion

    }
}
