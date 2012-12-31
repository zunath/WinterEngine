using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Toolset.Helpers;

namespace WinterEngine.Toolset.Controls.GenericControls
{
    public partial class NameTextBox : UserControl
    {

        public string Text
        {
            get { return textBoxName.Text; }
        }

        public NameTextBox()
        {
            InitializeComponent();
        }

    }
}
