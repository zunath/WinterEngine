using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
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
            List<Item> objectList = GetAllByResourceCategory(resourceCategory);

            using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
            {
                foreach (Item currentObject in objectList)
                {
                    Delete(currentObject.Resref);
                }
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
