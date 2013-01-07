using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    public class PlaceableRepository : IDisposable
    {
        /// <summary>
        /// Adds a placeable to the database.
        /// </summary>
        /// <param name="placeable">The placeable to add to the database.</param>
        /// <returns></returns>
        public void Add(Placeable placeable)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.Placeables.Add(placeable);
            }
        }

        /// <summary>
        /// Deletes a placeable with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Placeable placeable = context.Placeables.First(a => a.Resref == resref);
                context.Placeables.Remove(placeable);
            }
        }

        /// <summary>
        /// Returns all of the placeables from the database.
        /// </summary>
        /// <returns></returns>
        public List<Placeable> GetAll()
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
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
        public List<Placeable> GetAllByResourceCategory(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from placeable
                            in context.Placeables
                            where placeable.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
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
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                return context.Placeables.FirstOrDefault(x => x.Resref == resref);
            }
        }

        /// <summary>
        /// Deletes all of the placeables attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(ResourceCategory resourceCategory)
        {
            List<Placeable> objectList = GetAllByResourceCategory(resourceCategory);

            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                foreach (Placeable currentObject in objectList)
                {
                    Delete(currentObject.Resref);
                }
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
                Placeable placeable = context.Placeables.FirstOrDefault(a => a.Resref.Equals(resref));
                return !Object.ReferenceEquals(placeable, null);
            }
        }


        public void Dispose()
        {
        }

    }
}
