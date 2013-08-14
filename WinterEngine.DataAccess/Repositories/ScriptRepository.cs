﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class ScriptRepository : RepositoryBase, IGameObjectRepository<Script>
    {
        #region Constructors

        public ScriptRepository(string connectionString = "", bool autoSaveChanges = true) 
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a script to the database.
        /// </summary>
        /// <param name="script">The script to add to the database.</param>
        /// <returns></returns>
        public void Add(Script script)
        {
            Context.ScriptRepository.Add(script);
        }

        /// <summary>
        /// Adds a list of scripts to the database.
        /// </summary>
        /// <param name="scriptList">The list of scripts to add to the database.</param>
        public void Add(List<Script> scriptList)
        {
            Context.ScriptRepository.AddList(scriptList);
        }

        /// <summary>
        /// Updates an existing script in the database with new values.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newScript">The new script that will replace the script with the matching resref.</param>
        public void Update(Script newScript)
        {
            Context.ScriptRepository.Update(newScript);
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
                Context.ScriptRepository.Add(script);
            }
            else
            {
                Context.ScriptRepository.Update(script);
            }
        }

        /// <summary>
        /// Deletes a script with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            Script script = Context.ScriptRepository.Get(c => c.Resref == resref).SingleOrDefault();
            Context.ScriptRepository.Delete(script);
        }

        /// <summary>
        /// Returns all of the scripts from the database.
        /// </summary>
        /// <returns></returns>
        public List<Script> GetAll()
        {
            return Context.ScriptRepository.Get().ToList();
        }

        /// <summary>
        /// Returns all of the scripts in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Script> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.ScriptRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the script with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Script GetByResref(string resref)
        {
            return Context.ScriptRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the scripts attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Script> scriptList = Context.ScriptRepository.Get(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.DeleteAll(scriptList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Script script = Context.ScriptRepository.Get(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(script, null);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing scripts for use in tree views.
        /// </summary>
        /// <returns></returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Scripts");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectTypeID == (int)GameObjectTypeEnum.Script).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                List<Script> scripts = GetAllByResourceCategory(category);
                foreach (Script script in scripts)
                {
                    JSTreeNode childNode = new JSTreeNode(script.Name);
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
