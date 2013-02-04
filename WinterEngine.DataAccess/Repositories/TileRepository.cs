using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects.Mapping;

namespace WinterEngine.DataAccess.Repositories
{
    public class TileRepository : RepositoryBase, IDisposable, IResourceRepository<Tile>
    {
        #region Constructors

        public TileRepository(string connectionString = "")
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }
            ConnectionString = connectionString;
            
        }

        #endregion

        #region Methods

        public void Add(Tile tile)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.Tiles.Add(tile);
                context.SaveChanges();
            }
        }

        public void Add(List<Tile> tileList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (Tile tile in tileList)
                {
                    context.Tiles.Add(tile);
                }
                context.SaveChanges();
            }
        }

        public void Update(Tile tile)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tile dbTile = context.Tiles.SingleOrDefault(r => r.ID.Equals(tile.ID));

                if (!Object.ReferenceEquals(dbTile, null))
                {
                    context.Tiles.Remove(dbTile);
                    context.Tiles.Add(dbTile);
                    context.SaveChanges();
                }
            }
        }

        public void Upsert(Tile tile)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tile dbTile = context.Tiles.SingleOrDefault(r => r.ID.Equals(tile.ID));

                if (!Object.ReferenceEquals(dbTile, null))
                {
                    context.Tiles.Remove(dbTile);
                    context.Tiles.Add(dbTile);
                    context.SaveChanges();
                }
                else
                {
                    context.Tiles.Add(tile);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Tile tile)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tile dbResource = context.Tiles.SingleOrDefault(val => val.ID == tile.ID);

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.Tiles.Remove(dbResource);
                    context.SaveChanges();
                }
            }
        }

        public List<Tile> GetAll()
        {
            List<Tile> tileList = new List<Tile>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from tile
                            in context.Tiles
                            select tile;
                tileList = query.ToList();
            }

            return tileList;
        }

        public bool Exists(Tile tile)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Tile dbTile = context.Tiles.FirstOrDefault(t => t.ID.Equals(tile.ID));
                return !Object.ReferenceEquals(dbTile, null);
            }
        }

        public Tile GetByID(int tileID)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.Tiles.FirstOrDefault(x => x.ID == tileID);
            }
        }

        public void Dispose()
        {
        }

        #endregion


    }
}
