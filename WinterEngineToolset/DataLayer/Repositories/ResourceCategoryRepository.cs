using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.Enumerations;
using AutoMapper;
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
                ErrorHelper.ShowErrorDialog("Error retrieving all resource categories.", ex);
                _resourceCategoryList.Clear();
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
                ErrorHelper.ShowErrorDialog("Error retrieving specified resource category (CategoryID: " + resourceCategoryID + ", ResourceTypeID: " + resourceTypeID + ").", ex);
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
                ErrorHelper.ShowErrorDialog("Error retrieving specified resource categories (ResourceType: " + resourceType + ")", ex);
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
                ErrorHelper.ShowErrorDialog("Error adding new resource category (ResourceCategory: " + resourceCategory.ResourceName + ").", ex);
            }

            return success;
        }

        /// <summary>
        /// Updates an existing resource category with new values.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool UpdateResourceCategory(ResourceCategoryDTO resourceCategory)
        {
            bool success = true;

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    // Find the resource in the database that matches the passed-in resource's category ID (primary key)
                    ResourceCategory dbResource = context.ResourceCategories.FirstOrDefault(r => r.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID));

                    // Unable to find a matching resource. Do not attempt to update.
                    if (!Object.ReferenceEquals(dbResource, null))
                    {
                        // Map the DTO to the database object, replacing the existing object. Then save changes.
                        dbResource = Mapper.Map(resourceCategory, dbResource);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                ErrorHelper.ShowErrorDialog("Error renaming existing resource category (ResourceCategory: " + resourceCategory.ResourceName + ").", ex);
            }

            return success;
        }

        /// <summary>
        /// Deletes a resource category from the database.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public bool DeleteResourceCategory(ResourceCategoryDTO resourceCategory)
        {
            bool success = true;

            try
            {
                using (WinterContext context = new WinterContext())
                { 
                    // Find the category in the database. CategoryID is a primary key so there will only ever be one.
                    ResourceCategory category = context.ResourceCategories.FirstOrDefault(val => val.ResourceCategoryID == resourceCategory.ResourceCategoryID);
                    context.ResourceCategories.Remove(category);

                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                success = false;
                ErrorHelper.ShowErrorDialog("Error deleting resource category (ResourceCategory: " + resourceCategory.ResourceName + ").", ex);
            }

            return success;
        }

        public void Dispose()
        {
        }
    }
}
