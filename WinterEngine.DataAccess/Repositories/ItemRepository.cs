﻿using System;
using System.Linq;
using System.Collections.Generic;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataAccess.Repositories.Interfaces;

namespace WinterEngine.DataAccess
{
    public class ItemRepository : RepositoryBase, IGameObjectRepository<Item>
    {
        #region Constructors

        public ItemRepository(ModuleDataContext context, bool autoSaveChanges = true)
            : base(context, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">The item to add to the database.</param>
        /// <returns></returns>
        public Item Add(Item item)
        {
            return _context.Items.Add(item);
        }

        public void Add(List<Item> itemList)
        {
            _context.Items.AddRange(itemList);
        }

        /// <summary>
        /// Updates an existing item in the database with new values.
        /// </summary>
        /// <param name="newItem">The new item that will replace the item with the matching resref.</param>
        public void Update(Item newItem)
        {
            Item dbItem;
            if (newItem.ResourceID <= 0)
            {
                dbItem = _context.Items.Where(x => x.Resref == newItem.Resref).SingleOrDefault();
            }
            else
            {
                dbItem = _context.Items.Where(x => x.ResourceID == newItem.ResourceID).SingleOrDefault();
            }
            if (dbItem == null) return;

            if (newItem.GraphicResourceID <= 0)
            {
                newItem.GraphicResourceID = null;
            }

            _context.Entry(dbItem).CurrentValues.SetValues(newItem);
        }

        /// <summary>
        /// If an item with the same resref is in the database, it will be replaced with newItem.
        /// If an item does not exist by newItem's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new item to upsert.</param>
        public void Upsert(Item item)
        {
            if (item.ResourceID <= 0)
            {
                _context.Items.Add(item);
            }
            else
            {
                Update(item);
            }
        }

        /// <summary>
        /// Deletes an item with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Item item = _context.Items.Where(i => i.ResourceID == resourceID).SingleOrDefault();
            _context.Items.Remove(item);
        }

        /// <summary>
        /// Returns all of the items from the database.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAll()
        {
            return _context.Items.ToList();
        }

        /// <summary>
        /// Returns all of the items in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Items.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the item with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Item GetByResref(string resref)
        {
            return _context.Items.Where(x => x.Resref == resref).SingleOrDefault();
        }

        public Item GetByID(int resourceID)
        {
            return _context.Items.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the items attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Item> itemList = _context.Items.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            _context.Items.RemoveRange(itemList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Item item = _context.Items.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(item, null);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing items for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and items.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Items");
            rootNode.attr.Add("data-nodetype", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = _context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Item).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Item> items = GetAllByResourceCategory(category);
                foreach (Item item in items)
                {
                    JSTreeNode childNode = new JSTreeNode(item.Name);
                    childNode.attr.Add("data-nodetype", "object");
                    childNode.attr.Add("data-resourceid", Convert.ToString(item.ResourceID));
                    childNode.attr.Add("data-issystemresource", Convert.ToString(item.IsSystemResource));

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
