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

        public Faction Add(Faction faction)
        {
            return _context.Factions.Add(faction);
        }

        public void Add(List<Faction> factionList)
        {
            _context.Factions.AddRange(factionList);
        }

        public Faction Save(Faction faction)
        {
            if (faction.ResourceID <= 0)
            {
                _context.Factions.Add(faction);
            }
            else
            {
                Update(faction);
            }
            return faction;
        }

        public void Save(IEnumerable<Faction> entityList)
        {
            throw new NotImplementedException();
        }

        public void Update(Faction faction)
        {
            Faction dbFaction = _context.Factions.Where(x => x.ResourceID == faction.ResourceID).SingleOrDefault();
            if (dbFaction == null) return;

            _context.Entry(dbFaction).CurrentValues.SetValues(faction);
        }


        public void Delete(Faction faction)
        {
            _context.Factions.Remove(faction);
        }

        public void Delete(int resourceID)
        {
            var faction = _context.Factions.Find(resourceID);
            _context.Factions.Remove(faction);
        }

        public void Delete(IEnumerable<Faction> entityList)
        {
            throw new NotImplementedException();
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

        public void ApplyChanges()
        {
            _context.SaveChanges();
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
