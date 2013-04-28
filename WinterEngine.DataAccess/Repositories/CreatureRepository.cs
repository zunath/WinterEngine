using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;

using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess
{
    public class CreatureRepository : RepositoryBase, IGameObjectRepository<Creature>
    {
        #region Constructors
        
        public CreatureRepository(string connectionString = "") 
            : base(connectionString)
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
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.Creatures.Add(creature);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a list of creatures to the database.
        /// </summary>
        /// <param name="creatureList">The list of creatures to add to the database.</param>
        public void Add(List<Creature> creatureList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (Creature creature in creatureList)
                {
                    context.Creatures.Add(creature);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing creature in the database with new values.
        /// If a creature is not found by the specified resref, an exception will be thrown.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newItem">The new creature that will replace the creature with the matching resref.</param>
        public void Update(string resref, Creature newCreature)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Creature creature = context.Creatures.SingleOrDefault(x => x.Resref == resref);
                
                if (Object.ReferenceEquals(creature, null))
                {
                    throw new NullReferenceException("Unable to find creature by specified resref.");
                }
                else
                {
                    context.Creatures.Remove(creature);
                    context.Creatures.Add(newCreature);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// If an creature with the same resref is in the database, it will be replaced with newCreature.
        /// If an creature does not exist by newCreature's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new creature to upsert.</param>
        public void Upsert(Creature newCreature)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Creature creature = context.Creatures.SingleOrDefault(x => x.Resref == newCreature.Resref);

                // Didn't find an existing creature. Insert a new one.
                if (Object.ReferenceEquals(creature, null))
                {
                    context.Creatures.Add(newCreature);
                }
                else
                {
                    context.Creatures.Remove(creature);
                    context.Creatures.Add(newCreature);                
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an creature with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Creature creature = context.Creatures.First(a => a.Resref == resref);
                context.Creatures.Remove(creature);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all of the creatures from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAll()
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from creature
                            in context.Creatures
                            select creature;
                return query.ToList<Creature>();
            }
        }

        /// <summary>
        /// Returns all of the creatures in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetAllByResourceCategory(Category resourceCategory)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from creature
                            in context.Creatures
                            where creature.ResourceCategoryID.Equals(resourceCategory.ResourceID)
                            select creature;
                return query.ToList<Creature>();
            }
        }

        /// <summary>
        /// Returns the creature with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Creature GetByResref(string resref)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.Creatures.FirstOrDefault(x => x.Resref == resref);
            }
        }

        /// <summary>
        /// Deletes all of the creatures attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from creature
                            in context.Creatures
                            where creature.ResourceCategoryID == resourceCategory.ResourceID
                            select creature;
                List<Creature> creatureList = query.ToList<Creature>();

                foreach (Creature creature in creatureList)
                {
                    context.Creatures.Remove(creature);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Creature creature = context.Creatures.FirstOrDefault(a => a.Resref.Equals(resref));
                return !Object.ReferenceEquals(creature, null);
            }
        }


        public void Dispose()
        {
        }

        #endregion
    }
}
