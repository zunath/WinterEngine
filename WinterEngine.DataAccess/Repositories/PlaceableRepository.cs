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

        private Placeable InternalSave(Placeable placeable, bool saveChanges)
        {
            Placeable retPlaceable;
            if (placeable.ResourceID <= 0)
            {
                retPlaceable = _context.Placeables.Add(placeable);
            }
            else
            {
                retPlaceable = _context.Placeables.SingleOrDefault(x => x.ResourceID == placeable.ResourceID);
                if (retPlaceable == null) return null;
                _context.Entry(retPlaceable).CurrentValues.SetValues(placeable);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retPlaceable;
        }

        public Placeable Save(Placeable placeable)
        {
            return InternalSave(placeable, true);
        }

        public void Save(IEnumerable<Placeable> entityList)
        {
            if (entityList != null)
            {
                foreach (var placeable in entityList)
                {
                    InternalSave(placeable, false);
                }
                _context.SaveChanges();
            }
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

        private void DeleteInternal(Placeable placeable, bool saveChanges = true)
        {
            var dbPlaceable = _context.Placeables.SingleOrDefault(x => x.ResourceID == placeable.ResourceID);
            if (dbPlaceable == null) return;

            _context.LocalVariables.RemoveRange(placeable.LocalVariables.ToList());
            _context.Placeables.Remove(dbPlaceable);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Placeable placeable)
        {
            DeleteInternal(placeable);
        }

        public void Delete(int resourceID)
        {
            var placeable = _context.Placeables.Find(resourceID);
            DeleteInternal(placeable);
        }

        public void Delete(IEnumerable<Placeable> placeableList)
        {
            foreach (var placeable in placeableList)
            {
                DeleteInternal(placeable, false);
            }
            _context.SaveChanges();
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
