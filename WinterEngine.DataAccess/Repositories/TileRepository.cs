using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public void Add(List<Tile> tileList)
        {
        }

        public void Update(Tile tile)
        {
        }

        public void Upsert(Tile tile)
        {
        }

        public void Delete(Tile tile)
        {
        }

        public List<Tile> GetAll()
        {
            List<Tile> tileList = new List<Tile>();

            return tileList;
        }

        public bool Exists(Tile tile)
        {
            bool success = false;

            return success;
        }

        public void Dispose()
        {
        }

        #endregion


    }
}
