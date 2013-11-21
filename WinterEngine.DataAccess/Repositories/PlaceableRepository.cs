using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;



namespace WinterEngine.DataAccess
{
    public class PlaceableRepository : RepositoryBase, IGameObjectRepository<Placeable>
    {
        #region Constructors

        public PlaceableRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a placeable to the database.
        /// </summary>
        /// <param name="placeable">The placeable to add to the database.</param>
        /// <returns></returns>
        public Placeable Add(Placeable placeable)
        {
            return Context.Placeables.Add(placeable);
        }

        public void Add(List<Placeable> placeableList)
        {
            Context.Placeables.AddRange(placeableList);
        }

        /// <summary>
        /// Updates an existing placeable in the database with new values.
        /// </summary>
        /// <param name="newItem">The new placeable that will replace the placeable with the matching resref.</param>
        public void Update(Placeable newPlaceable)
        {
            Placeable dbPlaceable;
            if (newPlaceable.ResourceID <= 0)
            {
                dbPlaceable = Context.Placeables.SingleOrDefault(x => x.Resref == newPlaceable.Resref);
            }
            else
            {
                dbPlaceable = Context.Placeables.SingleOrDefault(x => x.ResourceID == newPlaceable.ResourceID);
            }
            if (dbPlaceable == null) return;

            foreach (LocalVariable variable in newPlaceable.LocalVariables)
            {
                variable.GameObjectBaseID = newPlaceable.ResourceID;
            }

            Context.Entry(dbPlaceable).CurrentValues.SetValues(newPlaceable);
            Context.LocalVariables.RemoveRange(dbPlaceable.LocalVariables.ToList());
            Context.LocalVariables.AddRange(newPlaceable.LocalVariables.ToList());
        }

        /// <summary>
        /// If an placeable with the same resref is in the database, it will be replaced with newPlaceable.
        /// If an placeable does not exist by newPlaceable's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new placeable to upsert.</param>
        public void Upsert(Placeable placeable)
        {
            if (placeable.ResourceID <= 0)
            {
                Context.Placeables.Add(placeable);
            }
            else
            {
                Update(placeable);
            }
        }

        /// <summary>
        /// Deletes a placeable with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Placeable placeable = Context.Placeables.SingleOrDefault(p => p.ResourceID == resourceID);
            Context.LocalVariables.RemoveRange(placeable.LocalVariables.ToList());
            Context.Placeables.Remove(placeable);
        }

        /// <summary>
        /// Returns all of the placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAll()
        {
            return Context.Placeables.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from placeable
                                                in Context.Placeables
                                                select new DropDownListUIObject
                                                {
                                                    Name = placeable.Name,
                                                    ResourceID = placeable.ResourceID
                                                }).ToList();
            return items;
        }

        /// <summary>
        /// Returns all of the placeables in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.Placeables.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the placeable with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Placeable GetByResref(string resref)
        {
            return Context.Placeables.SingleOrDefault(x => x.Resref == resref);
        }

        public Placeable GetByID(int resourceID)
        {
            return Context.Placeables.SingleOrDefault(x => x.ResourceID == resourceID);
        }

        /// <summary>
        /// Deletes all of the placeables attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Placeable> placeableList = Context.Placeables.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.Placeables.RemoveRange(placeableList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Placeable placeable = Context.Placeables.SingleOrDefault(x => x.Resref == resref);
            return !Object.ReferenceEquals(placeable, null);
        }


        /// <summary>
        /// Generates a hierarchy of categories containing placeables for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and placeables.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Placeables");
            rootNode.attr.Add("data-nodetype", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Placeable).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Placeable> placeables = Context.Placeables.Where(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
                foreach (Placeable placeable in placeables)
                {
                    JSTreeNode childNode = new JSTreeNode(placeable.Name);
                    childNode.attr.Add("data-nodetype", "object");
                    childNode.attr.Add("data-resourceid", Convert.ToString(placeable.ResourceID));
                    childNode.attr.Add("data-issystemresource", Convert.ToString(placeable.IsSystemResource));

                    categoryNode.children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            rootNode.children = treeNodes;
            return rootNode;
        }

        public int GetDefaultResourceID()
        {
            Placeable defaultObject = Context.Placeables.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
