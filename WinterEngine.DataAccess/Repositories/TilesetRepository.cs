using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class TilesetRepository : IGameObjectRepository<Tileset>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public TilesetRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        public Tileset Add(Tileset tileset)
        {
            return _context.Tilesets.Add(tileset);
        }

        public void Add(List<Tileset> tilesetList)
        {
            _context.Tilesets.AddRange(tilesetList);
        }

        public Tileset Save(Tileset tileset)
        {
            if (tileset.ResourceID <= 0)
            {
                _context.Tilesets.Add(tileset);
            }
            else
            {
                Update(tileset);
            }
            return tileset;
        }

        public void Save(IEnumerable<Tileset> entityList)
        {
            throw new NotImplementedException();
        }

        public void Update(Tileset newTileset)
        {
            Tileset dbTileset = _context.Tilesets.Where(x => x.ResourceID == newTileset.ResourceID).SingleOrDefault();
            if (dbTileset == null) return;

            _context.Entry(dbTileset).CurrentValues.SetValues(newTileset);
        }

        public void Delete(Tileset tileset)
        {
            _context.Tilesets.Remove(tileset);
        }

        public void Delete(int resourceID)
        {
            Tileset tileset = _context.Tilesets.Where(x => x.ResourceID == resourceID).SingleOrDefault();
            _context.LocalVariables.RemoveRange(tileset.LocalVariables.ToList());
            _context.Tilesets.Remove(tileset);
        }

        public void Delete(IEnumerable<Tileset> entityList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tileset> GetAll()
        {
            return _context.Tilesets.ToList();
        }

        public Tileset GetByID(int tilesetID)
        {
            return _context.Tilesets.Where(x => x.ResourceID == tilesetID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Tileset> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Tilesets.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
        }

        public Tileset GetByResref(string resref)
        {
            return _context.Tilesets.Where(x => x.Resref == resref).SingleOrDefault();
        }

        public bool Exists(string resref)
        {
            Tileset tileset = _context.Tilesets.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(tileset, null);
        }

        #endregion

    }
}
