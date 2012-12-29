using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using WinterEngine.Toolset.Data.Repositories;
using DejaVu;
using AutoMapper;

namespace WinterEngine.Toolset.Views
{
    public partial class AreaView : UserControl
    {
        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        private AreaEditorControl _areaEditorControl;

        public AreaView()
        {
            InitializeComponent();
            AddXNAViewerControl();

            // Debugging

            using (AreaRepository repo = new AreaRepository())
            {
                LoadContent(repo.GetAllAreas());
            }

            // End debugging

        }

        private void AddXNAViewerControl()
        {
            _areaEditorControl = new AreaEditorControl();
            _areaEditorControl.Dock = DockStyle.Fill;
            panelAreaEditorControl.Controls.Add(_areaEditorControl);
        }

        /// <summary>
        /// Loads a list of area objects into the GUI of this control.
        /// The list of areas should be retrieved from the database first and then passed into this method.
        /// </summary>
        /// <param name="areaList"></param>
        private void LoadContent(List<AreaDTO> areaList)
        {
            // Category support not implemented yet. Use a basic root node
            treeViewAreas.Nodes.Add("Uncategorized");

            UndoRedoManager.Start("Debugging");

            foreach (AreaDTO currentArea in areaList)
            {
                TreeNode node = new TreeNode();
                node.Text = currentArea.Name;
                node.Tag = currentArea;

                treeViewAreas.Nodes[0].Nodes.Add(node);
            }
            UndoRedoManager.Commit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UndoRedoManager.Undo();
            MessageBox.Show("Undo");
        }

    }
}
