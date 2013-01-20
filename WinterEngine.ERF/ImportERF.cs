using System;
using System.Collections.Generic;
using System.IO;
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

        private List<GameObject> _duplicateList;
        private List<GameObject> _nonDuplicateList;
        private List<GameObject> _fullList;
        private string _tempDirectory;

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

        /// <summary>
        /// Gets or sets the temporary directory containing ERF files.
        /// </summary>
        public string TemporaryDirectory
        {
            get { return _tempDirectory; }
            set { _tempDirectory = value; }
        }


        #endregion

        #region Constructors

        public ImportERF()
        {
            InitializeComponent();
            InitializeFilters();
        }

        #endregion

        #region Events / Delegates

        public event EventHandler OnERFImported;

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
            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                foreach (GameObject gameObject in FullList)
                {
                    gameObject.ResourceCategoryID = repo.GetUncategorizedCategory(gameObject.ResourceType).ResourceCategoryID;
                }
            }

            GameObjectFactory factory = new GameObjectFactory();
            factory.AddToDatabase(FullList);

            if (!Object.ReferenceEquals(OnERFImported, null))
            {
                // Make a call back to subscribers. Typically this is used to update the TreeViews of the main form.
                EventArgs eventArgs = new EventArgs();
                OnERFImported(this, eventArgs);
            }

            if (Directory.Exists(TemporaryDirectory))
            {
                Directory.Delete(TemporaryDirectory, true);
            }

            this.Dispose();
        }

        /// <summary>
        /// Call this to attempt an ERF import. It will ask
        /// user for an ERF file to import. If the ERF has conflicting resources,
        /// this form will appear. Otherwise, all of the resources will be imported
        /// to the module.
        /// </summary>
        public void AttemptImport()
        {
            try
            {
                string erfDatabaseConnectionString = "";
                WinterFileHelper fileHelper = new WinterFileHelper();
                TemporaryDirectory = fileHelper.CreateTemporaryDirectory("erf");
                string erfDatabaseFilePath = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (ZipFile zipFile = new ZipFile(openFileDialog.FileName))
                    {
                        zipFile.ExtractAll(TemporaryDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }

                    erfDatabaseFilePath = fileHelper.GetDatabaseFileInDirectory(TemporaryDirectory);

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
                        // Update the game object's TemporaryDisplayName so that the list view shows it properly
                        string resourceTypeName = EnumerationHelper.GetEnumerationDescription(gameObject.ResourceType);
                        gameObject.TemporaryDisplayName = resourceTypeName + "/" + gameObject.Name + " (" + gameObject.Resref + ")";
                    
                        listBoxResources.Items.Add(gameObject);
                    }

                    this.Show();
                }

            }
            catch (Exception ex)
            {
                if (Directory.Exists(TemporaryDirectory))
                {
                    Directory.Delete(TemporaryDirectory, true);
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

        /// <summary>
        /// Handles adding selected resources to the full list, which will then be imported.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImport_Click(object sender, EventArgs e)
        {
            GameObjectFactory factory = new GameObjectFactory();

            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                foreach (GameObject gameObject in listBoxResources.SelectedItems)
                {
                    gameObject.ResourceCategoryID = repo.GetUncategorizedCategory(gameObject.ResourceType).ResourceCategoryID;
                    factory.UpdateInDatabase(gameObject);
                }
            }
            // Handle importing the non-duplicate objects.
            FullList = NonDuplicateList;
            DoImport();

            this.Dispose();
        }

        #endregion
    }
}
