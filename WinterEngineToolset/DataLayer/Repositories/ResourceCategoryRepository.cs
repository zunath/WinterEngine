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
    public class ResourceCategoryRepository : IRepository, IDisposable
    {
        /// <summary>
        /// Returns all resource categories from the database.
        /// </summary>
        /// <returns></returns>
        public List<ResourceCategory> GetAllResourceCategories()
        {
            List<ResourceCategory> _resourceCategoryList = new List<ResourceCategory>();

            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    var query = from resourceCategory
                                in context.ResourceCategories
                                select resourceCategory;
                    _resourceCategoryList = query.ToList();
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
        public ResourceCategory GetResourceCategoryByID(int resourceCategoryID, int resourceTypeID)
        {
            ResourceCategory retResourceCategoryDTO = new ResourceCategory();

            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    var query = from resourceCategory
                                in context.ResourceCategories
                                where resourceCategory.ResourceCategoryID.Equals(resourceCategoryID) && 
                                      resourceCategory.ResourceTypeID.Equals(resourceTypeID)
                                select resourceCategory;
                    retResourceCategoryDTO = query.ToList()[0];
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error retrieving specified resource category (CategoryID: " + resourceCategoryID + ", ResourceTypeID: " + resourceTypeID + ").", ex);
            }

            return retResourceCategoryDTO;
        }

        public List<ResourceCategory> GetAllResourceCategoriesByResourceType(ResourceTypeEnum resourceType)
        {
            List<ResourceCategory> categoryList = new List<ResourceCategory>();

            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    var query = from resourceCategory
                                in context.ResourceCategories
                                where resourceCategory.ResourceTypeID.Equals((int)resourceType)
                                select resourceCategory;

                    categoryList = query.ToList<ResourceCategory>();

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
        public bool AddResourceCategory(ResourceCategory resourceCategory)
        {
            bool success = true;
            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    context.ResourceCategories.Add(resourceCategory);
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
        public bool UpdateResourceCategory(ResourceCategory resourceCategory)
        {
            bool success = true;

            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    // Find the resource in the database that matches the passed-in resource's category ID (primary key)
                    ResourceCategory dbResource = context.ResourceCategories.FirstOrDefault(r => r.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID));

                    // Unable to find a matching resource. Do not attempt to update.
                    if (!Object.ReferenceEquals(dbResource, null))
                    {
                        // Map the DTO to the database object, replacing the existing object. Then save changes.
                        dbResource = resourceCategory;
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
        public bool DeleteResourceCategory(ResourceCategory resourceCategory)
        {
            bool success = true;

            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
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
