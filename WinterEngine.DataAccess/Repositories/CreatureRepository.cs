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
    public class CreatureRepository : RepositoryBase, IGameObjectRepository<Creature>
    {
        #region Constructors
        
        public CreatureRepository(string connectionString = "", bool autoSaveChanges = true) 
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a creature to the database.
        /// </summary>
        /// <param name="creature">The creature to add to the database.</param>
        /// <returns></returns>
        public Creature Add(Creature creature)
        {
            return Context.Creatures.Add(creature);
        }

        /// <summary>
        /// Adds a list of creatures to the database.
        /// </summary>
        /// <param name="creatureList">The list of creatures to add to the database.</param>
        public void Add(List<Creature> creatureList)
        {
            Context.Creatures.AddRange(creatureList);
        }

        /// <summary>
        /// Updates an existing creature in the database with new values.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newCreature">The new creature that will replace the creature with the matching resref.</param>
        public void Update(Creature newCreature)
        {
            Creature dbCreature;
            if (newCreature.ResourceID <= 0)
            {
                dbCreature = Context.Creatures.SingleOrDefault(x => x.Resref == newCreature.Resref);
            }
            else
            {
                dbCreature = Context.Creatures.SingleOrDefault(x => x.ResourceID == newCreature.ResourceID);
            }
            if (dbCreature == null) return;

            foreach (LocalVariable variable in newCreature.LocalVariables)
            {
                variable.GameObjectBaseID = newCreature.ResourceID;
            }

            Context.Entry(dbCreature).CurrentValues.SetValues(newCreature);
            Context.LocalVariables.RemoveRange(dbCreature.LocalVariables.ToList());
            Context.LocalVariables.AddRange(newCreature.LocalVariables.ToList());
        }

        /// <summary>
        /// If an creature with the same resref is in the database, it will be replaced with newCreature.
        /// If an creature does not exist by newCreature's resref, it will be added to the database.
        /// </summary>
        /// <param name="creature">The new creature to upsert.</param>
        public void Upsert(Creature creature)
        {
            if (creature.ResourceID <= 0)
            {
                Context.Creatures.Add(creature);
            }
            else
            {
                Update(creature);
            }
        }

        /// <summary>
        /// Deletes a creature with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Creature creature = Context.Creatures.SingleOrDefault(c => c.ResourceID == resourceID);
            Context.LocalVariables.RemoveRange(creature.LocalVariables.ToList());
            Context.Creatures.Remove(creature);
        }

        /// <summary>
        /// Returns all of the creatures from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAll()
        {
            return Context.Creatures.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from creature
                                                in Context.Creatures
                                                select new DropDownListUIObject
                                                {
                                                    Name = creature.Name,
                                                    ResourceID = creature.ResourceID
                                                }).ToList();
            return items;
        }

        /// <summary>
        /// Returns all of the creatures in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.Creatures.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the creature with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Creature GetByResref(string resref)
        {
            return Context.Creatures.SingleOrDefault(x => x.Resref == resref);
        }

        public Creature GetByID(int resourceID)
        {
            return Context.Creatures.SingleOrDefault(x => x.ResourceID == resourceID);
        }

        /// <summary>
        /// Deletes all of the creatures attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Creature> creatureList = Context.Creatures.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.Creatures.RemoveRange(creatureList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Creature creature = Context.Creatures.SingleOrDefault(x => x.Resref == resref);
            return !Object.ReferenceEquals(creature, null);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing creatures for use in tree views.
        /// </summary>
        /// <returns>The root node containing all other categories and creatures.</returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Creatures");
            rootNode.attr.Add("data-nodetype", "root");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Creature).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Creature> creatures = Context.Creatures.Where(x => x.ResourceCategoryID.Equals(category.ResourceID) && x.IsInTreeView).ToList();
                foreach (Creature creature in creatures)
                {
                    JSTreeNode childNode = new JSTreeNode(creature.Name);
                    childNode.attr.Add("data-nodetype", "object");
                    childNode.attr.Add("data-resourceid", Convert.ToString(creature.ResourceID));
                    childNode.attr.Add("data-issystemresource", Convert.ToString(creature.IsSystemResource));

                    categoryNode.children.Add(childNode);
                }

                treeNodes.Add(categoryNode);
            }

            rootNode.children = treeNodes;
            return rootNode;
        }

        public int GetDefaultResourceID()
        {
            Creature defaultObject = Context.Creatures.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
