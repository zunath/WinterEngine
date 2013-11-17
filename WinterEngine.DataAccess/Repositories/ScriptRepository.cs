using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class Scripts : IGameObjectRepository<Script>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;
        #region Constructors

        public Scripts(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Script InternalSave(Script script, bool saveChanges)
        {
            Script retScript;
            if (script.ResourceID <= 0)
            {
                retScript = _context.Scripts.Add(script);
            }
            else
            {
                retScript = _context.Scripts.SingleOrDefault(x => x.ResourceID == script.ResourceID);
                if (retScript == null) return null;
                _context.Entry(retScript).CurrentValues.SetValues(script);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retScript;
        }

        public Script Save(Script script)
        {
            return InternalSave(script, true);
        }

        public void Save(IEnumerable<Script> entityList)
        {
            if (entityList != null)
            {
                foreach (var script in entityList)
                {
                    InternalSave(script, false);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing script in the database with new values.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newScript">The new script that will replace the script with the matching resref.</param>
        public void Update(Script newScript)
        {
            Script dbScript;
            if (newScript.ResourceID <= 0)
            {
                dbScript = _context.Scripts.Where(x => x.Resref == newScript.Resref).SingleOrDefault();
            }
            else
            {
                dbScript = _context.Scripts.Where(x => x.ResourceID == newScript.ResourceID).SingleOrDefault();
            }
            if (dbScript == null) return;

            foreach (LocalVariable variable in newScript.LocalVariables)
            {
                variable.GameObjectBaseID = newScript.ResourceID;
            }

            _context.Entry(dbScript).CurrentValues.SetValues(newScript);
            _context.LocalVariables.RemoveRange(dbScript.LocalVariables.ToList());
            _context.LocalVariables.AddRange(newScript.LocalVariables.ToList());
            _context.Entry(dbScript).CurrentValues.SetValues(newScript);
        }


        private void DeleteInternal(Script script, bool saveChanges = true)
        {
            var dbScript = _context.Scripts.SingleOrDefault(x => x.ResourceID == script.ResourceID);
            if (dbScript == null) return;

            _context.LocalVariables.RemoveRange(dbScript.LocalVariables.ToList());
            _context.Scripts.Remove(dbScript);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Script script)
        {
            DeleteInternal(script);
        }

        public void Delete(int resourceID)
        {
            var script = _context.Scripts.Find(resourceID);
            DeleteInternal(script);
        }

        public void Delete(IEnumerable<Script> scriptList)
        {
            foreach (var script in scriptList)
            {
                DeleteInternal(script, false);
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// Returns all of the scripts from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Script> GetAll()
        {
            return _context.Scripts.ToList();
        }

        public Script GetByID(int resourceID)
        {
            return _context.Scripts.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        //todo: This needs to go somehwhere else.
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from script
        //                                        in _context.ScriptRepository
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = script.Name,
        //                                            ResourceID = script.ResourceID
        //                                        }).ToList();
        //    return items;
        //}

        /// <summary>
        /// Returns all of the scripts in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Script> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Scripts.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the script with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Script GetByResref(string resref)
        {
            return _context.Scripts.Where(x => x.Resref == resref).SingleOrDefault();
        }

        

        /// <summary>
        /// Removes all of the scripts attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Script> scriptList = _context.Scripts.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            _context.Scripts.RemoveRange(scriptList);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing scripts for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and scripts.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Scripts");
            rootNode.attr.Add("data-nodetype", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = _context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Script).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Script> scripts = _context.Scripts.Where(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
                foreach (Script script in scripts)
                {
                    JSTreeNode childNode = new JSTreeNode(script.Name);
                    childNode.attr.Add("data-nodetype", "object");
                    childNode.attr.Add("data-resourceid", Convert.ToString(script.ResourceID));
                    childNode.attr.Add("data-issystemresource", Convert.ToString(script.IsSystemResource));

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
            Script script = _context.Scripts.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(script, null);
        }       

        //public int GetDefaultResourceID()
        //{
        //    Script defaultObject = _context.Scripts.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

        #endregion

    }
}
