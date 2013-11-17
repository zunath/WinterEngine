﻿using System;
using System.Linq;
using System.Collections.Generic;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess
{
    public class ItemRepository : IGameObjectRepository<Item>
    {
        #region Constructors

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        public ItemRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private Item InternalSave(Item item, bool saveChanges)
        {
            Item retItem;
            if (item.ResourceID <= 0)
            {
                retItem = _context.Items.Add(item);
            }
            else
            {
                retItem = _context.Items.SingleOrDefault(x => x.ResourceID == item.ResourceID);
                if (retItem == null) return null;
                _context.Entry(retItem).CurrentValues.SetValues(item);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retItem;
        }

        public Item Save(Item item)
        {
            return InternalSave(item, true);
        }

        public void Save(IEnumerable<Item> entityList)
        {
            if (entityList != null)
            {
                foreach (var item in entityList)
                {
                    InternalSave(item, false);
                }
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing item in the database with new values.
        /// </summary>
        /// <param name="newItem">The new item that will replace the item with the matching resref.</param>
        public void Update(Item newItem)
        {
            Item dbItem;
            if (newItem.ResourceID <= 0)
            {
                dbItem = _context.Items.Where(x => x.Resref == newItem.Resref).SingleOrDefault();
            }
            else
            {
                dbItem = _context.Items.Where(x => x.ResourceID == newItem.ResourceID).SingleOrDefault();
            }
            if (dbItem == null) return;

            foreach (LocalVariable variable in newItem.LocalVariables)
            {
                variable.GameObjectBaseID = newItem.ResourceID;
            }

            _context.Entry(dbItem).CurrentValues.SetValues(newItem);
            _context.LocalVariables.RemoveRange(dbItem.LocalVariables.ToList());
            _context.LocalVariables.AddRange(newItem.LocalVariables.ToList());
            
        }

        private void DeleteInternal(Item item, bool saveChanges = true)
        {
            var dbItem = _context.Items.SingleOrDefault(x => x.ResourceID == item.ResourceID);
            if (dbItem == null) return;

            _context.LocalVariables.RemoveRange(item.LocalVariables.ToList());

            _context.Items.Remove(dbItem);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(Item item)
        {
            DeleteInternal(item);
        }

        public void Delete(int resourceID)
        {
            var item = _context.Items.Find(resourceID);
            DeleteInternal(item);
        }

        public void Delete(IEnumerable<Item> itemList)
        {
            foreach (var item in itemList)
            {
                DeleteInternal(item, false);
            }
            _context.SaveChanges();
        }
        public IEnumerable<Item> GetAll()
        {
            return _context.Items.AsEnumerable();
        }

        public Item GetByID(int resourceID)
        {
            return _context.Items.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }


        /// <summary>
        /// Returns all of the items in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetAllByResourceCategory(Category resourceCategory)
        {
            return _context.Items.Where(x => x.ResourceCategoryID.Equals(resourceCategory.ResourceID)).ToList();
        }

        /// <summary>
        /// Returns the item with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Item GetByResref(string resref)
        {
            return _context.Items.Where(x => x.Resref == resref).SingleOrDefault();
        }

        /// <summary>
        /// Returns True if an object with the specified resref exists in the database.
        /// Returns False if no object with the specified resref is found in the database.
        /// </summary>
        /// <param name="resref">The resource reference to look for.</param>
        /// <returns></returns>
        public bool Exists(string resref)
        {
            Item item = _context.Items.Where(x => x.Resref == resref).SingleOrDefault();
            return !Object.ReferenceEquals(item, null);
        }

        #endregion

    }
}
