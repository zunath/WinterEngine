using System;
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
            return Context.TilesetRepository.Add(tileset);
        }

        public void Add(List<Tileset> tilesetList)
        {
            Context.TilesetRepository.AddList(tilesetList);
        }

        public void Update(Tileset newTileset)
        {
            Tileset dbTileset = Context.TilesetRepository.Get(x => x.ResourceID == newTileset.ResourceID).SingleOrDefault();
            if (dbTileset == null) return;

            Context.Context.Entry(dbTileset).CurrentValues.SetValues(newTileset);
        }

        public void Upsert(Tileset tileset)
        {
            if (tileset.ResourceID <= 0)
            {
                Context.TilesetRepository.Add(tileset);
            }
            else
            {
                Update(tileset);
            }
        }

        public bool Exists(string resref)
        {
            Tileset tileset = Context.TilesetRepository.Get(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(tileset, null);
        }

        public void Delete(Tileset tileset)
        {
            Context.TilesetRepository.Delete(tileset);
        }

        public void Delete(int resourceID)
        {
            Tileset tileset = Context.TilesetRepository.Get(x => x.ResourceID == resourceID).SingleOrDefault();
            Context.TilesetRepository.Delete(tileset);
        }

        public Tileset GetByID(int tilesetID)
        {
            return Context.TilesetRepository.Get(x => x.ResourceID == tilesetID).SingleOrDefault();
        }

        public List<Tileset> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.TilesetRepository.Get(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
        }

        public Tileset GetByResref(string resref)
        {
            return Context.TilesetRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        public List<Tileset> GetAll()
        {
            return Context.TilesetRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects(bool includeDefault = false)
        {
            List<DropDownListUIObject> items = (from tileset
                                                in Context.TilesetRepository.Get()
                                                select new DropDownListUIObject
                                                {
                                                    Name = tileset.Name,
                                                    ResourceID = tileset.ResourceID
                                                }).ToList();
            if (includeDefault)
            {
                items.Insert(0, new DropDownListUIObject(0, "(None)"));
            }

            return items;
        }

        public void DeleteAllByCategory(Category category)
        {
            List<Tileset> tilesetList = Context.TilesetRepository.Get(x => x.ResourceCategoryID == category.ResourceID).ToList();
            Context.DeleteAll(tilesetList);
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
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectType == GameObjectTypeEnum.Tileset).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Tileset> tilesets = GetAllByResourceCategory(category);
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



        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
