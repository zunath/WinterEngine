using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.DataLayer.DataTransferObjects;
using DejaVu;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class ItemView : UserControl
    {
        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        private ItemEditorControl _ItemEditorControl;

        public ItemView()
        {
            InitializeComponent();
            AddXNAViewerControl();
        }

        private void AddXNAViewerControl()
        {
            _ItemEditorControl = new ItemEditorControl();
            _ItemEditorControl.Dock = DockStyle.Fill;
            panelItemEditorControl.Controls.Add(_ItemEditorControl);            
        }


        /// <summary>
        /// Loads a list of item objects into the GUI of this control.
        /// The list of items should be retrieved from the repository first and then passed into this method.
        /// </summary>
        /// <param name="areaList"></param>
        private void LoadContent(List<ItemDTO> itemList)
        {
            using (UndoRedoManager.StartInvisible("Load Item Tree List"))
            {
                foreach (ItemDTO currentItem in itemList)
                {
                    TreeNode node = new TreeNode();
                    node.Text = currentItem.Name;
                    node.Tag = currentItem;

                    treeViewItems.Nodes[0].Nodes.Add(node);
                }
                UndoRedoManager.Commit();
            }
        }
    }
}
