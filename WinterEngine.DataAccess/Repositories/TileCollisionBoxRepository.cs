using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class TileCollisionBoxRepository: RepositoryBase
    {
        #region Constructors

        public TileCollisionBoxRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }


        #endregion

        #region Methods

        public List<TileCollisionBox> GetAll()
        {
            return Context.TileCollisionBoxRepository.Get().ToList();
        }

        public TileCollisionBox Add(TileCollisionBox box)
        {
            return Context.TileCollisionBoxRepository.Add(box);
        }

        public void Add(List<TileCollisionBox> boxes)
        {
            Context.TileCollisionBoxRepository.AddList(boxes);
        }

        public void Update(TileCollisionBox box)
        {
            TileCollisionBox dbBox = Context.TileCollisionBoxRepository.Get(x => x.CollisionBoxID == box.CollisionBoxID).SingleOrDefault();
            if (dbBox == null) return;

            Context.Context.Entry(dbBox).CurrentValues.SetValues(box);
        }

        public void Upsert(TileCollisionBox box)
        {
            if (box.CollisionBoxID <= 0)
            {
                Context.TileCollisionBoxRepository.Add(box);
            }
            else
            {
                Update(box);
            }
        }

        public TileCollisionBox GetByID(int boxID)
        {
            return Context.TileCollisionBoxRepository.Get(x => x.CollisionBoxID == boxID).SingleOrDefault();
        }

        public bool Exists(TileCollisionBox box)
        {
            TileCollisionBox dbBox = Context.TileCollisionBoxRepository.Get(x => x.CollisionBoxID == box.CollisionBoxID).SingleOrDefault();
            return !Object.ReferenceEquals(dbBox, null);
        }

        public void Delete(TileCollisionBox box)
        {
            Context.TileCollisionBoxRepository.Delete(box);
        }

        public List<TileCollisionBox> GetByTileID(int tileID)
        {
            return Context.TileCollisionBoxRepository.Get(x => x.TileID == tileID).ToList();
        }

        public List<TileCollisionBox> GetByTilesetID(int tilesetID)
        {
            List<TileCollisionBox> collisionBoxes = new List<TileCollisionBox>();
            List<Tile> tiles = Context.TileRepository.Get(x => x.TilesetID == tilesetID).ToList();

            foreach (Tile tile in tiles)
            {
                collisionBoxes.AddRange(tile.CollisionBoxes);
            }

            return collisionBoxes;
        }

        #endregion

    }
}
