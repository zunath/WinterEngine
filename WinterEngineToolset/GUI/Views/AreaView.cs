using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;

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
        }

        private void AddXNAViewerControl()
        {
            _areaEditorControl = new AreaEditorControl();
            _areaEditorControl.Dock = DockStyle.Fill;
            panelAreaEditorControl.Controls.Add(_areaEditorControl);            
        }
    }
}
