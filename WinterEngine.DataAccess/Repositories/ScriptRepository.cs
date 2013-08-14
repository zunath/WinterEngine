﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess.Repositories
{
    public class ScriptRepository : RepositoryBase, IResourceRepository<Script>
    {
        #region Constructors

        public ScriptRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<Script> GetAll()
        {
            return Context.ScriptRepository.Get().ToList();
        }

        public void Add(Script script)
        {
            Context.ScriptRepository.Add(script);
        }

        public void Add(List<Script> scriptList)
        {
            Context.ScriptRepository.AddList(scriptList);
        }

        public void Update(Script script)
        {
            Context.ScriptRepository.Update(script);
        }

        public void Upsert(Script script)
        {
            if (script.ResourceID <= 0)
            {
                Context.ScriptRepository.Add(script);
            }
            else
            {
                Context.ScriptRepository.Update(script);
            }
        }

        public Script GetByID(int scriptID)
        {
            return Context.ScriptRepository.Get(x => x.ResourceID == scriptID).SingleOrDefault();
        }

        public bool Exists(Script script)
        {
            Script dbScript = Context.ScriptRepository.Get(x => x.ResourceID == script.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbScript, null);
        }

        public void Delete(Script script)
        {
            Context.ScriptRepository.Delete(script);
        }

        public List<Script> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.ScriptRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Generates a hierarchy of categories containing scripts for use in tree views.
        /// </summary>
        /// <returns></returns>
        public JSTreeNode GenerateJSTreeCategories()
        {
            JSTreeNode rootNode = new JSTreeNode("Scripts");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectTypeID == (int)GameObjectTypeEnum.Script).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.VisibleName);
                List<Script> scripts = GetAllByResourceCategory(category);
                foreach (Script script in scripts)
                {
                    JSTreeNode childNode = new JSTreeNode(script.VisibleName);
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
