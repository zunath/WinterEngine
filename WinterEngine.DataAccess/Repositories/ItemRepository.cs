using System;
using System.Linq;
using System.Collections.Generic;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Contexts;

namespace WinterEngine.DataAccess
{
    public class ItemRepository : IDisposable
    {
        /// <summary>
        /// Adds an item to the database.
        /// </summary>
        /// <param name="item">The item to add to the database.</param>
        /// <returns></returns>
        public void Add(Item item)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                context.Items.Add(item);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing item in the database with new values.
        /// If an item is not found by the specified resref, an exception will be thrown.
        /// </summary>
        /// <param name="resref">The resource reference to search for and update.</param>
        /// <param name="newItem">The new item that will replace the item with the matching resref.</param>
        public void Update(string resref, Item newItem)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Item item = context.Items.SingleOrDefault(x => x.Resref == resref);

                if (Object.ReferenceEquals(item, null))
                {
                    throw new NullReferenceException("Unable to find item by specified resref.");
                }
                else
                {
                    context.Items.Remove(item);
                    context.Items.Add(newItem);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// If an item with the same resref is in the database, it will be replaced with newItem.
        /// If an item does not exist by newItem's resref, it will be added to the database.
        /// </summary>
        /// <param name="newItem">The new item to upsert.</param>
        public void Upsert(Item newItem)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Item item = context.Items.SingleOrDefault(x => x.Resref == newItem.Resref);

                // Didn't find an existing item. Insert a new one.
                if (Object.ReferenceEquals(item, null))
                {
                    context.Items.Add(item);
                }
                else
                {
                    context.Items.Remove(item);
                    context.Items.Add(newItem);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an item with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for and delete.</param>
        /// <returns></returns>
        public void Delete(string resref)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                Item item = context.Items.First(a => a.Resref == resref);
                context.Items.Remove(item);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all of the items from the database.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAll()
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from item
                            in context.Items
                            select item;
                return query.ToList<Item>();
            }
        }

        /// <summary>
        /// Returns all of the items in a specified category from the database.
        /// </summary>
        /// <returns></returns>
        public List<Item> GetAllByResourceCategory(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from item
                            in context.Items
                            where item.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                            select item;
                return query.ToList<Item>();
            }
        }

        /// <summary>
        /// Returns the item with the specified resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <returns></returns>
        public Item GetByResref(string resref)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                return context.Items.FirstOrDefault(x => x.Resref == resref);
            }
        }

        /// <summary>
        /// Deletes all of the items attached to a specified category from the database.
        /// </summary>
        public void DeleteAllByCategory(ResourceCategory resourceCategory)
        {
            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                var query = from item
                            in context.Items
                            where item.ResourceCategoryID == resourceCategory.ResourceCategoryID
                            select item;
                List<Item> itemList = query.ToList<Item>();

                foreach (Item item in itemList)
                {
                    context.Items.Remove(item);
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
                Item item = context.Items.FirstOrDefault(a => a.Resref.Equals(resref));
                return !Object.ReferenceEquals(item, null);
            }
        }


        public void Dispose()
        {
        }

    }
}
