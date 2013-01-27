using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class GraphicResourceRepository : RepositoryBase, IDisposable
    {
        #region Constructors

        public GraphicResourceRepository(string connectionString = "")
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
        public List<GraphicResource> GetAllGraphicResources()
        {
            List<GraphicResource> _resourceList = new List<GraphicResource>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from resource
                            in context.GraphicResources
                            select resource;
                _resourceList = query.ToList();
            }

            return _resourceList;
        }

        /// <summary>
        /// Returns all of the graphic resources for a particular resource type.
        /// </summary>
        /// <param name="resourceType">The type of resources to get.</param>
        /// <returns></returns>
        public List<GraphicResource> GetAllGraphicResourcesByResourceType(ResourceTypeEnum resourceType)
        {
            List<GraphicResource> categoryList = new List<GraphicResource>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from resource
                            in context.GraphicResources
                            where resource.ResourceTypeID.Equals((int)resourceType)
                            select resource;

                categoryList = query.ToList<GraphicResource>();

            }

            return categoryList;
        }

        /// <summary>
        /// Adds a graphic resource to the database.
        /// </summary>
        /// <param name="resource">The graphic resource to add to the database.</param>
        /// <returns></returns>
        public bool AddGraphicResource(GraphicResource resource)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.GraphicResources.Add(resource);
                context.SaveChanges();
            }

            return success;
        }

        /// <summary>
        /// Updates an existing graphic resource with new values.
        /// </summary>
        /// <param name="resource">The resource to update. Its ID must match the ID of an entry in the database.</param>
        /// <returns></returns>
        public bool UpdateGraphicResource(GraphicResource resource)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the resource in the database that matches the passed-in resource's category ID (primary key)
                GraphicResource dbResource = context.GraphicResources.SingleOrDefault(r => r.ResourceID.Equals(resource.ResourceID));

                // Unable to find a matching resource. Do not attempt to update.
                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.GraphicResources.Remove(dbResource);
                    context.GraphicResources.Add(resource);
                    context.SaveChanges();
                }
                // Unable to find a matching resource. Return false.
                else
                {
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Returns the graphic resource matching the resourceCategoryID passed in.
        /// Returns null if graphic resource does not exist.
        /// </summary>
        /// <param name="resourceID">The unique ID number of a graphic resource</param>
        /// <returns></returns>
        public GraphicResource GetByGraphicResourceID(int resourceID)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.GraphicResources.FirstOrDefault(r => r.ResourceID == resourceID);
            }
        }

        /// <summary>
        /// Returns true if a graphic resource exists in the database.
        /// Returns false if a graphic resource does not exist in the database.
        /// </summary>
        /// <param name="resource">The resource to look in the database for. Its resource ID will be used for the comparison.</param>
        /// <returns></returns>
        public bool Exists(GraphicResource resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                GraphicResource dbResource = context.GraphicResources.FirstOrDefault(r => r.ResourceID.Equals(resource.ResourceID));
                return !Object.ReferenceEquals(dbResource, null);
            }
        }

        /// <summary>
        /// Deletes a graphic resource from the database.
        /// </summary>
        /// <param name="resource">The graphic resource to delete.</param>
        /// <returns></returns>
        public bool DeleteResourceCategory(GraphicResource resource)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the graphic resource in the database. CategoryID is a primary key so there will only ever be one.
                GraphicResource dbResource = context.GraphicResources.SingleOrDefault(val => val.ResourceID == resource.ResourceID);

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.GraphicResources.Remove(dbResource);
                    context.SaveChanges();
                }
                // Unable to locate a resource in the database. Return false.
                else
                {
                    success = false;
                }
            }

            return success;
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
