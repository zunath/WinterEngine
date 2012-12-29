﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using System.Windows.Forms;
using DejaVu;

namespace WinterEngine.Toolset.Data.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class ItemRepository : IDisposable
    {
        /// <summary>
        /// Returns all items from the database.
        /// </summary>
        /// <returns></returns>
        public List<ItemDTO> GetAllItems()
        {
            UndoRedoManager.StartInvisible("Data Access");
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
                UndoRedoManager.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving all items.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
            }

            return _itemList;
        }

        /// <summary>
        /// Returns a specified item by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public ItemDTO GetItemByResref(string resref)
        {
            UndoRedoManager.StartInvisible("Data Access");
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
                UndoRedoManager.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving specified item (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
            }

            return retItem;
        }

        public void Dispose()
        {
        }
    }
}
