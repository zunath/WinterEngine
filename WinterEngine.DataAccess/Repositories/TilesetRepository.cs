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

        private Tileset InternalSave(Tileset tileSet, bool saveChanges)
        {
            Tileset retTileset;
            if (tileSet.ResourceID <= 0)
            {
                retTileset = _context.Tilesets.Add(tileSet);
            }
            else
            {
                retTileset = _context.Tilesets.SingleOrDefault(x => x.ResourceID == tileSet.ResourceID);
                if (retTileset == null) return null;
                _context.Entry(retTileset).CurrentValues.SetValues(tileSet);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retTileset;
        }

        public Tileset Save(Tileset tileset)
        {
            return InternalSave(tileset, true);
        }

        public void Save(IEnumerable<Tileset> entityList)
        {
            if (entityList != null)
            {
                foreach (var tileset in entityList)
                {
                    InternalSave(tileset, false);
                }
                _context.SaveChanges();
            }
        }

        public void Update(Tileset newTileset)
        {
            Tileset dbTileset = _context.Tilesets.Where(x => x.ResourceID == newTileset.ResourceID).SingleOrDefault();
            if (dbTileset == null) return;

            _context.Entry(dbTileset).CurrentValues.SetValues(newTileset);
        }

        private void DeleteInternal(Tileset tileset, bool saveChanges = true)
        {
            Tileset dbTileset = _context.Tilesets.SingleOrDefault(x => x.ResourceID == tileset.ResourceID);
            if (dbTileset == null) return;

            _context.LocalVariables.RemoveRange(dbTileset.LocalVariables.ToList());
            _context.Tilesets.Remove(dbTileset);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Tileset tileset)
        {
            DeleteInternal(tileset, true);
        }

        public void Delete(int resourceID)
        {
            Tileset tileset = _context.Tilesets.Where(x => x.ResourceID == resourceID).SingleOrDefault();
            DeleteInternal(tileset, true);
        }

        public void Delete(IEnumerable<Tileset> tilesetList)
        {
            foreach (var tilesets in tilesetList)
            {
                DeleteInternal(tilesets, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Tileset> GetAll()
        {
            return _context.Tilesets.ToList();
        }

        public Tileset GetByID(int tilesetID)
        {
            return _context.Tilesets.Where(x => x.ResourceID == tilesetID).SingleOrDefault();
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
