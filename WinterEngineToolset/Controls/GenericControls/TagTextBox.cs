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
    public partial class TagTextBox : UserControl
    {
        public string TagText
        {
            get { return textBoxTag.Text; }
        }

        public TagTextBox()
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
            Tuple<string, int, int> retValue = textHelper.Validate(textBoxTag.Text, lettersOnly, textBoxTag.SelectionStart, textBoxTag.SelectionLength, true);

            textBoxTag.Text = retValue.Item1;
            textBoxTag.SelectionStart = retValue.Item2;
            textBoxTag.SelectionLength = retValue.Item3;
        }
    }
}
