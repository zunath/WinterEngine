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

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class AreaView : UserControl
    {
        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        private AreaEditorControl _areaEditorControl;

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

        /// <summary>
        /// Loads a list of area objects into the GUI of this control.
        /// The list of areas should be retrieved from the repository first and then passed into this method.
        /// </summary>
        /// <param name="areaList"></param>
        private void LoadContent()
        {
            using (UndoRedoManager.StartInvisible("Load Area Tree List"))
            {
                List<AreaDTO> areaList = new List<AreaDTO>();
                List<ResourceCategoryDTO> categoryList = new List<ResourceCategoryDTO>();

                using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
                {
                    categoryList = repo.GetAllResourceCategories();
                }

                using (AreaRepository repo = new AreaRepository())
                {
                    areaList = repo.GetAllAreas();
                    treeViewAreas.Nodes[0].Nodes.Clear(); // Clear all nodes attached to root but not root iself.

                    foreach (AreaDTO currentArea in areaList)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = currentArea.Name;
                        node.Tag = currentArea;

                        treeViewAreas.Nodes[0].Nodes.Add(node);
                    }
                }

                UndoRedoManager.Commit();
            }
        }


        /// <summary>
        /// Method called from child component buttonAddCategory.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void buttonAddCategory_RefreshTreeView(object sender, EventArgs e)
        {
            
            LoadContent();
        }

    }
}
