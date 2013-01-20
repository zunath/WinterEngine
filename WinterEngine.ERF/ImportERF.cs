using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Helpers;

namespace WinterEngine.ERF
{
    public partial class ImportERF : Form
    {
        #region Fields

        List<GameObject> _duplicateList;
        List<GameObject> _nonDuplicateList;
        List<GameObject> _fullList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the list of duplicate game objects.
        /// </summary>
        public List<GameObject> DuplicateList
        {
            get { return _duplicateList; }
            set { _duplicateList = value; }
        }

        /// <summary>
        /// Gets or sets the list of non-duplicate game objects.
        /// </summary>
        public List<GameObject> NonDuplicateList
        {
            get { return _nonDuplicateList; }
            set { _nonDuplicateList = value; }
        }

        /// <summary>
        /// Gets or sets the full list of game objects.
        /// </summary>
        public List<GameObject> FullList
        {
            get { return _fullList; }
            set { _fullList = value; }
        }

        #endregion

        #region Constructors

        public ImportERF()
        {
            InitializeComponent();
            InitializeFilters();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the filters for the openFileDialog 
        /// </summary>
        private void InitializeFilters()
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            string erfFileExtension = factory.GetFileExtension(FileTypeEnum.Erf);

            openFileDialog.Filter = "Encapsulated Resource Files|*" + erfFileExtension; 
        }

        /// <summary>
        /// Add all of the objects in the FullList to the main database.
        /// </summary>
        /// <param name="gameObjectList"></param>
        private void DoImport()
        {
        }

        /// <summary>
        /// Call this to attempt an ERF import. It will ask
        /// user for an ERF file to import. If the ERF has conflicting resources,
        /// this form will appear. Otherwise, all of the resources will be imported
        /// to the module.
        /// </summary>
        public void AttemptImport()
        {
            string tempDirectory = "";
            try
            {
                string erfDatabaseConnectionString = "";
                WinterFileHelper fileHelper = new WinterFileHelper();
                tempDirectory = fileHelper.CreateTemporaryDirectory("erf");
                string erfDatabaseFilePath = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (ZipFile zipFile = new ZipFile(openFileDialog.FileName))
                    {
                        zipFile.ExtractAll(tempDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }

                    erfDatabaseFilePath = fileHelper.GetDatabaseFileInDirectory(tempDirectory);

                    using (DatabaseRepository repo = new DatabaseRepository())
                    {
                        erfDatabaseConnectionString = repo.BuildConnectionString(erfDatabaseFilePath);
                    }

                    using (ERFRepository repo = new ERFRepository(erfDatabaseConnectionString))
                    {
                        Tuple<List<GameObject>, List<GameObject>, List<GameObject>> gameObjectTuple = repo.GetDuplicateResources();
                        FullList = gameObjectTuple.Item1;
                        DuplicateList = gameObjectTuple.Item2;
                        NonDuplicateList = gameObjectTuple.Item3;
                    }
                }

                // No duplicates found. Do the import.
                if (DuplicateList.Count <= 0)
                {
                    DoImport();
                }
                else
                {
                    foreach (GameObject gameObject in DuplicateList)
                    {
                        listBoxResources.Items.Add(gameObject);
                    }

                    this.Show();
                }

            }
            catch (Exception ex)
            {
                if (Directory.Exists(tempDirectory))
                {
                    Directory.Delete(tempDirectory, true);
                }
                
                ErrorHelper.ShowErrorDialog("Error importing encapsulated resource file", ex);
            }
        }

        #endregion


        #region Event handling

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

        #endregion
    }
}
