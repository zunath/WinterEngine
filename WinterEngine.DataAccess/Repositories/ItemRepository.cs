using System;
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

        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">The item to add to the database.</param>
        /// <returns></returns>
        public Item Add(Item item)
        {
            return _context.Items.Add(item);
        }

        public void Add(List<Item> itemList)
        {
            _context.Items.AddRange(itemList);
        }

        /// <summary>
        /// If an item with the same resref is in the database, it will be replaced with newItem.
        /// If an item does not exist by newItem's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new item to upsert.</param>
        public Item Save(Item item)
        {
            if (item.ResourceID <= 0)
            {
                _context.Items.Add(item);
            }
            else
            {
                Update(item);
            }
            return item;
        }

        public void Save(IEnumerable<Item> entityList)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// Deletes and item from the database.
        /// </summary>
        /// <param name="item"></param>
        public void Delete(Item item)
        {
            this.Delete(item.ResourceID);
        }

        /// <summary>
        /// Deletes an item with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(int resourceID)
        {
            Item item = _context.Items.Where(i => i.ResourceID == resourceID).SingleOrDefault();
            _context.LocalVariables.RemoveRange(item.LocalVariables.ToList());
            _context.Items.Remove(item);
        }

        public void Delete(IEnumerable<Item> entityList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all of the items from the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetAll()
        {
            return _context.Items.ToList();
        }
        
        public Item GetByID(int resourceID)
        {
            return _context.Items.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
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
