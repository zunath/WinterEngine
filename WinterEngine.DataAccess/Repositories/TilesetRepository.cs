using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects.Mapping;

namespace WinterEngine.DataAccess.Repositories
{
    public class TilesetRepository : RepositoryBase, IDisposable, IResourceRepository<Tileset>
    {
        #region Constructors

        public TilesetRepository(string connectionString = "")
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }
            ConnectionString = connectionString;
        }

        #endregion

        #region Methods

        public void Add(Tileset tileset)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.Tilesets.Add(tileset);
                context.SaveChanges();
            }
        }

        public void Add(List<Tileset> tilesetList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (Tileset tileset in tilesetList)
                {
                    context.Tilesets.Add(tileset);
                }
                context.SaveChanges();
            }
        }

        public void Update(Tileset tileset)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tileset dbTileset = context.Tilesets.SingleOrDefault(r => r.TilesetID.Equals(tileset.TilesetID));

                if (!Object.ReferenceEquals(dbTileset, null))
                {
                    context.Tilesets.Remove(dbTileset);
                    context.Tilesets.Add(dbTileset);
                    context.SaveChanges();
                }
            }
        }

        public void Upsert(Tileset tileset)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tileset dbTileset = context.Tilesets.SingleOrDefault(r => r.TilesetID.Equals(tileset.TilesetID));

                if (!Object.ReferenceEquals(dbTileset, null))
                {
                    context.Tilesets.Remove(dbTileset);
                    context.Tilesets.Add(dbTileset);
                    context.SaveChanges();
                }
                else
                {
                    context.Tilesets.Add(tileset);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Tileset tileset)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tileset dbTileset = context.Tilesets.SingleOrDefault(val => val.TilesetID == tileset.TilesetID);

                if (!Object.ReferenceEquals(dbTileset, null))
                {
                    context.Tilesets.Remove(dbTileset);
                    context.SaveChanges();
                }
            }
        }

        public List<Tileset> GetAll()
        {
            List<Tileset> tilesetList = new List<Tileset>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from tileset
                            in context.Tilesets
                            select tileset;
                tilesetList = query.ToList();
            }

            return tilesetList;
        }

        public bool Exists(Tileset tileset)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tileset dbTileset = context.Tilesets.FirstOrDefault(t => t.TilesetID.Equals(tileset.TilesetID));
                return !Object.ReferenceEquals(dbTileset, null);
            }
        }

        public void Dispose()
        {
        }

        #endregion


    }
}
