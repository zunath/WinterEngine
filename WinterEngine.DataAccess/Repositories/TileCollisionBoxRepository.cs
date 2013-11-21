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
            return Context.TileCollisionBoxes.ToList();
        }

        public TileCollisionBox Add(TileCollisionBox box)
        {
            return Context.TileCollisionBoxes.Add(box);
        }

        public void Add(List<TileCollisionBox> boxes)
        {
            Context.TileCollisionBoxes.AddRange(boxes);
        }

        public void Update(TileCollisionBox box)
        {
            TileCollisionBox dbBox = Context.TileCollisionBoxes.SingleOrDefault(x => x.CollisionBoxID == box.CollisionBoxID);
            if (dbBox == null) return;

            Context.Entry(dbBox).CurrentValues.SetValues(box);
        }

        public void Upsert(TileCollisionBox box)
        {
            if (box.CollisionBoxID <= 0)
            {
                Context.TileCollisionBoxes.Add(box);
            }
            else
            {
                Update(box);
            }
        }

        public TileCollisionBox GetByID(int boxID)
        {
            return Context.TileCollisionBoxes.SingleOrDefault(x => x.CollisionBoxID == boxID);
        }

        public bool Exists(TileCollisionBox box)
        {
            TileCollisionBox dbBox = Context.TileCollisionBoxes.SingleOrDefault(x => x.CollisionBoxID == box.CollisionBoxID);
            return !Object.ReferenceEquals(dbBox, null);
        }

        public void Delete(TileCollisionBox box)
        {
            Context.TileCollisionBoxes.Remove(box);
        }

        public List<TileCollisionBox> GetByTileID(int tileID)
        {
            return Context.TileCollisionBoxes.Where(x => x.TileID == tileID).ToList();
        }

        public List<TileCollisionBox> GetByTilesetID(int tilesetID)
        {
            List<TileCollisionBox> collisionBoxes = new List<TileCollisionBox>();
            List<Tile> tiles = Context.Tiles.Where(x => x.TilesetID == tilesetID).ToList();

            foreach (Tile tile in tiles)
            {
                collisionBoxes.AddRange(tile.CollisionBoxes);
            }

            return collisionBoxes;
        }

        #endregion

    }
}
