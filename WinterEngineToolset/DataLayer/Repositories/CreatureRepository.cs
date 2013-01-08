using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    public class CreatureRepository : IDisposable
    {
        /// <summary>
        /// Adds a creature to the database.
        /// </summary>
        /// <param name="creature">The creature to add to the database.</param>
        /// <returns></returns>
        public void Add(Creature creature)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.Creatures.Add(creature);
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
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
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
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
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
        public List<Creature> GetAllByResourceCategory(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from creature
                            in context.Creatures
                            where creature.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
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
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                return context.Creatures.FirstOrDefault(x => x.Resref == resref);
            }
        }

        /// <summary>
        /// Deletes all of the creatures attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from creature
                            in context.Creatures
                            where creature.ResourceCategoryID == resourceCategory.ResourceCategoryID
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
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Creature creature = context.Creatures.FirstOrDefault(a => a.Resref.Equals(resref));
                return !Object.ReferenceEquals(creature, null);
            }
        }


        public void Dispose()
        {
        }

    }
}
