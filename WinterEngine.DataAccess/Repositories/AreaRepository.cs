﻿using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;


namespace WinterEngine.DataAccess
{
    public class AreaRepository : RepositoryBase, IGameObjectRepository<Area>
    {
        #region Constructors

        public AreaRepository(string connectionString = "", bool autoSaveChanges = true) 
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an area to the database.
        /// </summary>
        /// <param name="area">The area to add to the database.</param>
        /// <returns></returns>
        public void Add(Area area)
        {
            Context.AreaRepository.Add(area);
        }

        /// <summary>
        /// Adds a list of areas to the database.
        /// </summary>
        /// <param name="areaList">The list of areas to add to the database.</param>
        public void Add(List<Area> areaList)
        {
            Context.AreaRepository.AddList(areaList);   
        }

        /// <summary>
        /// Updates an existing area in the database with new values.
        /// </summary>
        /// <param name="newItem">The new area that will replace the area with the matching resref.</param>
        public void Update(Area newArea)
        {
            Context.AreaRepository.Update(newArea);
        }

        /// <summary>
        /// If an area with the same resref is in the database, it will be replaced with newArea.
        /// If an area does not exist by newArea's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new area to upsert.</param>
        public void Upsert(Area area)
        {
            if (area.ResourceID <= 0)
            {
                Context.AreaRepository.Add(area);
            }
            else
            {
                Context.AreaRepository.Update(area);
            }
        }

        /// <summary>
        /// Deletes an area with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            Area area = Context.AreaRepository.Get(a => a.Resref == resref).SingleOrDefault();
            Context.AreaRepository.Delete(area);
        }

        /// <summary>
        /// Returns all of the areas from the database.
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAll()
        {
            return Context.AreaRepository.Get().ToList();
        }

        /// <summary>
        /// Returns all of the areas in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.AreaRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the area with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Area GetByResref(string resref)
        {
            return Context.AreaRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the areas attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Area> areaList = Context.AreaRepository.Get(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.DeleteAll(areaList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Area area = Context.AreaRepository.Get(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(area, null);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing areas for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and areas.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Areas");
            rootNode.attr.Add("data-nodeType", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectTypeID == (int)GameObjectTypeEnum.Area).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodeType", "category");
                categoryNode.attr.Add("data-categoryID", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-isSystemResource", Convert.ToString(category.IsSystemResource));
                
                List<Area> areas = GetAllByResourceCategory(category);
                foreach (Area area in areas)
                {
                    JSTreeNode childNode = new JSTreeNode(area.Name);
                    childNode.attr.Add("data-nodeType", "object");
                    childNode.attr.Add("data-resref", area.Resref);

                    categoryNode.children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            rootNode.children = treeNodes;
            return rootNode;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
