using System;
using System.Linq;
using System.Collections.Generic;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;



namespace WinterEngine.DataAccess
{
    public class ItemRepository : RepositoryBase, IGameObjectRepository<Item>
    {
        #region Constructors

        public ItemRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
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
            return Context.ItemRepository.Add(item);
        }

        public void Add(List<Item> itemList)
        {
            Context.ItemRepository.AddList(itemList);
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
                dbItem = Context.ItemRepository.Get(x => x.Resref == newItem.Resref).SingleOrDefault();
            }
            else
            {
                dbItem = Context.ItemRepository.Get(x => x.ResourceID == newItem.ResourceID).SingleOrDefault();
            }
            if (dbItem == null) return;

            foreach (LocalVariable variable in newItem.LocalVariables)
            {
                variable.GameObjectBaseID = newItem.ResourceID;
            }

            Context.Context.Entry(dbItem).CurrentValues.SetValues(newItem);
            Context.LocalVariableRepository.DeleteList(dbItem.LocalVariables.ToList());
            Context.LocalVariableRepository.AddList(newItem.LocalVariables.ToList());
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
                Context.ItemRepository.Add(item);
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
            Item item = Context.ItemRepository.Get(i => i.ResourceID == resourceID).SingleOrDefault();
            Context.LocalVariableRepository.DeleteList(item.LocalVariables.ToList());
            Context.ItemRepository.Delete(item);
        }

        /// <summary>
        /// Returns all of the items from the database.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAll()
        {
            return Context.ItemRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.ItemRepository.Get()
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();

            return items;
        }

        /// <summary>
        /// Returns all of the items in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.ItemRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the item with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Item GetByResref(string resref)
        {
            return Context.ItemRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        public Item GetByID(int resourceID)
        {
            return Context.ItemRepository.Get(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the items attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Item> itemList = Context.ItemRepository.Get(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.DeleteAll(itemList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Item item = Context.ItemRepository.Get(x => x.Resref == resref).SingleOrDefault();
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
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectType == GameObjectTypeEnum.Item).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Item> items = Context.ItemRepository.Get(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
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

        public int GetDefaultResourceID()
        {
            Item defaultObject = Context.ItemRepository.Get(x => x.IsDefault).FirstOrDefault();
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
