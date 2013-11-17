using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class TileCollisionBoxRepository : IGenericRepository<TileCollisionBox>
    {
        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public TileCollisionBoxRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }


        #endregion

        #region Methods

        private TileCollisionBox InternalSave(TileCollisionBox tileCollisionBox, bool saveChanges)
        {
            TileCollisionBox retTileCollisionBox;
            if (tileCollisionBox.CollisionBoxID <= 0)
            {
                retTileCollisionBox = _context.TileCollisionBoxes.Add(tileCollisionBox);
            }
            else
            {
                retTileCollisionBox = _context.TileCollisionBoxes.SingleOrDefault(x => x.CollisionBoxID == tileCollisionBox.CollisionBoxID);
                if (retTileCollisionBox == null) return null;
                _context.Entry(retTileCollisionBox).CurrentValues.SetValues(tileCollisionBox);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retTileCollisionBox;
        }

        public TileCollisionBox Save(TileCollisionBox tCollisionBox)
        {
            return InternalSave(tCollisionBox, true);
        }

        public void Save(IEnumerable<TileCollisionBox> entityList)
        {
            if (entityList != null)
            {
                foreach (var tCollisionBox in entityList)
                {
                    InternalSave(tCollisionBox, false);
                }
                _context.SaveChanges();
            }
        }

        public void Update(TileCollisionBox box)
        {
            TileCollisionBox dbBox = _context.TileCollisionBoxes.Where(x => x.CollisionBoxID == box.CollisionBoxID).SingleOrDefault();
            if (dbBox == null) return;

            _context.Entry(dbBox).CurrentValues.SetValues(box);
        }

        private void DeleteInternal(TileCollisionBox tileCollisionBox, bool saveChanges = true)
        {
            var dbTileCollisionBox = _context.TileCollisionBoxes.SingleOrDefault(x => x.CollisionBoxID == tileCollisionBox.CollisionBoxID);
            if (dbTileCollisionBox == null) return;

            _context.TileCollisionBoxes.Remove(tileCollisionBox);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(TileCollisionBox ability)
        {
            DeleteInternal(ability);
        }

        public void Delete(int resourceID)
        {
            var tileCollisionBox = _context.TileCollisionBoxes.Find(resourceID);
            DeleteInternal(tileCollisionBox);
        }

        public void Delete(IEnumerable<TileCollisionBox> tileCollisionBoxList)
        {
            foreach (var tileCollisionBox in tileCollisionBoxList)
            {
                DeleteInternal(tileCollisionBox, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<TileCollisionBox> GetAll()
        {
            return _context.TileCollisionBoxes.ToList();
        }

        public TileCollisionBox GetByID(int boxID)
        {
            return _context.TileCollisionBoxes.Where(x => x.CollisionBoxID == boxID).SingleOrDefault();
        }


        //public bool Exists(TileCollisionBox box)
        //{
        //    TileCollisionBox dbBox = _context.TileCollisionBoxes.Where(x => x.CollisionBoxID == box.CollisionBoxID).SingleOrDefault();
        //    return !Object.ReferenceEquals(dbBox, null);
        //}        

        //public List<TileCollisionBox> GetByTileID(int tileID)
        //{
        //    return _context.TileCollisionBoxes.Where(x => x.TileID == tileID).ToList();
        //}

        //public List<TileCollisionBox> GetByTilesetID(int tilesetID)
        //{
        //    List<TileCollisionBox> collisionBoxes = new List<TileCollisionBox>();
        //    List<Tile> tiles = _context.Tiles.Where(x => x.TilesetID == tilesetID).ToList();

        //    foreach (Tile tile in tiles)
        //    {
        //        collisionBoxes.AddRange(tile.CollisionBoxes);
        //    }

        //    return collisionBoxes;
        //}

        #endregion


    }
}
