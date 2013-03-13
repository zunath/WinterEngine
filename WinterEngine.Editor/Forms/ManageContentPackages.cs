using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.Library.Factories;

namespace WinterEngine.Editor.Forms
{
    public partial class ManageContentPackages : Form
    {
        #region Fields

        private bool _modified;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the list of content packages has been modified.
        /// </summary>
        private bool IsModified
        {
            get { return _modified; }
            set { _modified = value; }
        }

        #endregion

        #region Constructors

        public ManageContentPackages()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Initializes filters and loads current content packages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageContentPackages_Load(object sender, EventArgs e)
        {
            InitializeFilters();
            LoadContentPackages();
            IsModified = false;
        }

        /// <summary>
        /// Prompts user to select a new content package to add to the module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddContentPackage_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        /// <summary>
        /// Removes the selected Content Package from the module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveContentPackage_Click(object sender, EventArgs e)
        {
            ContentPackage package = listBoxContentPackages.SelectedItem as ContentPackage;

            if (!Object.ReferenceEquals(package, null))
            {
                listBoxContentPackages.Items.Remove(package);
            }
        }

        /// <summary>
        /// Saves the content packages to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveContentPackages();
            IsModified = false;
        }

        /// <summary>
        /// Saves changes to the database and closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                SaveContentPackages();
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                DialogResult result = MessageBox.Show("Save before closing?", "There are pending changes which have not been saved. Would you like to save them now?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveContentPackages();
                    this.Close();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves changes to the content package list to the database.
        /// </summary>
        private bool SaveContentPackages()
        {
            bool saveCompleted = false;

            return saveCompleted;
        }

        /// <summary>
        /// Initializes the filter for the OpenFileDialog.
        /// </summary>
        private void InitializeFilters()
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            openFileDialog.Filter = factory.BuildContentPackageFileFilter();
        }

        /// <summary>
        /// Loads all content packages currently attached to the module.
        /// System packages are not included in the list.
        /// </summary>
        private void LoadContentPackages()
        {
        }

        #endregion

    }
}
