﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;

namespace WinterEngine.Toolset.Views
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

    }
}
