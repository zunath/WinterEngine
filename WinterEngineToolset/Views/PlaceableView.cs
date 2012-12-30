using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.Data.DataTransferObjects;
using DejaVu;

namespace WinterEngine.Toolset.Views
{
    public partial class PlaceableView : UserControl
    {
        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        private PlaceableEditorControl _placeableEditorControl;

        public PlaceableView()
        {
            InitializeComponent();
            AddXNAViewerControl();
        }

        private void AddXNAViewerControl()
        {
            _placeableEditorControl = new PlaceableEditorControl();
            _placeableEditorControl.Dock = DockStyle.Fill;
            panelPlaceableEditorControl.Controls.Add(_placeableEditorControl);            
        }

        /// <summary>
        /// Loads a list of placeable objects into the GUI of this control.
        /// The list of placeables should be retrieved from the repository first and then passed into this method.
        /// </summary>
        /// <param name="areaList"></param>
        private void LoadContent(List<PlaceableDTO> placeableList)
        {
            using (UndoRedoManager.StartInvisible("Load Placeable Tree List"))
            {
                foreach (PlaceableDTO currentPlaceable in placeableList)
                {
                    TreeNode node = new TreeNode();
                    node.Text = currentPlaceable.Name;
                    node.Tag = currentPlaceable;

                    treeViewPlaceables.Nodes[0].Nodes.Add(node);
                }
                UndoRedoManager.Commit();
            }
        }
    }
}
