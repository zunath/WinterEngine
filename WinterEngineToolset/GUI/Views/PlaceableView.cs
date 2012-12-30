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
    }
}
