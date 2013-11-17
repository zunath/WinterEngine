﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;


namespace WinterEngine.DataAccess
{
    public class AreaRepository : IGameObjectRepository<Area>
    {
        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors
        public AreaRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Area InternalSave(Area area, bool saveChanges)
        {
            Area retArea;
            if (area.ResourceID <= 0)
            {
                retArea = _context.Areas.Add(area);
            }
            else
            {
                retArea = _context.Areas.SingleOrDefault(x => x.ResourceID == area.ResourceID);
                if (retArea == null) return null;
                _context.Entry(retArea).CurrentValues.SetValues(area);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retArea;
        }

        /// <summary>
        /// If an area with the same resref is in the database, it will be replaced with newArea.
        /// If an area does not exist by newArea's resref, it will be added to the database.
        /// </summary>
        /// <param name="area">The new area to upsert.</param>
        public Area Save(Area area)
        {
            return InternalSave(area, true);
        }

        public void Save(IEnumerable<Area> entityList)
        {
            if(entityList != null)
            {
                foreach(var area in entityList)
                {
                    InternalSave(area, false);
                }
                _context.SaveChanges();
            }
            
        }

        private void DeleteInternal(Area area, bool saveChanges = true)
        {
            var dbArea = _context.Areas.SingleOrDefault(x => x.ResourceID == area.ResourceID);
            if (dbArea == null) return;

            _context.Areas.Remove(dbArea);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Area area)
        {
            DeleteInternal(area);
        }

        public void Delete(int resourceID)
        {
            var area = _context.Areas.Find(resourceID);
            DeleteInternal(area);
        }

        public void Delete(IEnumerable<Area> areas)
        {
            foreach (var area in areas)
            {
                DeleteInternal(area, false);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all of the areas from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Area> GetAll()
        {
            return _context.Areas.ToList();
        }

        public Area GetByID(int resourceID)
        {
            var result = _context.Areas.Where(x => x.ResourceID == resourceID).SingleOrDefault();
            return result;
        }
        
        /// <summary>
        /// Returns all of the areas in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Area> GetAllByResourceCategory(Category resourceCategory)
        {
            var result = _context.Areas.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
            return result;
        }


        /// <summary>
        /// Returns the area with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Area GetByResref(string resref)
        {
            var result = _context.Areas.Where(x => x.Resref == resref).SingleOrDefault();
            return result;
        }

        /// <summary>
        /// Deletes all of the areas attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Area> areaList = _context.Areas.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            _context.Areas.RemoveRange(areaList);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing areas for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and areas.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Areas");
            rootNode.attr.Add("data-nodetype", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = _context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Area).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));
                
                List<Area> areas = _context.Areas.Where(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
                foreach (Area area in areas)
                {
                    JSTreeNode childNode = new JSTreeNode(area.Name);
                    childNode.attr.Add("data-nodetype", "object");
                    childNode.attr.Add("data-resourceid", Convert.ToString(area.ResourceID));
                    childNode.attr.Add("data-issystemresource", Convert.ToString(area.IsSystemResource));

                    categoryNode.children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            rootNode.children = treeNodes;
            return rootNode;
        }


        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Area area = _context.Areas.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(area, null);
        }

        //public int GetDefaultResourceID()
        //{
        //    Area defaultObject = _context.Areas.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

        #endregion

        
    }
}
