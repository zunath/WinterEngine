using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;

using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess
{
    public class PlaceableRepository : RepositoryBase, IGameObjectRepository<Placeable>
    {
        #region Constructors

        public PlaceableRepository(string connectionString = "")
            : base(connectionString)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a placeable to the database.
        /// </summary>
        /// <param name="placeable">The placeable to add to the database.</param>
        /// <returns></returns>
        public void Add(Placeable placeable)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.Placeables.Add(placeable);
                context.SaveChanges();
            }
        }

        public void Add(List<Placeable> placeableList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (Placeable placeable in placeableList)
                {
                    context.Placeables.Add(placeable);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing placeable in the database with new values.
        /// If a placeable is not found by the specified resref, an exception will be thrown.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newItem">The new placeable that will replace the placeable with the matching resref.</param>
        public void Update(string resref, Placeable newPlaceable)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Placeable placeable = context.Placeables.SingleOrDefault(x => x.Resref == resref);

                if (Object.ReferenceEquals(placeable, null))
                {
                    throw new NullReferenceException("Unable to find placeable by specified resref.");
                }
                else
                {
                    context.Placeables.Remove(placeable);
                    context.Placeables.Add(newPlaceable);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// If an placeable with the same resref is in the database, it will be replaced with newPlaceable.
        /// If an placeable does not exist by newPlaceable's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new placeable to upsert.</param>
        public void Upsert(Placeable newPlaceable)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Placeable placeable = context.Placeables.SingleOrDefault(x => x.Resref == newPlaceable.Resref);

                // Didn't find an existing creature. Insert a new one.
                if (Object.ReferenceEquals(placeable, null))
                {
                    context.Placeables.Add(placeable);
                }
                else
                {
                    context.Placeables.Remove(placeable);
                    context.Placeables.Add(newPlaceable);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a placeable with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                Placeable placeable = context.Placeables.First(a => a.Resref == resref);
                context.Placeables.Remove(placeable);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all of the placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAll()
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from placeable
                            in context.Placeables
                            select placeable;
                return query.ToList<Placeable>();
            }
        }

        /// <summary>
        /// Returns all of the placeables in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAllByResourceCategory(Category resourceCategory)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from placeable
                            in context.Placeables
                            where placeable.ResourceCategoryID.Equals(resourceCategory.ResourceID)
                            select placeable;
                return query.ToList<Placeable>();
            }
        }

        /// <summary>
        /// Returns the placeable with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Placeable GetByResref(string resref)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.Placeables.FirstOrDefault(x => x.Resref == resref);
            }
        }

        /// <summary>
        /// Deletes all of the placeables attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from item
                            in context.Items
                            where item.ResourceCategoryID == resourceCategory.ResourceID
                            select item;
                List<Item> itemList = query.ToList<Item>();

                foreach (Item item in itemList)
                {
                    context.Items.Remove(item);
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
                Placeable placeable = context.Placeables.FirstOrDefault(a => a.Resref.Equals(resref));
                return !Object.ReferenceEquals(placeable, null);
            }
        }


        public void Dispose()
        {
        }

        #endregion
    }
}
