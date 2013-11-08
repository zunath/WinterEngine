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
    public class CreatureRepository : IGameObjectRepository<Creature>, IRepository
    {
        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public CreatureRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
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
            return _context.Creatures.Add(creature);
        }

        /// <summary>
        /// Adds a list of creatures to the database.
        /// </summary>
        /// <param name="creatureList">The list of creatures to add to the database.</param>
        public void Add(List<Creature> creatureList)
        {
            _context.Creatures.AddRange(creatureList);
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
                dbCreature = _context.Creatures.Where(x => x.Resref == newCreature.Resref).SingleOrDefault();
            }
            else
            {
                dbCreature = _context.Creatures.Where(x => x.ResourceID == newCreature.ResourceID).SingleOrDefault();
            }
            if (dbCreature == null) return;

            if (newCreature.GraphicResourceID <= 0)
            {
                newCreature.GraphicResourceID = null;
            }

            _context.Entry(dbCreature).CurrentValues.SetValues(newCreature);
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
                _context.Creatures.Add(creature);
            }
            else
            {
                Update(creature);
            }
        }

        /// <summary>
        /// Deletes a creature from the database
        /// </summary>
        /// <param name="creature"></param>
        void IGenericRepository<Creature>.Delete(Creature creature)
        {
            this.Delete(creature.ResourceID);
        }

        /// <summary>
        /// Deletes a creature with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Creature creature = _context.Creatures.Where(c => c.ResourceID == resourceID).SingleOrDefault();
            _context.Creatures.Remove(creature);
        }

        /// <summary>
        /// Returns all of the creatures from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAll()
        {
            return _context.Creatures.ToList();
        }

        /// <summary>
        /// Returns all of the creatures in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Creatures.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the creature with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Creature GetByResref(string resref)
        {
            return _context.Creatures.Where(x => x.Resref == resref).SingleOrDefault();
        }

        public Creature GetByID(int resourceID)
        {
            return _context.Creatures.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the creatures attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            List<Creature> creatureList = _context.Creatures.Where(x => x.ResourceCategoryID == resourceCategory.ResourceID).ToList();
            _context.Creatures.RemoveRange(creatureList);
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Creature creature = _context.Creatures.Where(x => x.Resref == resref).SingleOrDefault();
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
            List<Category> categories = _context.ResourceCategories.Where(x => x.GameObjectType == GameObjectTypeEnum.Creature).ToList();
            foreach (Category category in categories)
            {
                JSTreeNode categoryNode = new JSTreeNode(category.Name);
                categoryNode.attr.Add("data-nodetype", "category");
                categoryNode.attr.Add("data-categoryid", Convert.ToString(category.ResourceID));
                categoryNode.attr.Add("data-issystemresource", Convert.ToString(category.IsSystemResource));

                List<Creature> creatures = GetAllByResourceCategory(category);
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

        #endregion

        public object Load(int resourceID)
        {
            throw new NotImplementedException();
        }

        public int Save(object gameObject)
        {
            throw new NotImplementedException();
        }

        public void DeleteByCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
