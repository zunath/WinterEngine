using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    public class AreaRepository : IDisposable
    {
        /// <summary>
        /// Adds an area to the database.
        /// </summary>
        /// <param name="area">The area to add to the database.</param>
        /// <returns></returns>
        public void Add(Area area)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.Areas.Add(area);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing area in the database with new values.
        /// If an area is not found by the specified resref, an exception will be thrown.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newItem">The new area that will replace the area with the matching resref.</param>
        public void Update(string resref, Area newArea)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Area area = context.Areas.SingleOrDefault(x => x.Resref == resref);

                if (Object.ReferenceEquals(area, null))
                {
                    throw new NullReferenceException("Unable to find area by specified resref.");
                }
                else
                {
                    area = newArea;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// If an area with the same resref is in the database, it will be replaced with newArea.
        /// If an area does not exist by newArea's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new area to upsert.</param>
        public void Upsert(Area newArea)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Area area = context.Areas.SingleOrDefault(x => x.Resref == newArea.Resref);

                // Didn't find an existing area. Insert a new one.
                if (Object.ReferenceEquals(area, null))
                {
                    context.Areas.Add(area);
                }
                else
                {
                    area = newArea;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an area with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Area area = context.Areas.First(a => a.Resref == resref);
                context.Areas.Remove(area);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all of the areas from the database.
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAll()
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from area
                            in context.Areas
                            select area;
                return query.ToList<Area>();
            }
        }

        /// <summary>
        /// Returns all of the areas in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAllByResourceCategory(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from area
                            in context.Areas
                            where area.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                            select area;
                return query.ToList<Area>();
            }
        }

        /// <summary>
        /// Returns the area with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Area GetByResref(string resref)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                return context.Areas.FirstOrDefault(x => x.Resref == resref);                
            }
        }

        /// <summary>
        /// Deletes all of the areas attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from area
                            in context.Areas
                            where area.ResourceCategoryID == resourceCategory.ResourceCategoryID
                            select area;
                List<Area> areaList = query.ToList<Area>();

                foreach (Area area in areaList)
                {
                    context.Areas.Remove(area);
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
                Area area = context.Areas.FirstOrDefault(a => a.Resref.Equals(resref));
                return !Object.ReferenceEquals(area, null);
            }
        }


        public void Dispose()
        {
        }
    }
}
