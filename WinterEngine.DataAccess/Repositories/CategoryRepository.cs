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
    public class CategoryRepository : RepositoryBase, IDisposable
    {
        #region Constructors

        public CategoryRepository(string connectionString = "")
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
        /// Returns all resource categories from the database.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllResourceCategories()
        {
            List<Category> _resourceCategoryList = new List<Category>();

            using (WinterContext context = new WinterContext(ConnectionString))
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
        public List<Category> GetAllResourceCategoriesByResourceType(ResourceTypeEnum resourceType)
        {
            List<Category> categoryList = new List<Category>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from resourceCategory
                            in context.ResourceCategories
                            where resourceCategory.ResourceTypeID.Equals((int)resourceType)
                            select resourceCategory;

                categoryList = query.ToList<Category>();

            }
            

            return categoryList;
        }

        /// <summary>
        /// Adds a resource category to the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool AddResourceCategory(Category resourceCategory)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(ConnectionString))
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
        public bool UpdateResourceCategory(Category resourceCategory)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the resource in the database that matches the passed-in resource's category ID (primary key)
                Category dbResource = context.ResourceCategories.SingleOrDefault(r => r.ResourceID.Equals(resourceCategory.ResourceID));

                // Unable to find a matching resource. Do not attempt to update.
                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.ResourceCategories.Remove(dbResource);
                    context.ResourceCategories.Add(resourceCategory);
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
        /// Returns the resource category matching the resourceCategoryID passed in.
        /// Returns null if resource category does not exist.
        /// </summary>
        /// <param name="resourceCategoryID"></param>
        /// <returns></returns>
        public Category GetByResourceCategoryID(int resourceCategoryID)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.ResourceCategories.FirstOrDefault(r => r.ResourceID == resourceCategoryID);
            }
        }

        /// <summary>
        /// Returns true if a resource category exists in the database.
        /// Returns false if a resource category does not exist in the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool Exists(Category resourceCategory)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Category dbResourceCategory = context.ResourceCategories.FirstOrDefault(r => r.ResourceID.Equals(resourceCategory.ResourceID));
                return !Object.ReferenceEquals(dbResourceCategory, null);
            }
        }

        /// <summary>
        /// Deletes a resource category from the database.
        /// </summary>
        /// <param name="resourceCategory">The resource category to delete.</param>
        /// <returns></returns>
        public bool DeleteResourceCategory(Category resourceCategory)
        {
            bool success = true;

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the category in the database. CategoryID is a primary key so there will only ever be one.
                Category category = context.ResourceCategories.SingleOrDefault(val => val.ResourceID == resourceCategory.ResourceID);

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

        /// <summary>
        /// Returns the system category named "*Uncategorized" for a particular resource type.
        /// </summary>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <returns></returns>
        public Category GetUncategorizedCategory(ResourceTypeEnum resourceType)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from category
                            in context.ResourceCategories
                            where category.IsSystemResource == true && category.ResourceTypeID == (int)resourceType
                            select category;
                return query.ToList()[0];
            }
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
