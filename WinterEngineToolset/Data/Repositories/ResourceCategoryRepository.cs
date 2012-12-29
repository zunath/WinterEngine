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
        public List<ResourceCategoryDTO> GetAllResourceCategories()
        {
            List<ResourceCategoryDTO> _resourceCategoryList = new List<ResourceCategoryDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var resourceCategories = from resourceCategory
                                             in context.ResourceCategories
                                             select resourceCategory;
                    _resourceCategoryList = Mapper.Map(resourceCategories.ToList<ResourceCategory>(), _resourceCategoryList);
                }
            }
            catch (Exception ex)
            {
                _resourceCategoryList.Clear();
                MessageBox.Show("Error retrieving all resource categories.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            return _resourceCategoryList;
        }
    }
}
