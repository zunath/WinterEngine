using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
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
    }
}
