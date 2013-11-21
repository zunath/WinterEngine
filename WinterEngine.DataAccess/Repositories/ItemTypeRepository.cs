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
    public class ItemTypeRepository : RepositoryBase, IResourceRepository<ItemType>
    {
        #region Constructors

        public ItemTypeRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns all item types from the database.
        /// </summary>
        /// <returns></returns>
        public List<ItemType> GetAll()
        {
            return Context.ItemTypes.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.ItemTypes
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();

            return items;
        }


        /// <summary>
        /// Adds an item type to the database.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public ItemType Add(ItemType itemType)
        {
            return Context.ItemTypes.Add(itemType);
        }

        /// <summary>
        /// Adds a list of item types to the database.
        /// </summary>
        /// <param name="itemTypeList"></param>
        /// <returns></returns>
        public void Add(List<ItemType> itemTypeList)
        {
            Context.ItemTypes.AddRange(itemTypeList);
        }

        /// <summary>
        /// Updates an existing item type with new values.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public void Update(ItemType itemType)
        {
            ItemType dbItemType = Context.ItemTypes.SingleOrDefault(x => x.ResourceID == itemType.ResourceID);
            if (dbItemType == null) return;

            Context.Entry(dbItemType).CurrentValues.SetValues(itemType);
        }

        public void Upsert(ItemType itemType)
        {
            if (itemType.ResourceID <= 0)
            {
                Context.ItemTypes.Add(itemType);
            }
            else
            {
                Update(itemType);
            }
        }

        /// <summary>
        /// Returns the item types matching the itemTypeID passed in.
        /// Returns null if item type does not exist.
        /// </summary>
        /// <param name="itemTypeID"></param>
        /// <returns></returns>
        public ItemType GetByID(int itemTypeID)
        {
            return Context.ItemTypes.SingleOrDefault(x => x.ResourceID == itemTypeID);
        }

        /// <summary>
        /// Returns true if an item type exists in the database.
        /// Returns false if an item type does not exist in the database.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public bool Exists(ItemType itemType)
        {
            ItemType dbItemType = Context.ItemTypes.SingleOrDefault(x => x.ResourceID == itemType.ResourceID);
            return !Object.ReferenceEquals(dbItemType, null);
        }

        /// <summary>
        /// Deletes an item type from the database.
        /// </summary>
        /// <param name="itemType">The item type to delete.</param>
        /// <returns></returns>
        public void Delete(ItemType itemType)
        {
            Context.ItemTypes.Remove(itemType);
        }

        public int GetDefaultResourceID()
        {
            ItemType defaultObject = Context.ItemTypes.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
