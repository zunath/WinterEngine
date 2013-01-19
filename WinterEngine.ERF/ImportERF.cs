using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinterEngine.ERF
{
    public partial class ImportERF : Form
    {
        public ImportERF()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles selecting all items in the list box when user clicks the "Select All" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            // Disable the visibility so that the control won't try to redraw itself
            // while selecting all objects. This speeds up the process.
            listBoxResources.Visible = false;

            for (int index = 0; index < listBoxResources.Items.Count; index++)
            {
                listBoxResources.SetSelected(index, true);
            }

            // Make it visible again.
            listBoxResources.Visible = true;
        }

        /// <summary>
        /// Handles de-selecting all items in the list box when user clicks the "Select None" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectNone_Click(object sender, EventArgs e)
        {
            listBoxResources.Visible = false;

            for (int index = 0; index < listBoxResources.Items.Count; index++)
            {
                listBoxResources.SetSelected(index, false);
            }

            listBoxResources.Visible = true;
        }

        /// <summary>
        /// Handles exiting the import ERF form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
