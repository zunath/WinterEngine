using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;


namespace WinterEngine.DataAccess
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class CategoryRepository : RepositoryBase, IResourceRepository<Category>
    {
        #region Constructors

        public CategoryRepository(string connectionString = "", bool autoSaveChanges = true) 
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns all resource categories from the database.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAll()
        {
            return Context.ResourceCategories.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from category
                                                in Context.ResourceCategories
                                                select new DropDownListUIObject
                                                {
                                                    Name = category.Name,
                                                    ResourceID = category.ResourceID
                                                }).ToList();

            return items;
        }

        /// <summary>
        /// Returns all of the resource categories for a particular resource type.
        /// </summary>
        /// <param name="resourceType">The type of resources to get.</param>
        /// <returns></returns>
        public List<Category> GetAllResourceCategoriesByResourceType(GameObjectTypeEnum resourceType)
        {
            return Context.ResourceCategories.Where(x => x.GameObjectType.Equals(resourceType)).ToList();
        }

        /// <summary>
        /// Adds a resource category to the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public Category Add(Category resourceCategory)
        {
            return Context.ResourceCategories.Add(resourceCategory);
        }

        public void Add(List<Category> categoryList)
        {
            Context.ResourceCategories.AddRange(categoryList);
        }

        /// <summary>
        /// Updates an existing resource category with new values.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public void Update(Category resourceCategory)
        {
            Category dbCategory = Context.ResourceCategories.SingleOrDefault(x => x.ResourceID == resourceCategory.ResourceID);
            if (dbCategory == null) return;

            Context.Entry(dbCategory).CurrentValues.SetValues(resourceCategory);
        }

        public void Upsert(Category category)
        {
            if (category.ResourceID <= 0)
            {
                Context.ResourceCategories.Add(category);
            }
            else
            {
                Update(category);
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
            Category category = Context.ResourceCategories.SingleOrDefault(r => r.ResourceID.Equals(resourceCategory.ResourceID));
            return !Object.ReferenceEquals(category, null);
        }

        /// <summary>
        /// Deletes a resource category from the database.
        /// </summary>
        /// <param name="resourceCategory">The resource category to delete.</param>
        /// <returns></returns>
        public void Delete(Category resourceCategory)
        {
            Context.ResourceCategories.Remove(resourceCategory);
        }

        /// <summary>
        /// Returns a category based on its ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category GetByID(int categoryID)
        {
            return Context.ResourceCategories.SingleOrDefault(x => x.ResourceID == categoryID);
        }

        /// <summary>
        /// Returns the system category named "*Uncategorized" for a particular resource type.
        /// </summary>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <returns></returns>
        public Category GetUncategorizedCategory(GameObjectTypeEnum resourceType)
        {
            return Context.ResourceCategories.SingleOrDefault(x => x.IsSystemResource == true && x.GameObjectType == resourceType);
        }

        public int GetDefaultResourceID(GameObjectTypeEnum resourceType)
        {
            Category defaultObject = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == resourceType);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
