using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects;
using WinterEngine.Toolset.Enumerations;
using AutoMapper;
using System.Windows.Forms;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ResourceCategoryRepository: IDisposable
    {
        /// <summary>
        /// Returns all resource categories from the database.
        /// </summary>
        /// <returns></returns>
        public List<ResourceCategoryDTO> GetAllResourceCategories()
        {
            List<ResourceCategoryDTO> _resourceCategoryList = new List<ResourceCategoryDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from resourceCategory
                                in context.ResourceCategories
                                select resourceCategory;
                    _resourceCategoryList = Mapper.Map(query.ToList<ResourceCategory>(), _resourceCategoryList);
                }
            }
            catch (Exception ex)
            {
                _resourceCategoryList.Clear();
                MessageBox.Show("Error retrieving all resource categories.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _resourceCategoryList;
        }

        /// <summary>
        /// Returns a specified resource category by ResourceCategoryID and ResourceTypeID from the database.
        /// </summary>
        /// <param name="resourceCategoryID"></param>
        /// <param name="resourceTypeID"></param>
        /// <returns></returns>
        public ResourceCategoryDTO GetResourceCategoryByID(int resourceCategoryID, int resourceTypeID)
        {
            ResourceCategoryDTO retResourceCategoryDTO = new ResourceCategoryDTO();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from resourceCategory
                                in context.ResourceCategories
                                where resourceCategory.ResourceCategoryID.Equals(resourceCategoryID) && 
                                      resourceCategory.ResourceTypeID.Equals(resourceTypeID)
                                select resourceCategory;
                    List<ResourceCategory> resultResourceCategories = query.ToList<ResourceCategory>();
                    retResourceCategoryDTO = Mapper.Map(resultResourceCategories[0], retResourceCategoryDTO);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving specified resource category (CategoryID: " + resourceCategoryID + ", ResourceTypeID: " + resourceTypeID + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retResourceCategoryDTO;
        }

        public List<ResourceCategoryDTO> GetAllResourceCategoriesByResourceType(ResourceTypeEnum resourceType)
        {
            List<ResourceCategoryDTO> categoryList = new List<ResourceCategoryDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from resourceCategory
                                in context.ResourceCategories
                                where resourceCategory.ResourceTypeID.Equals((int)resourceType)
                                select resourceCategory;

                    List<ResourceCategory> resultResourceCategories = query.ToList<ResourceCategory>();
                    categoryList = Mapper.Map(resultResourceCategories, categoryList);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving specified resource categories (ResourceType: " + resourceType + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return categoryList;
        }

        /// <summary>
        /// Adds a resource category to the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool AddResourceCategory(ResourceCategoryDTO resourceCategory)
        {
            bool success = true;
            try
            {
                using (WinterContext context = new WinterContext())
                {
                    ResourceCategory category = new ResourceCategory();
                    category = Mapper.Map(resourceCategory, category);
                    context.ResourceCategories.Add(category);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show("Error adding new resource category (ResourceCategory: " + resourceCategory.ResourceName + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return success;
        }
        
        public void Dispose()
        {
        }
    }
}
