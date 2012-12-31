using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class AreaViewControl : UserControl
    {
        // Custom controls are defined here due to a bug with the Visual Studio 2010 designer.
        private ObjectViewer3D _objectViewer;

        public AreaViewControl()
        {
            InitializeComponent();
            AddXNAViewerControl();
        }

        private void AddXNAViewerControl()
        {
            _objectViewer = new ObjectViewer3D();
            _objectViewer.Dock = DockStyle.Fill;
            panelObjectViewer.Controls.Add(_objectViewer);
        }

    }
}
