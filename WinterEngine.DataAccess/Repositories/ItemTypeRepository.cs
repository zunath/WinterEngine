using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;


namespace WinterEngine.DataAccess
{
    public class ItemTypeRepository : IGenericRepository<ItemType>
    {
        #region Constructors

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        public ItemTypeRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private ItemType InternalSave(ItemType itemType, bool saveChanges)
        {
            ItemType retItemType;
            if (itemType.ResourceID <= 0)
            {
                retItemType = _context.ItemTypes.Add(itemType);
            }
            else
            {
                retItemType = _context.ItemTypes.SingleOrDefault(x => x.ResourceID == itemType.ResourceID);
                if (retItemType == null) return null;
                _context.Entry(retItemType).CurrentValues.SetValues(itemType);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retItemType;
        }

        
        public ItemType Save(ItemType itemType)
        {
            return InternalSave(itemType, true);
        }

        public void Save(IEnumerable<ItemType> entityList)
        {
            if (entityList != null)
            {
                foreach (var itemType in entityList)
                {
                    InternalSave(itemType, false);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing item type with new values.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public void Update(ItemType itemType)
        {
            var dbItemType = _context.ItemTypes.Where(x => x.ResourceID == itemType.ResourceID).SingleOrDefault();
            if (dbItemType == null) return;

            _context.Entry(dbItemType).CurrentValues.SetValues(itemType);
        }

        private void DeleteInternal(ItemType itemType, bool saveChanges = true)
        {
            var dbItemType = _context.Abilities.SingleOrDefault(x => x.ResourceID == itemType.ResourceID);
            if (dbItemType == null) return;

            _context.Abilities.Remove(dbItemType);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(ItemType itemType)
        {
            DeleteInternal(itemType);
        }

        public void Delete(int resourceID)
        {
            var itemType = _context.ItemTypes.Find(resourceID);
            DeleteInternal(itemType);
        }

        public void Delete(IEnumerable<ItemType> itemTypeList)
        {
            foreach (var itemType in itemTypeList)
            {
                DeleteInternal(itemType, false);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns all item types from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ItemType> GetAll()
        {
            return _context.ItemTypes.ToList();
        }

        /// <summary>
        /// Returns the item types matching the itemTypeID passed in.
        /// Returns null if item type does not exist.
        /// </summary>
        /// <param name="itemTypeID"></param>
        /// <returns></returns>
        public ItemType GetByID(int itemTypeID)
        {
            return _context.ItemTypes.Where(x => x.ResourceID == itemTypeID).SingleOrDefault();
        }


        ///// <summary>
        ///// Returns true if an item type exists in the database.
        ///// Returns false if an item type does not exist in the database.
        ///// </summary>
        ///// <param name="itemType"></param>
        ///// <returns></returns>
        //public bool Exists(ItemType itemType)
        //{
        //    ItemType dbItemType = _context.ItemTypes.Where(x => x.ResourceID == itemType.ResourceID).SingleOrDefault();
        //    return !Object.ReferenceEquals(dbItemType, null);
        //}

        #endregion
    }
}
