using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess
{
    public class ItemTypeRepository : RepositoryBase, IResourceRepository<ItemType>
    {
        #region Constructors

        public ItemTypeRepository(string connectionString = "")
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }
            ConnectionString = connectionString;
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns all item types from the database.
        /// </summary>
        /// <returns></returns>
        public List<ItemType> GetAll()
        {
            List<ItemType> _itemTypeList = new List<ItemType>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from itemType
                            in context.ItemTypes
                            select itemType;
                _itemTypeList = query.ToList();
            }

            return _itemTypeList;
        }


        /// <summary>
        /// Adds an item type to the database.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public void Add(ItemType itemType)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.ItemTypes.Add(itemType);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a list of item types to the database.
        /// </summary>
        /// <param name="itemTypeList"></param>
        /// <returns></returns>
        public void Add(List<ItemType> itemTypeList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (ItemType itemType in itemTypeList)
                {
                    context.ItemTypes.Add(itemType);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing item type with new values.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public void Update(ItemType itemType)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the resource in the database that matches the passed-in item type's ID (primary key)
                ItemType dbResource = context.ItemTypes.SingleOrDefault(r => r.ResourceID.Equals(itemType.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.ItemTypes.Remove(dbResource);
                    context.ItemTypes.Add(itemType);
                    context.SaveChanges();
                }
            }
        }

        public void Upsert(ItemType itemType)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ItemType dbResource = context.ItemTypes.SingleOrDefault(r => r.ResourceID.Equals(itemType.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.ItemTypes.Remove(dbResource);
                    context.ItemTypes.Add(itemType);
                    context.SaveChanges();
                }
                else
                {
                    context.ItemTypes.Add(itemType);
                    context.SaveChanges();
                }
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
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.ItemTypes.FirstOrDefault(r => r.ResourceID == itemTypeID);
            }
        }

        /// <summary>
        /// Returns true if an item type exists in the database.
        /// Returns false if an item type does not exist in the database.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public bool Exists(ItemType itemType)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ItemType dbItemType = context.ItemTypes.FirstOrDefault(r => r.ResourceID.Equals(itemType.ResourceID));
                return !Object.ReferenceEquals(dbItemType, null);
            }
        }

        /// <summary>
        /// Deletes an item type from the database.
        /// </summary>
        /// <param name="itemType">The item type to delete.</param>
        /// <returns></returns>
        public void Delete(ItemType itemType)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the item type in the database. CategoryID is a primary key so there will only ever be one.
                ItemType dbItemType = context.ItemTypes.SingleOrDefault(val => val.ResourceID == itemType.ResourceID);

                if (!Object.ReferenceEquals(dbItemType, null))
                {
                    context.ItemTypes.Remove(dbItemType);
                    context.SaveChanges();
                }
            }
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
