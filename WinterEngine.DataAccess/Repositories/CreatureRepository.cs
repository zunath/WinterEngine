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
        public void Add(Creature creature)
        {
            Context.CreatureRepository.Add(creature);
        }

        /// <summary>
        /// Adds a list of creatures to the database.
        /// </summary>
        /// <param name="creatureList">The list of creatures to add to the database.</param>
        public void Add(List<Creature> creatureList)
        {
            Context.CreatureRepository.AddList(creatureList);
        }

        /// <summary>
        /// Updates an existing creature in the database with new values.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newCreature">The new creature that will replace the creature with the matching resref.</param>
        public void Update(Creature newCreature)
        {
            Context.CreatureRepository.Update(newCreature);
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
                Context.CreatureRepository.Add(creature);
            }
            else
            {
                Context.CreatureRepository.Update(creature);
            }
        }

        /// <summary>
        /// Deletes a creature with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            Creature creature = Context.CreatureRepository.Get(c => c.Resref == resref).SingleOrDefault();
            Context.CreatureRepository.Delete(creature);
        }

        /// <summary>
        /// Returns all of the creatures from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAll()
        {
            return Context.CreatureRepository.Get().ToList();
        }

        /// <summary>
        /// Returns all of the creatures in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.CreatureRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the creature with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Creature GetByResref(string resref)
        {
            return Context.CreatureRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the creatures attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Creature> creatureList = Context.CreatureRepository.Get(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            Context.DeleteAll(creatureList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Creature creature = Context.CreatureRepository.Get(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(creature, null);
        }

        /// <summary>
        /// Generates a hierarchy of categories containing creatures for use in tree views.
        /// </summary>
        /// <returns></returns>
        public JSTreeNode GenerateJSTreeHierarchy()
        {
            JSTreeNode rootNode = new JSTreeNode("Creatures");
            List<JSTreeNode> treeNodes = new List<JSTreeNode>();
            List<Category> categories = Context.CategoryRepository.Get(x => x.GameObjectTypeID == (int)GameObjectTypeEnum.Creature).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                List<Creature> creatures = GetAllByResourceCategory(category);
                foreach (Creature creature in creatures)
                {
                    JSTreeNode childNode = new JSTreeNode(creature.Name);
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
