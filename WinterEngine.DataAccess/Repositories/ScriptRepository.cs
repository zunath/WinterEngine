using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories.Interfaces;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class Scripts : RepositoryBase, IGameObjectRepository<Script>
    {
        #region Constructors

        public Scripts(ModuleDataContext context, bool autoSaveChanges = true)
            : base(context, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a script to the database.
        /// </summary>
        /// <param name="script">The script to add to the database.</param>
        /// <returns></returns>
        public Script Add(Script script)
        {
            return _context.Scripts.Add(script);
        }

        /// <summary>
        /// Adds a list of scripts to the database.
        /// </summary>
        /// <param name="scriptList">The list of scripts to add to the database.</param>
        public void Add(List<Script> scriptList)
        {
            _context.Scripts.AddRange(scriptList);
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

            if (newScript.GraphicResourceID <= 0)
            {
                newScript.GraphicResourceID = null;
            }

            _context.Entry(dbScript).CurrentValues.SetValues(newScript);
        }

        /// <summary>
        /// If an script with the same resref is in the database, it will be replaced with newScript.
        /// If an script does not exist by newScript's resref, it will be added to the database.
        /// </summary>
        /// <param name="script">The new script to upsert.</param>
        public void Upsert(Script script)
        {
            if (script.ResourceID <= 0)
            {
                _context.Scripts.Add(script);
            }
            else
            {
                Update(script);
            }
        }

        /// <summary>
        /// Removes a script with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and Remove.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Script script = _context.Scripts.Where(c => c.ResourceID == resourceID).SingleOrDefault();
            _context.Scripts.Remove(script);
        }

        /// <summary>
        /// Returns all of the scripts from the database.
        /// </summary>
        /// <returns></returns>
        public List<Script> GetAll()
        {
            return _context.Scripts.ToList();
        }

        /// <summary>
        /// Returns all of the scripts in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Script> GetAllByResourceCategory(Category resourceCategory)
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

        public Script GetByID(int resourceID)
        {
            return _context.Scripts.Where(x => x.ResourceID == resourceID).SingleOrDefault();
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

                List<Script> scripts = GetAllByResourceCategory(category);
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

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
