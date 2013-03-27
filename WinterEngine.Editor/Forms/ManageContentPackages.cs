using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Utility;
using FlatRedBall;
using FlatRedBall.IO;
using WinterEngine.Editor.Services;
using System.IO;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.FileAccess;

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
        /// Saves changes to the database and closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (IsModified)
            {
                DialogResult result = MessageBox.Show("You are about to change the list of content packages used by this module. Doing this carries the risk of causing the game and/or editor to behave incorrectly or crash.\n\nAre you sure you want to proceed with this action?", 
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveContentPackages();
                    this.Close();
                }
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
                DialogResult result = MessageBox.Show("There are pending changes which have not been saved. Would you like to save them now?", "Save before closing?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
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

        /// <summary>
        /// Marks the form as being modified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxPackages_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            IsModified = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves changes to the content package list to the database.
        /// </summary>
        private bool SaveContentPackages()
        {
            bool saveCompleted = false;

            List<ContentPackage> contentPackages = new List<ContentPackage>();

            foreach (ContentPackage package in checkedListBoxPackages.Items)
            {
                contentPackages.Add(package);
            }

            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                repo.ReplaceAll(contentPackages);
            }

            // Add references to the content package files to the database.
            ImportContentPackagesToDatabase(contentPackages);

            return saveCompleted;
        }

        /// <summary>
        /// Imports data from a list of content packages into the module's database.
        /// </summary>
        /// <param name="contentPackages"></param>
        private void ImportContentPackagesToDatabase(List<ContentPackage> contentPackages)
        {

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
            FileExtensionFactory factory = new FileExtensionFactory();
            List<string> files = FileManager.GetAllFilesInDirectory(DirectoryPaths.ContentPackageDirectoryPath, factory.GetFileExtension(FileTypeEnum.ContentPackage));

            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                foreach (string currentFile in files)
                {
                    ContentPackage package = new ContentPackage();
                    package.ContentPackagePath = currentFile;
                    package.VisibleName = Path.GetFileNameWithoutExtension(currentFile);
                    package.FileName = package.VisibleName;

                    int index = checkedListBoxPackages.Items.Add(package);
                    if (repo.Exists(checkedListBoxPackages.Items[index] as ContentPackage))
                    {
                        checkedListBoxPackages.SetItemChecked(index, true);
                    }

                }
            }
        }

        #endregion



    }
}
