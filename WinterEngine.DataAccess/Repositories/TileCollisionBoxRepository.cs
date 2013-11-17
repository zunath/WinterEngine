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

        public TileCollisionBox Add(TileCollisionBox box)
        {
            return _context.TileCollisionBoxes.Add(box);
        }

        public void Add(List<TileCollisionBox> boxes)
        {
            _context.TileCollisionBoxes.AddRange(boxes);
        }

        public TileCollisionBox Save(TileCollisionBox box)
        {
            if (box.CollisionBoxID <= 0)
            {
                _context.TileCollisionBoxes.Add(box);
            }
            else
            {
                Update(box);
            }
            return box;
        }

        public void Save(IEnumerable<TileCollisionBox> entityList)
        {
            throw new NotImplementedException();
        }

        public void Update(TileCollisionBox box)
        {
            TileCollisionBox dbBox = _context.TileCollisionBoxes.Where(x => x.CollisionBoxID == box.CollisionBoxID).SingleOrDefault();
            if (dbBox == null) return;

            _context.Entry(dbBox).CurrentValues.SetValues(box);
        }

        public void Delete(TileCollisionBox box)
        {
            _context.TileCollisionBoxes.Remove(box);
        }

        public void Delete(int resourceID)
        {
            var tileCollisionBox = _context.TileCollisionBoxes.Find(resourceID);
            Delete(tileCollisionBox);
        }

        public void Delete(IEnumerable<TileCollisionBox> entityList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TileCollisionBox> GetAll()
        {
            return _context.TileCollisionBoxes.ToList();
        }

        public TileCollisionBox GetByID(int boxID)
        {
            return _context.TileCollisionBoxes.Where(x => x.CollisionBoxID == boxID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        #endregion


    }
}
