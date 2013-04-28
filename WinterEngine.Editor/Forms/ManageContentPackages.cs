using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Enumerations;

using WinterEngine.Library.Factories;
using WinterEngine.Library.Utility;
using FlatRedBall;
using FlatRedBall.IO;
using WinterEngine.Editor.Services;
using System.IO;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataAccess.Factories;
using WinterEngine.Editor.Managers;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Paths;

namespace WinterEngine.Editor.Forms
{
    public partial class ManageContentPackages : Form
    {
        #region Fields

        private bool _modified;
        private bool _isLoaded;

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

        #region Events / Delegates

        public event EventHandler OnPackagesSaved;

        #endregion

        #region Event Handling

        /// <summary>
        /// Initializes filters and loads current content packages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageContentPackages_Load(object sender, EventArgs e)
        {
            UnloadContentPackages();
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
        /// Adds the selected package in the available content packages list to the attached content packages list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddPackage_Click(object sender, EventArgs e)
        {
            ContentPackage package = listBoxAvailableContentPackages.SelectedItem as ContentPackage;
            if (!Object.ReferenceEquals(package, null))
            {
                if (!DoesContentPackageExistInAttachedList(package))
                {
                    listBoxAttachedContentPackages.Items.Add(package);
                    IsModified = true;
                }
            }
        }

        /// <summary>
        /// Removes the selected package in the attached content packages list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemovePackage_Click(object sender, EventArgs e)
        {
            ContentPackage package = listBoxAttachedContentPackages.SelectedItem as ContentPackage;
            if (!Object.ReferenceEquals(package, null))
            {
                listBoxAttachedContentPackages.Items.Remove(package);
                IsModified = true;
            }
        }

        /// <summary>
        /// Loads the selected content package's name and description into the appropriate text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAttachedContentPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContentPackage package = listBoxAttachedContentPackages.SelectedItem as ContentPackage;
            if (!Object.ReferenceEquals(package, null))
            {
                textBoxDescription.Text = package.Description;
                textBoxName.Text = package.VisibleName;
            }
        }

        /// <summary>
        /// Loads the selected content package's name and description into the appropriate text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAvailableContentPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContentPackage package = listBoxAvailableContentPackages.SelectedItem as ContentPackage;
            if (!Object.ReferenceEquals(package, null))
            {
                textBoxName.Text = package.VisibleName;
                textBoxDescription.Text = package.Description;
            }
        }

        /// <summary>
        /// Handles adding the selected item to the attached packages list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAvailableContentPackages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonAddPackage.PerformClick();
        }

        /// <summary>
        /// Handles removing the selected item from the attached packages list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAttachedContentPackages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonRemovePackage.PerformClick();
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

            foreach (ContentPackage package in listBoxAttachedContentPackages.Items)
            {
                contentPackages.Add(package);
            }

            // Rebuild module references
            using (GameResourceManager manager = new GameResourceManager())
            {
                manager.RebuildModule(contentPackages);
            }

            if (!Object.ReferenceEquals(OnPackagesSaved, null))
            {
                OnPackagesSaved(this, new EventArgs());
            }

            return saveCompleted;
        }

        private void UnloadContentPackages()
        {
            listBoxAttachedContentPackages.Items.Clear();
            listBoxAvailableContentPackages.Items.Clear();
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
                    ContentPackage package = new ContentPackage(currentFile, false);
                    
                    int index = listBoxAvailableContentPackages.Items.Add(package);
                    if (repo.Exists(package))
                    {
                        listBoxAttachedContentPackages.Items.Add(package);
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if a content package with the same FileName exists in the attached list.
        /// Returns false if no content packages with the same FileName exist in the attached list.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private bool DoesContentPackageExistInAttachedList(ContentPackage package)
        {
            bool exists = false;

            foreach (ContentPackage current in listBoxAttachedContentPackages.Items)
            {
                if (current.FileName == package.FileName)
                {
                    exists = true;
                }
            }

            return exists;
        }

        #endregion


    }
}
