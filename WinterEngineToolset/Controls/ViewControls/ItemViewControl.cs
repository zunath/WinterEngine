﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class ItemViewControl : UserControl
    {
        private ObjectViewer3D _itemModel;
        private ObjectViewer2D _itemIcon;

        public ItemViewControl()
        {
            InitializeComponent();

            // Designer in VS2010 has issues with custom controls.
            // Manually add the 3D object viewer when the program runs.
            _itemModel = new ObjectViewer3D();
            _itemModel.Dock = DockStyle.Fill;
            panelItemModelViewer.Controls.Add(_itemModel);

            _itemIcon = new ObjectViewer2D();
            _itemIcon.Dock = DockStyle.Fill;
            panelItemIconViewer.Controls.Add(_itemIcon);
        }

        /// <summary>
        /// Handles updating an item's entry in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveChangesItemDetails_Click(object sender, EventArgs e)
        {
            using (ItemRepository repo = new ItemRepository())
            {
                Item item = new Item();
                item.Comment = textBoxComments.Text;
                item.Description = textBoxDescription.Text;
                item.Name = textBoxItemName.Text;
                item.Tag = textBoxItemTag.Text;
                item.Resref = textBoxItemResref.Text;
                item.Price = (int)numericUpDownPrice.Value;
                item.Weight = (int)numericUpDownWeight.Value;

                repo.Update(textBoxItemResref.Text, item); 
            }
        }

        private void buttonDiscardChangesItemDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
