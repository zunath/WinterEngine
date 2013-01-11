using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.Enumerations;
using System.Windows.Forms;
using WinterEngine.Toolset.Helpers;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ResourceCategoryRepository : IDisposable
    {
        /// <summary>
        /// Returns all resource categories from the database.
        /// </summary>
        /// <returns></returns>
        public List<ResourceCategory> GetAllResourceCategories()
        {
            List<ResourceCategory> _resourceCategoryList = new List<ResourceCategory>();

            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from resourceCategory
                            in context.ResourceCategories
                            select resourceCategory;
                _resourceCategoryList = query.ToList();
            }
            
            return _resourceCategoryList;
        }

        /// <summary>
        /// Returns all of the resource categories for a particular resource type.
        /// </summary>
        /// <param name="resourceType">The type of resources to get.</param>
        /// <returns></returns>
        public List<ResourceCategory> GetAllResourceCategoriesByResourceType(ResourceTypeEnum resourceType)
        {
            List<ResourceCategory> categoryList = new List<ResourceCategory>();

            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from resourceCategory
                            in context.ResourceCategories
                            where resourceCategory.ResourceTypeID.Equals((int)resourceType)
                            select resourceCategory;

                categoryList = query.ToList<ResourceCategory>();

            }
            

            return categoryList;
        }

        /// <summary>
        /// Adds a resource category to the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool AddResourceCategory(ResourceCategory resourceCategory)
        {
            bool success = true;
            
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.ResourceCategories.Add(resourceCategory);
                context.SaveChanges();
            }
            

            return success;
        }

        /// <summary>
        /// Updates an existing resource category with new values.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool UpdateResourceCategory(ResourceCategory resourceCategory)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                // Find the resource in the database that matches the passed-in resource's category ID (primary key)
                ResourceCategory dbResource = context.ResourceCategories.SingleOrDefault(r => r.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID));

                // Unable to find a matching resource. Do not attempt to update.
                if (!Object.ReferenceEquals(dbResource, null))
                {
                    dbResource = resourceCategory;
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
        /// Returns true if a resource category exists in the database.
        /// Returns false if a resource category does not exist in the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool Exists(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                ResourceCategory dbResourceCategory = context.ResourceCategories.FirstOrDefault(r => r.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID));
                return !Object.ReferenceEquals(dbResourceCategory, null);
            }
        }

        /// <summary>
        /// Deletes a resource category from the database.
        /// </summary>
        /// <param name="resourceCategory">The resource category to delete.</param>
        /// <returns></returns>
        public bool DeleteResourceCategory(ResourceCategory resourceCategory)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                // Find the category in the database. CategoryID is a primary key so there will only ever be one.
                ResourceCategory category = context.ResourceCategories.SingleOrDefault(val => val.ResourceCategoryID == resourceCategory.ResourceCategoryID);

                if (!Object.ReferenceEquals(category, null))
                {
                    context.ResourceCategories.Remove(category);
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
    }
}
