using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;

namespace WinterEngine.Toolset.Data.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ItemRepository
    {
        public List<ItemDTO> GetAllItems()
        {
            List<ItemDTO> _itemList = new List<ItemDTO>();

            try
            {
                using (WinterContext context = new WinterContext())
                {
                    var items = from item
                                in context.Items
                                select item;
                    _itemList = Mapper.Map(items.ToList<Item>(), _itemList);
                }
            }
            catch (Exception ex)
            {
                _itemList.Clear();
                MessageBox.Show("Error retrieving all items.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            return _itemList;
        }
    }
}
