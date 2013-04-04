using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.FileAccess.Repositories;
using WinterEngine.Library.Factories;

namespace WinterEngine.Editor.Forms
{
    public partial class ContentPackageCreator : Form
    {
        #region Fields

        private bool _contentPackageLoaded;
        private ContentPackage _contentPackage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether a content package is loaded.
        /// </summary>
        private bool IsContentPackageLoaded
        {
            get { return _contentPackageLoaded; }
            set { _contentPackageLoaded = value; }
        }

        /// <summary>
        /// Gets or sets the content package currently loaded.
        /// </summary>
        private ContentPackage Package
        {
            get { return _contentPackage; }
            set { _contentPackage = value; }
        }

        #endregion

        #region Constructors

        public ContentPackageCreator()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Handles loading an image in the preview PictureBox panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxResources.SelectedItems.Count > 0)
            {
                ContentPackageBuilderResource resource = listBoxResources.SelectedItem as ContentPackageBuilderResource;
                pictureBoxPreview.Image = Bitmap.FromFile(resource.ResourcePath);
            }
            else
            {
                pictureBoxPreview.Image = null;
            }
        }

        /// <summary>
        /// Handles initialization of controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPackageCreator_Load(object sender, EventArgs e)
        {
            FileExtensionFactory extensionFactory = new FileExtensionFactory();
            saveFileDialog.Filter = extensionFactory.BuildContentPackageFileFilter();
            openFileDialogContentPackages.Filter = extensionFactory.BuildContentPackageFileFilter();
            openFileDialogResources.Filter = extensionFactory.BuildContentPackageResourceFileFilter();
        }

        /// <summary>
        /// Handles saving the list of content package resources to a CPAK file on disk.
        /// If this is the first time a user has saved the content package, they will be prompted to select a save location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsContentPackageLoaded)
            {
                DoSave();
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Prompts user to select a save location and saves the list of resources to a CPAK file on disk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Package = new ContentPackage();
                    Package.ContentPackagePath = saveFileDialog.FileName;
                    Package.FileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                    Package.ResourceType = ResourceTypeEnum.ContentPackage;
                    Package.IsSystemResource = false;

                    DoSave();

                    IsContentPackageLoaded = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving content package.", ex);
            }
        }

        /// <summary>
        /// Handles closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Prompts user to add file(s) to the content package.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialogResources.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialogResources.FileNames)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);

                    if (!DoesResourceExist(fileNameWithoutExtension))
                    {
                        ContentPackageBuilderResource resource = new ContentPackageBuilderResource(file, GameObjectTypeEnum.Item, ContentBuilderFileTypeEnum.ExternalFile);
                        resource.ResourceName = Path.GetFileNameWithoutExtension(file);

                        listBoxResources.Items.Add(resource);
                    }
                }
            }
        }

        /// <summary>
        /// Removes all of the selected items from the resources list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveFiles_Click(object sender, EventArgs e)
        {
            for (int current = listBoxResources.SelectedItems.Count - 1; current >= 0; current--)
            {
                listBoxResources.Items.Remove(listBoxResources.SelectedItems[current]);
            }
        }

        /// <summary>
        /// Handles prompting user to open a content package file.
        /// Once opened, all resources in the content package are loaded to the resource list box control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogContentPackages.ShowDialog() == DialogResult.OK)
            {
                listBoxResources.Items.Clear();
                using (ContentPackageFileRepository manager = new ContentPackageFileRepository())
                {
                    Package = manager.ConvertFileToContentPackage(openFileDialogContentPackages.FileName);
                    listBoxResources.Items.AddRange(manager.GetContentPackageResourcesFromManifest(Package).ToArray());
                }

                IsContentPackageLoaded = true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns true if a resource with the specified name exists in the list box.
        /// Returns false if no resource with the specified name exists in the list box.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private bool DoesResourceExist(string resourceName)
        {
            bool exists = false;

            foreach (ContentPackageBuilderResource resource in listBoxResources.Items)
            {
                if (resource.ResourceName == resourceName)
                {
                    exists = true;
                    break;
                }
            }
            
            return exists;
        }

        /// <summary>
        /// Handles saving all of the resources in the form's list box to disk.
        /// </summary>
        private void DoSave()
        {
            List<ContentPackageBuilderResource> resources = new List<ContentPackageBuilderResource>();

            foreach (ContentPackageBuilderResource resource in listBoxResources.Items)
            {
                resources.Add(resource);
            }

            using (ContentPackageFileRepository repo = new ContentPackageFileRepository())
            {
                repo.SaveContentPackageToDisk(Package, resources);
            }

            MessageBox.Show("Content package was saved successfully!", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
            
        }

        #endregion



    }
}
