using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;

namespace WinterEngine.Toolset.Data.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ResourceCategoryRepository
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
            ResourceCategoryDTO retResourceCategory = new ResourceCategoryDTO();

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
                    retResourceCategory = Mapper.Map(resultResourceCategories[0], retResourceCategory);
                }
            }
            catch (Exception ex)
            {
                retResourceCategory = null;
                MessageBox.Show("Error retrieving specified resource category (CategoryID: " + resourceCategoryID + ", ResourceTypeID: " + resourceTypeID + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retResourceCategory;
        }
    }
}
