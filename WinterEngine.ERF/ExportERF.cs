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
    public partial class ExportERF : Form
    {
        #region Fields

        #endregion

        #region Properties

        #endregion


        #region Constructors

        public ExportERF()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods - Menu Items

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonExit.PerformClick();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region Form events

        /// <summary>
        /// Handles initialization on form load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportERF_Load(object sender, EventArgs e)
        {
            comboBoxResourceType.Items.Add("Areas");
            comboBoxResourceType.Items.Add("Creatures");
            comboBoxResourceType.Items.Add("Items");
            comboBoxResourceType.Items.Add("Placeables");
            comboBoxResourceType.SelectedIndex = 0;
        }

        #endregion




    }
}
