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
    public class CategoryRepository : IGenericRepository<Category>, ICategoryRepository
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

        private Category InternalSave(Category category, bool saveChanges)
        {
            Category retCategory;
            if (category.ResourceID <= 0)
            {
                retCategory = _context.ResourceCategories.Add(category);
            }
            else
            {
                retCategory = _context.ResourceCategories.SingleOrDefault(x => x.ResourceID == category.ResourceID);
                if (retCategory == null) return null;
                _context.Entry(retCategory).CurrentValues.SetValues(category);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retCategory;
        }

        public Category Save(Category category)
        {
            return InternalSave(category, true);
        }

        public void Save(IEnumerable<Category> entityList)
        {
            if(entityList != null)
            {
                foreach(var cat in entityList)
                {
                    InternalSave(cat, false);
                }
                _context.SaveChanges();
            }
        }

        private void DeleteInternal(Category ability, bool saveChanges = true)
        {
            var dbCategory = _context.ResourceCategories.SingleOrDefault(x => x.ResourceID == ability.ResourceID);
            if (dbCategory == null) return;

            _context.ResourceCategories.Remove(ability);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Category category)
        {
            DeleteInternal(category);
        }

        public void Delete(int resourceID)
        {
            var category = _context.ResourceCategories.Find(resourceID);
            DeleteInternal(category);
        }

        public void Delete(IEnumerable<Category> categoryList)
        {
            foreach (var category in categoryList)
            {
                DeleteInternal(category, false);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all resource categories from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAll()
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

        public IEnumerable<Category> GetAllResourceCategoriesByResourceType(GameObjectTypeEnum resourceType)
        {
            throw new NotImplementedException();
        }

        public Category GetUncategorizedCategory(GameObjectTypeEnum resourceType)
        {
            throw new NotImplementedException();
        }

        #endregion

        
    }
}
