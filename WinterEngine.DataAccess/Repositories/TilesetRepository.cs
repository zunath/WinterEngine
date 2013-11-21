﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class TilesetRepository : RepositoryBase, IGameObjectRepository<Tileset>
    {
        #region Constructors

        public TilesetRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public Tileset Add(Tileset tileset)
        {
            return Context.Tilesets.Add(tileset);
        }

        public void Add(List<Tileset> tilesetList)
        {
            Context.Tilesets.AddRange(tilesetList);
        }

        public void Update(Tileset newTileset)
        {
            Tileset dbTileset = Context.Tilesets.SingleOrDefault(x => x.ResourceID == newTileset.ResourceID);
            if (dbTileset == null) return;

            Context.Entry(dbTileset).CurrentValues.SetValues(newTileset);
        }

        public void Upsert(Tileset tileset)
        {
            if (tileset.ResourceID <= 0)
            {
                Context.Tilesets.Add(tileset);
            }
            else
            {
                Update(tileset);
            }
        }

        public bool Exists(string resref)
        {
            Tileset tileset = Context.Tilesets.SingleOrDefault(x => x.Resref == resref);
            return !Object.ReferenceEquals(tileset, null);
        }

        public void Delete(int resourceID)
        {
            Tileset tileset = Context.Tilesets.SingleOrDefault(x => x.ResourceID == resourceID);
            Context.LocalVariables.RemoveRange(tileset.LocalVariables.ToList());
            Context.Tilesets.Remove(tileset);
        }

        public Tileset GetByID(int tilesetID)
        {
            return Context.Tilesets.SingleOrDefault(x => x.ResourceID == tilesetID);
        }

        public List<Tileset> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.Tilesets.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
        }

        public Tileset GetByResref(string resref)
        {
            return Context.Tilesets.SingleOrDefault(x => x.Resref == resref);
        }

        public List<Tileset> GetAll()
        {
            return Context.Tilesets.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from tileset
                                                in Context.Tilesets
                                                select new DropDownListUIObject
                                                {
                                                    Name = tileset.Name,
                                                    ResourceID = tileset.ResourceID
                                                }).ToList();

            return items;
        }

        public void DeleteAllByCategory(Category category)
        {
            List<Tileset> tilesetList = Context.Tilesets.Where(x => x.ResourceCategoryID == category.ResourceID).ToList();
            Context.Tilesets.RemoveRange(tilesetList);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing scripts for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and scripts.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Tilesets");
            rootNode.attr.Add("data-nodetype", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Tileset).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Tileset> tilesets = Context.Tilesets.Where(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
                foreach (Tileset tileset in tilesets)
                {
                    JSTreeNode childNode = new JSTreeNode(tileset.Name);
                    childNode.attr.Add("data-nodetype", "object");
                    childNode.attr.Add("data-resourceid", Convert.ToString(tileset.ResourceID));
                    childNode.attr.Add("data-issystemresource", Convert.ToString(tileset.IsSystemResource));

                    categoryNode.children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            rootNode.children = treeNodes;
            return rootNode;
        }

        public int GetDefaultResourceID()
        {
            Tileset defaultObject = Context.Tilesets.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
