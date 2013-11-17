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
    public class CreatureRepository : IGameObjectRepository<Creature>
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
        /// If an creature with the same resref is in the database, it will be replaced with newCreature.
        /// If an creature does not exist by newCreature's resref, it will be added to the database.
        /// </summary>
        /// <param name="creature">The new creature to upsert.</param>
        public Creature Save(Creature creature)
        {
            if (creature.ResourceID <= 0)
            {
                _context.Creatures.Add(creature);
            }
            else
            {
                Update(creature);
            }
            return creature;
        }

        public void Save(IEnumerable<Creature> entityList)
        {
            throw new NotImplementedException();
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

            foreach (LocalVariable variable in newCreature.LocalVariables)
            {
                variable.GameObjectBaseID = newCreature.ResourceID;
            }

            _context.Entry(dbCreature).CurrentValues.SetValues(newCreature);
        }

        /// <summary>
        /// Deletes a creature from the database
        /// </summary>
        /// <param name="creature"></param>
        public void Delete(Creature creature)
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
            _context.LocalVariables.RemoveRange(creature.LocalVariables.ToList());
            _context.Creatures.Remove(creature);
        }

        public void Delete(IEnumerable<Creature> entityList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all of the creatures from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Creature> GetAll()
        {
            return _context.Creatures.ToList();
        }

        public Creature GetByID(int resourceID)
        {
            return _context.Creatures.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all of the creatures in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Creature> GetAllByResourceCategory(Category resourceCategory)
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


        #endregion

    }
}
