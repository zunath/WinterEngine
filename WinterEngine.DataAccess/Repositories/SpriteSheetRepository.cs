using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Graphics;

namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class SpriteSheetRepository : RepositoryBase, IResourceRepository<SpriteSheet>
    {
        #region Constructors

        public SpriteSheetRepository(string connectionString = "")
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }
            ConnectionString = connectionString;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns all graphic resources from the database.
        /// </summary>
        /// <returns></returns>
        public List<SpriteSheet> GetAll()
        {
            List<SpriteSheet> _resourceList = new List<SpriteSheet>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from resource
                            in context.SpriteSheets
                            select resource;
                _resourceList = query.ToList();
            }

            return _resourceList;
        }


        /// <summary>
        /// Adds a graphic resource to the database.
        /// </summary>
        /// <param name="resource">The graphic resource to add to the database.</param>
        /// <returns></returns>
        public void Add(SpriteSheet resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.SpriteSheets.Add(resource);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a list of sprite sheets to the database.
        /// </summary>
        /// <param name="resourceList">The list of sprite sheets to add to the database.</param>
        /// <returns></returns>
        public void Add(List<SpriteSheet> resourceList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (SpriteSheet resource in resourceList)
                {
                    context.SpriteSheets.Add(resource);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing sprite sheet with new values.
        /// </summary>
        /// <param name="resource">The resource to update. Its ID must match the ID of an entry in the database.</param>
        /// <returns></returns>
        public void Update(SpriteSheet resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the resource in the database that matches the passed-in resource's category ID (primary key)
                SpriteSheet dbResource = context.SpriteSheets.SingleOrDefault(r => r.ResourceID.Equals(resource.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.SpriteSheets.Remove(dbResource);
                    context.SpriteSheets.Add(resource);
                    context.SaveChanges();
                }
            }
        }

        public void Upsert(SpriteSheet resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                SpriteSheet dbResource = context.SpriteSheets.SingleOrDefault(r => r.ResourceID.Equals(resource.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.SpriteSheets.Remove(dbResource);
                    context.SpriteSheets.Add(resource);
                    context.SaveChanges();
                }
                else
                {
                    context.SpriteSheets.Add(resource);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Returns true if a graphic resource exists in the database.
        /// Returns false if a graphic resource does not exist in the database.
        /// </summary>
        /// <param name="resource">The resource to look in the database for. Its resource ID will be used for the comparison.</param>
        /// <returns></returns>
        public bool Exists(SpriteSheet resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                SpriteSheet dbResource = context.SpriteSheets.FirstOrDefault(r => r.ResourceID.Equals(resource.ResourceID));
                return !Object.ReferenceEquals(dbResource, null);
            }
        }

        /// <summary>
        /// Deletes a sprite sheet from the database.
        /// </summary>
        /// <param name="resource">The graphic resource to delete.</param>
        /// <returns></returns>
        public void Delete(SpriteSheet resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the graphic resource in the database. CategoryID is a primary key so there will only ever be one.
                SpriteSheet dbResource = context.SpriteSheets.SingleOrDefault(val => val.ResourceID == resource.ResourceID);

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.SpriteSheets.Remove(dbResource);
                    context.SaveChanges();
                }
            }
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
