using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class CategoryRepository : IResourceRepository<Category>
    {
        #region Constructors

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        public CategoryRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a resource category to the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public Category Add(Category resourceCategory)
        {
            return _context.ResourceCategories.Add(resourceCategory);
        }

        public void Add(List<Category> categoryList)
        {
            _context.ResourceCategories.AddRange(categoryList);
        }

        public void Save(Category category)
        {
            if (category.ResourceID <= 0)
            {
                _context.ResourceCategories.Add(category);
            }
            else
            {
                Update(category);
            }
        }

        /// <summary>
        /// Updates an existing resource category with new values.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public void Update(Category resourceCategory)
        {
            Category dbCategory = _context.ResourceCategories.Where(x => x.ResourceID == resourceCategory.ResourceID).SingleOrDefault();
            if (dbCategory == null) return;

            _context.Entry(dbCategory).CurrentValues.SetValues(resourceCategory);
        }

        /// <summary>
        /// Deletes a resource category from the database.
        /// </summary>
        /// <param name="resourceCategory">The resource category to delete.</param>
        /// <returns></returns>
        public void Delete(Category resourceCategory)
        {
            _context.ResourceCategories.Remove(resourceCategory);
        }

        public void Delete(int resourceID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all resource categories from the database.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAll()
        {
            var result = _context.ResourceCategories.ToList();
            return result;
            //return Context.CategoryRepository.Get().ToList();
        }

        /// <summary>
        /// Returns a category based on its ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category GetByID(int categoryID)
        {
            return _context.ResourceCategories.Where(x => x.ResourceID == categoryID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        ///// <summary>
        ///// Returns all of the resource categories for a particular resource type.
        ///// </summary>
        ///// <param name="resourceType">The type of resources to get.</param>
        ///// <returns></returns>
        //public List<Category> GetAllResourceCategoriesByResourceType(GameObjectTypeEnum resourceType)
        //{
        //    return _context.ResourceCategories.Where(x => x.GameObjectType.Equals(resourceType)).ToList();
        //}      

        ///// <summary>
        ///// Returns true if a resource category exists in the database.
        ///// Returns false if a resource category does not exist in the database.
        ///// </summary>
        ///// <param name="resourceCategory"></param>
        ///// <returns></returns>
        //public bool Exists(Category resourceCategory)
        //{
        //    Category category = _context.ResourceCategories.Where(r => r.ResourceID.Equals(resourceCategory.ResourceID)).SingleOrDefault();
        //    return !Object.ReferenceEquals(category, null);
        //}

        

        ///// <summary>
        ///// Returns the system category named "*Uncategorized" for a particular resource type.
        ///// </summary>
        ///// <param name="resourceType">The type of resource to look for.</param>
        ///// <returns></returns>
        //public Category GetUncategorizedCategory(GameObjectTypeEnum resourceType)
        //{
        //    return _context.ResourceCategories.Where(x => x.IsSystemResource == true && x.GameObjectType == resourceType).SingleOrDefault();
        //}

        #endregion

        
    }
}
