using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;



namespace WinterEngine.DataAccess
{
    public class PlaceableRepository : RepositoryBase, IGameObjectRepository<Placeable>
    {
        #region Constructors

        public PlaceableRepository(ModuleDataContext context, bool autoSaveChanges = true)
            : base(context, autoSaveChanges)
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
            return _context.Placeables.Add(placeable);
        }

        public void Add(List<Placeable> placeableList)
        {
            _context.Placeables.AddRange(placeableList);
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
                dbPlaceable = _context.Placeables.Where(x => x.Resref == newPlaceable.Resref).SingleOrDefault();
            }
            else
            {
                dbPlaceable = _context.Placeables.Where(x => x.ResourceID == newPlaceable.ResourceID).SingleOrDefault();
            }
            if (dbPlaceable == null) return;

            if (newPlaceable.GraphicResourceID <= 0)
            {
                newPlaceable.GraphicResourceID = null;
            }

            _context.Entry(dbPlaceable).CurrentValues.SetValues(newPlaceable);
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
                _context.Placeables.Add(placeable);
            }
            else
            {
                Update(placeable);
            }
        }

        /// <summary>
        /// Removes a placeable from the database
        /// </summary>
        /// <param name="placeable"></param>
        void IGenericRepository<Placeable>.Delete(Placeable placeable)
        {
            this.Delete(placeable.ResourceID);
        }

        /// <summary>
        /// Removes a placeable with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and Remove.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Placeable placeable = _context.Placeables.Where(p => p.ResourceID == resourceID).SingleOrDefault();
            _context.Placeables.Remove(placeable);
        }

        /// <summary>
        /// Returns all of the placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAll()
        {
            return _context.Placeables.ToList();
        }

        /// <summary>
        /// Returns all of the placeables in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Placeables.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the placeable with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Placeable GetByResref(string resref)
        {
            return _context.Placeables.Where(x => x.Resref == resref).SingleOrDefault();
        }

        public Placeable GetByID(int resourceID)
        {
            return _context.Placeables.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        /// <summary>
        /// Removes all of the placeables attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Placeable> placeableList = _context.Placeables.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            _context.Placeables.RemoveRange(placeableList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Placeable placeable = _context.Placeables.Where(x => x.Resref == resref).SingleOrDefault();
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
            List<Category> categories = _context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Placeable).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Placeable> placeables = GetAllByResourceCategory(category);
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

        #endregion
    }
}
