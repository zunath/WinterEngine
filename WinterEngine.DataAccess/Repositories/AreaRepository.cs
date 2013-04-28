﻿using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess
{
    public class AreaRepository : RepositoryBase, IGameObjectRepository<Area>
    {
        #region Constructors

        public AreaRepository(string connectionString = "", bool autoSaveChanges = true) : base()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an area to the database.
        /// </summary>
        /// <param name="area">The area to add to the database.</param>
        /// <returns></returns>
        public void Add(Area area)
        {
            Context.AreaRepository.Add(area);
        }

        /// <summary>
        /// Adds a list of areas to the database.
        /// </summary>
        /// <param name="areaList">The list of areas to add to the database.</param>
        public void Add(List<Area> areaList)
        {
            foreach (Area area in areaList)
            {
                Context.AreaRepository.Add(area);
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
            Context.Update(newArea);
        }

        /// <summary>
        /// If an area with the same resref is in the database, it will be replaced with newArea.
        /// If an area does not exist by newArea's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new area to upsert.</param>
        public void Upsert(Area area)
        {
            if(area.GameObjectID <= 0)
            {
                Context.AreaRepository.Add(area);
            }
            else
            {
                Context.Update(area);
            }
        }

        /// <summary>
        /// Deletes an area with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            Area area = Context.AreaRepository.Get(a => a.Resref == resref).SingleOrDefault();
            Context.AreaRepository.Delete(area);
        }

        /// <summary>
        /// Returns all of the areas from the database.
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAll()
        {
            return Context.AreaRepository.Get().ToList();
        }

        /// <summary>
        /// Returns all of the areas in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAllByResourceCategory(Category resourceCategory)
        {
            return Context.AreaRepository.Get(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the area with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Area GetByResref(string resref)
        {
            return Context.AreaRepository.Get(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Deletes all of the areas attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(Category resourceCategory)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from area
                            in context.Areas
                            where area.ResourceCategoryID == resourceCategory.ResourceID
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
            Area area = Context.AreaRepository.Get(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(area, null);
        }

        public void Dispose()
        {
            if (AutoSaveChanges)
            {
                Context.Save();
            }
        }

        #endregion
    }
}
