using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects;
using WinterEngine.Toolset.DataLayer.Repositories;
using DejaVu;
using AutoMapper;
using WinterEngine.Toolset.Enumerations;
using WinterEngine.Toolset.Controls.ControlHelpers;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class AreaView : UserControl
    {
        #region Fields
        private AreaEditorControl _areaEditorControl;
        #endregion

        #region Constructors and Initialization

        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        public AreaView()
        {
            InitializeComponent();
            AddXNAViewerControl();
            buttonAddCategory.RefreshTreeView += new EventHandler(buttonAddCategory_RefreshTreeView);
        }

        private void AddXNAViewerControl()
        {
            _areaEditorControl = new AreaEditorControl();
            _areaEditorControl.Dock = DockStyle.Fill;
            panelAreaEditorControl.Controls.Add(_areaEditorControl);
        }

        #endregion

        #region GUI Population Methods

        /// <summary>
        /// Unloads and then reloads the categories and areas listed in the tree view.
        /// </summary>
        private void RefreshTreeViewGUI()
        {
            TreeViewPopulator populator = new TreeViewPopulator();
            populator.RepopulateTreeView(ref treeViewAreas, ResourceTypeEnum.Area);
        }

        #endregion

        /// <summary>
        /// Method called from child component buttonAddCategory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonAddCategory_RefreshTreeView(object sender, EventArgs e)
        {
            RefreshTreeViewGUI();
        }

        private void treeViewAreas_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Highlight the selected node on all clicks.
            treeViewAreas.SelectedNode = e.Node;
        }

        /// <summary>
        /// Handles populating the options in the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuAreaNode_Opening(object sender, CancelEventArgs e)
        {
            // No context menu for the root node at this time.
            if (treeViewAreas.SelectedNode == treeViewAreas.TopNode)
            {
                e.Cancel = true;
            }
            // Category node was selected. We know this because all categories fall under
            // the root node.
            else if (treeViewAreas.SelectedNode.Parent == treeViewAreas.TopNode)
            {
                // Add options for category context menu
                contextMenuTreeViewAreas.Items.Clear();
                contextMenuTreeViewAreas.Items.Add("Create Area");
                contextMenuTreeViewAreas.Items.Add("-");
                contextMenuTreeViewAreas.Items.Add("Delete Category");
            }
            // Otherwise, an area node was selected.
            else
            {
                contextMenuTreeViewAreas.Items.Clear();
                contextMenuTreeViewAreas.Items.Add("Delete Area");
            }
        }

        /// <summary>
        /// Handles different actions when the context menu items are clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuTreeViewAreas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // An item in the context menu for a category was selected.
            if (treeViewAreas.SelectedNode.Parent == treeViewAreas.TopNode)
            {

            }
            // An item in the context menu for an area was selected.
            else if (treeViewAreas.SelectedNode != treeViewAreas.TopNode)
            {
            }
        }

    }
}
