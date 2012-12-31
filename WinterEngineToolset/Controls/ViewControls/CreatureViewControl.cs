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
    public partial class CreatureViewControl : UserControl
    {
        private ObjectViewer3D _objectViewer;

        public CreatureViewControl()
        {
            InitializeComponent();

            // Designer in VS2010 has issues with custom controls.
            // Manually add the 3D object viewer when the program runs.
            _objectViewer = new ObjectViewer3D();
            _objectViewer.Dock = DockStyle.Fill;
            panelCreatureObjectViewer.Controls.Add(_objectViewer);
        }
    }
}
