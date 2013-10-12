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
            return Context.PlaceableRepository.Add(placeable);
        }

        public void Add(List<Placeable> placeableList)
        {
            Context.PlaceableRepository.AddList(placeableList);
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
                dbPlaceable = Context.PlaceableRepository.Get(x => x.Resref == newPlaceable.Resref).SingleOrDefault();
            }
            else
            {
                dbPlaceable = Context.PlaceableRepository.Get(x => x.ResourceID == newPlaceable.ResourceID).SingleOrDefault();
            }
            if (dbPlaceable == null) return;

            Context.Context.Entry(dbPlaceable).CurrentValues.SetValues(newPlaceable);
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
                Context.PlaceableRepository.Add(placeable);
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
            Placeable placeable = Context.PlaceableRepository.Get(p => p.ResourceID == resourceID).SingleOrDefault();
            Context.PlaceableRepository.Delete(placeable);
        }

        /// <summary>
        /// Returns all of the placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAll()
        {
            return Context.PlaceableRepository.Get().ToList();
        }

        /// <summary>
        /// Returns all of the placeables in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.PlaceableRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the placeable with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Placeable GetByResref(string resref)
        {
            return Context.PlaceableRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        public Placeable GetByID(int resourceID)
        {
            return Context.PlaceableRepository.Get(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the placeables attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Placeable> placeableList = Context.PlaceableRepository.Get(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.DeleteAll(placeableList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Placeable placeable = Context.PlaceableRepository.Get(x => x.Resref == resref).SingleOrDefault();
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
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectTypeID == (int)GameObjectTypeEnum.Placeable).ToList();
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


        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
