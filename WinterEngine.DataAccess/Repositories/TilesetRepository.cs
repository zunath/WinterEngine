using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class TilesetRepository : IGameObjectRepository<Tileset>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public TilesetRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Tileset InternalSave(Tileset tileSet, bool saveChanges)
        {
            Tileset retTileset;
            if (tileSet.ResourceID <= 0)
            {
                retTileset = _context.Tilesets.Add(tileSet);
            }
            else
            {
                retTileset = _context.Tilesets.SingleOrDefault(x => x.ResourceID == tileSet.ResourceID);
                if (retTileset == null) return null;
                _context.Entry(retTileset).CurrentValues.SetValues(tileSet);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retTileset;
        }

        public Tileset Save(Tileset tileset)
        {
            return InternalSave(tileset, true);
        }

        public void Save(IEnumerable<Tileset> entityList)
        {
            if (entityList != null)
            {
                foreach (var tileset in entityList)
                {
                    InternalSave(tileset, false);
                }
                _context.SaveChanges();
            }
        }

        public void Update(Tileset newTileset)
        {
            Tileset dbTileset = _context.Tilesets.Where(x => x.ResourceID == newTileset.ResourceID).SingleOrDefault();
            if (dbTileset == null) return;

            _context.Entry(dbTileset).CurrentValues.SetValues(newTileset);
        }

        private void DeleteInternal(Tileset tileset, bool saveChanges = true)
        {
            Tileset dbTileset = _context.Tilesets.SingleOrDefault(x => x.ResourceID == tileset.ResourceID);
            if (dbTileset == null) return;

            _context.LocalVariables.RemoveRange(dbTileset.LocalVariables.ToList());
            _context.Tilesets.Remove(dbTileset);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Tileset tileset)
        {
            DeleteInternal(tileset, true);
        }

        public void Delete(int resourceID)
        {
            Tileset tileset = _context.Tilesets.Where(x => x.ResourceID == resourceID).SingleOrDefault();
            DeleteInternal(tileset, true);
        }

        public void Delete(IEnumerable<Tileset> tilesetList)
        {
            foreach (var tilesets in tilesetList)
            {
                DeleteInternal(tilesets, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Tileset> GetAll()
        {
            return _context.Tilesets.ToList();
        }

        public Tileset GetByID(int tilesetID)
        {
            return _context.Tilesets.Where(x => x.ResourceID == tilesetID).SingleOrDefault();
        }
        
        public IEnumerable<Tileset> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Tilesets.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
        }

        public Tileset GetByResref(string resref)
        {
            return _context.Tilesets.Where(x => x.Resref == resref).SingleOrDefault();
        }

        

        //Move logic somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from tileset
        //                                        in Context.TilesetRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = tileset.Name,
        //                                            ResourceID = tileset.ResourceID
        //                                        }).ToList();

        //    return items;
        //}

        public void DeleteAllByCategory(Category category)
        {
            List<Tileset> tilesetList = _context.Tilesets.Where(x => x.ResourceCategoryID == category.ResourceID).ToList();
            _context.Tilesets.RemoveRange(tilesetList);
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
            List<Category> categories = _context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Tileset).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Tileset> tilesets = _context.Tilesets.Where(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
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

        public bool Exists(string resref)
        {
            Tileset tileset = _context.Tilesets.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(tileset, null);
        }

        #endregion

        //public int GetDefaultResourceID()
        //{
        //    Tileset defaultObject = _context.Tilesets.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

    }
}
