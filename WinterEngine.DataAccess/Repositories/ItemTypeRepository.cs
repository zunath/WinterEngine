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
    public class ItemTypeRepository : IResourceRepository<ItemType>
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

        

        //move logic somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from item
        //                                        in Context.ItemTypeRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = item.Name,
        //                                            ResourceID = item.ResourceID
        //                                        }).ToList();

        //    return items;
        //}


        /// <summary>
        /// Adds an item type to the database.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public ItemType Add(ItemType itemType)
        {
            return _context.ItemTypes.Add(itemType);
        }

        /// <summary>
        /// Adds a list of item types to the database.
        /// </summary>
        /// <param name="itemTypeList"></param>
        /// <returns></returns>
        public void Add(List<ItemType> itemTypeList)
        {
            _context.ItemTypes.AddRange(itemTypeList);
        }

        public void Save(ItemType itemType)
        {
            if (itemType.ResourceID <= 0)
            {
                _context.ItemTypes.Add(itemType);
            }
            else
            {
                Update(itemType);
            }
        }

        /// <summary>
        /// Updates an existing item type with new values.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public void Update(ItemType itemType)
        {
            ItemType dbItemType = _context.ItemTypes.Where(x => x.ResourceID == itemType.ResourceID).SingleOrDefault();
            if (dbItemType == null) return;

            _context.Entry(dbItemType).CurrentValues.SetValues(itemType);
        }

        /// <summary>
        /// Deletes an item type from the database.
        /// </summary>
        /// <param name="itemType">The item type to delete.</param>
        /// <returns></returns>
        public void Delete(ItemType itemType)
        {
            _context.ItemTypes.Remove(itemType);
        }

        public void Delete(int resourceID)
        {
            var item = _context.ItemTypes.Find(resourceID);
            Delete(item);
        }

        /// <summary>
        /// Returns all item types from the database.
        /// </summary>
        /// <returns></returns>
        public List<ItemType> GetAll()
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

        public void ApplyChanges()
        {
            throw new NotImplementedException();
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
