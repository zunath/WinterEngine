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
    public partial class ResrefTextBox : UserControl
    {

        public string ResrefText
        {
            get { return textBoxResref.Text; }
        }

        public ResrefTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles validation whenever text in the resref text box is changed.
        /// Resrefs can only contain lower case letters and numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxResref_TextChanged(object sender, EventArgs e)
        {
            Regex lettersOnly = new Regex("^[a-zA-Z0-9_]*$");
            TextHelper textHelper = new TextHelper();
            Tuple<string, int, int> retValue = textHelper.Validate(textBoxResref.Text, lettersOnly, textBoxResref.SelectionStart, textBoxResref.SelectionLength, false);

            textBoxResref.Text = retValue.Item1;
            textBoxResref.SelectionStart = retValue.Item2;
            textBoxResref.SelectionLength = retValue.Item3;
        }
    }
}
