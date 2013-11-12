using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class FactionRepository : IResourceRepository<Faction>, IRepository
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

        public List<Faction> GetAll()
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


        public Faction Add(Faction faction)
        {
            return _context.Factions.Add(faction);
        }

        public void Add(List<Faction> factionList)
        {
            _context.Factions.AddRange(factionList);
        }

        public void Update(Faction faction)
        {
            Faction dbFaction = _context.Factions.Where(x => x.ResourceID == faction.ResourceID).SingleOrDefault();
            if (dbFaction == null) return;

            _context.Entry(dbFaction).CurrentValues.SetValues(faction);
        }

        public void Upsert(Faction faction)
        {
            if (faction.ResourceID <= 0)
            {
                _context.Factions.Add(faction);
            }
            else
            {
                Update(faction);
            }
        }

        public Faction GetByID(int factionID)
        {
            return _context.Factions.Where(x => x.ResourceID == factionID).SingleOrDefault();
        }

        public bool Exists(Faction faction)
        {
            Faction dbFaction = _context.Factions.Where(x => x.ResourceID == faction.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbFaction, null);
        }

        public void Delete(Faction faction)
        {
            _context.Factions.Remove(faction);
        }

        public int GetDefaultResourceID()
        {
            Faction defaultObject = _context.Factions.Where(x => x.IsDefault).FirstOrDefault();
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        #endregion


        public object Load(int resourceID)
        {
            throw new NotImplementedException();
        }

        public int Save(object gameObject)
        {
            throw new NotImplementedException();
        }

        public void DeleteByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int resourceID)
        {
            throw new NotImplementedException();
        }
    }
}
