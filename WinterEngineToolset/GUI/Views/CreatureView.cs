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
    public partial class CreatureView : UserControl
    {
        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        private CreatureEditorControl _creatureEditorControl;

        public CreatureView()
        {
            InitializeComponent();
            AddXNAViewerControl();

        }

        private void AddXNAViewerControl()
        {
            _creatureEditorControl = new CreatureEditorControl();
            _creatureEditorControl.Dock = DockStyle.Fill;
            panelCreatureEditorControl.Controls.Add(_creatureEditorControl);
        }

        /// <summary>
        /// Loads a list of creature objects into the GUI of this control.
        /// The list of creatures should be retrieved from the repository first and then passed into this method.
        /// </summary>
        /// <param name="areaList"></param>
        private void LoadContent(List<CreatureDTO> creatureList)
        {
            using (UndoRedoManager.StartInvisible("Load Creature Tree List"))
            {
                foreach (CreatureDTO currentCreature in creatureList)
                {
                    TreeNode node = new TreeNode();
                    node.Text = currentCreature.Name;
                    node.Tag = currentCreature;

                    treeViewCreatures.Nodes[0].Nodes.Add(node);
                }
                UndoRedoManager.Commit();
            }
        }
    }
}
