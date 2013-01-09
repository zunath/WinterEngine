using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.DataLayer.Repositories;

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

        private void buttonSaveChangesItemDetails_Click(object sender, EventArgs e)
        {
            using (ItemRepository repo = new ItemRepository())
            {
             
            }
        }

        private void buttonDiscardChangesItemDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
