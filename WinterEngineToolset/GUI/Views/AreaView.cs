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
        }

        private void AddXNAViewerControl()
        {
            _areaEditorControl = new AreaEditorControl();
            _areaEditorControl.Dock = DockStyle.Fill;
            panelAreaEditorControl.Controls.Add(_areaEditorControl);
        }

        #endregion

    }
}
