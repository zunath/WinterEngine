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
    public class PlaceableRepository : IGameObjectRepository<Placeable>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;
        #region Constructors

        public PlaceableRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a placeable to the database.
        /// </summary>
        /// <param name="placeable">The placeable to add to the database.</param>
        /// <returns></returns>
        public Placeable Add(Placeable placeable)
        {
            return _context.Placeables.Add(placeable);
        }

        public void Add(List<Placeable> placeableList)
        {
            _context.Placeables.AddRange(placeableList);
        }

        /// <summary>
        /// If an placeable with the same resref is in the database, it will be replaced with newPlaceable.
        /// If an placeable does not exist by newPlaceable's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new placeable to upsert.</param>
        public Placeable Save(Placeable placeable)
        {
            if (placeable.ResourceID <= 0)
            {
                _context.Placeables.Add(placeable);
            }
            else
            {
                Update(placeable);
            }
            return placeable;
        }

        public void Save(IEnumerable<Placeable> entityList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing placeable in the database with new values.
        /// </summary>
        /// <param name="newItem">The new placeable that will replace the placeable with the matching resref.</param>
        public void Update(Placeable newPlaceable)
        {
            Placeable dbPlaceable;
            if (newPlaceable.ResourceID <= 0)
            {
                dbPlaceable = _context.Placeables.Where(x => x.Resref == newPlaceable.Resref).SingleOrDefault();
            }
            else
            {
                dbPlaceable = _context.Placeables.Where(x => x.ResourceID == newPlaceable.ResourceID).SingleOrDefault();
            }
            if (dbPlaceable == null) return;

            foreach (LocalVariable variable in newPlaceable.LocalVariables)
            {
                variable.GameObjectBaseID = newPlaceable.ResourceID;
            }

            _context.Entry(dbPlaceable).CurrentValues.SetValues(newPlaceable);
            _context.LocalVariables.RemoveRange(dbPlaceable.LocalVariables.ToList());
            _context.LocalVariables.AddRange(newPlaceable.LocalVariables.ToList());
            _context.Entry(dbPlaceable).CurrentValues.SetValues(newPlaceable);
        }

        

        /// <summary>
        /// Removes a placeable from the database
        /// </summary>
        /// <param name="placeable"></param>
        public void Delete(Placeable placeable)
        {
            this.Delete(placeable.ResourceID);
        }

        /// <summary>
        /// Removes a placeable with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and Remove.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Placeable placeable = _context.Placeables.Where(p => p.ResourceID == resourceID).SingleOrDefault();
            _context.LocalVariables.RemoveRange(placeable.LocalVariables.ToList());
            _context.Placeables.Remove(placeable);
        }

        public void Delete(IEnumerable<Placeable> entityList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all of the placeables from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Placeable> GetAll()
        {
            return _context.Placeables.ToList();
        }
        
        public Placeable GetByID(int resourceID)
        {
            return _context.Placeables.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all of the placeables in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Placeable> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Placeables.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the placeable with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Placeable GetByResref(string resref)
        {
            return _context.Placeables.Where(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Placeable placeable = _context.Placeables.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(placeable, null);
        }

        #endregion
    }
}
