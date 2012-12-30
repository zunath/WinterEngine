using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoMapper;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ItemRepository : IWinterObjectRepository
    {
        /// <summary>
        /// Returns all items from the database.
        /// </summary>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjects()
        {
            List<ItemDTO> _itemList = new List<ItemDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from item
                                in context.Items
                                select item;
                    _itemList = Mapper.Map(query.ToList<Item>(), _itemList);
                }
            }
            catch (Exception ex)
            {
                _itemList.Clear();
                MessageBox.Show("Error retrieving all items.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _itemList.ConvertAll(x => (WinterObjectDTO)x);
        }

        /// <summary>
        /// Returns all items that match a particular ResourceCategoryDTO's CategoryID field.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjectsByResourceCategory(ResourceCategoryDTO resourceCategory)
        {
            List<ItemDTO> _itemList = new List<ItemDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from item
                                in context.Items
                                where item.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                select item;
                    _itemList = Mapper.Map(query.ToList<Item>(), _itemList);
                }
            }
            catch (Exception ex)
            {
                _itemList.Clear();
                MessageBox.Show("Error retrieving items by resource category.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _itemList.ConvertAll(x => (WinterObjectDTO)x);
        }

        /// <summary>
        /// Returns a specified item by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public WinterObjectDTO GetObjectByResref(string resref)
        {
            ItemDTO retItem = new ItemDTO();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var query = from item
                                in context.Items
                                where item.Resref.Equals(resref) // Must match the resref - primary key in database
                                select item;
                    List<Item> resultItems = query.ToList<Item>();
                    retItem = Mapper.Map(resultItems[0], retItem);
                }
            }
            catch (Exception ex)
            {
                retItem = new ItemDTO();
                MessageBox.Show("Error retrieving specified item (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retItem as WinterObjectDTO;
        }

        public void Dispose()
        {
        }
    }
}
