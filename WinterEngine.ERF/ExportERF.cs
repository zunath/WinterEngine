using System;
using System.Windows.Forms;
using WinterEngine.Library.Factories;
using System.Collections.Generic;
using WinterEngine.Library.Helpers;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataAccess;

namespace WinterEngine.ERF
{
    public partial class ExportERF : Form
    {
        #region Fields

        private string _fileLocation;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file location which is used for saving and loading ERFs.
        /// </summary>
        private string FileLocation
        {
            get { return _fileLocation; }
            set { _fileLocation = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Main constructor
        /// </summary>
        public ExportERF()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods - Menu Items

        /// <summary>
        /// Handles closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonExit.PerformClick();
        }

        /// <summary>
        /// Handles saving the ERF file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles saving the ERF file, letting user to select a new location for the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles resetting the "Added" list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles opening an ERF file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles closing the current ERF file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles adding a resource to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            foreach (var selectedObject in listBoxAvailable.SelectedItems)
            {
                GameObject selectedGameObject = selectedObject as GameObject;

                bool exists = false;
                foreach (var addedObject in listBoxAdded.Items)
                {
                    GameObject addedGameObject = addedObject as GameObject;

                    // Resrefs must be unique to individual resource types. It's OK if an area has the same resref as a creature.
                    if (addedGameObject.Resref == selectedGameObject.Resref && addedGameObject.ResourceType == selectedGameObject.ResourceType)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    listBoxAdded.Items.Add(selectedGameObject);
                }
            }
        }

        /// <summary>
        /// Handles removing a resource from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for(int index = listBoxAdded.SelectedItems.Count - 1; index >= 0; index --)
            {
                listBoxAdded.Items.Remove(listBoxAdded.SelectedItems[index]);
            }
        }

        /// <summary>
        /// Handles saving the ERF file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles closing the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            comboBoxResourceType.Items.Add(new ERFResource(ResourceTypeEnum.Area));
            comboBoxResourceType.Items.Add(new ERFResource(ResourceTypeEnum.Creature));
            comboBoxResourceType.Items.Add(new ERFResource(ResourceTypeEnum.Item));
            comboBoxResourceType.Items.Add(new ERFResource(ResourceTypeEnum.Placeable));
            comboBoxResourceType.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles updating the available resources when the index of the combo box is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxResourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameObjectFactory factory = new GameObjectFactory();
            ERFResource resource = comboBoxResourceType.SelectedItem as ERFResource;

            // Clear existing objects
            listBoxAvailable.Items.Clear();

            // Retrieve all objects from the database based on the type of resource,
            // then add them to the "Available" list.
            List<GameObject> gameObjects = factory.GetAllFromDatabase(resource.ResourceType);

            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                foreach (GameObject currentGameObject in gameObjects)
                {
                    string resourceTypeName = EnumerationHelper.GetEnumerationDescription(currentGameObject.ResourceType);
                    string categoryName = repo.GetByResourceCategoryID(currentGameObject.ResourceCategoryID).ResourceName;
                    currentGameObject.Name = resourceTypeName + "/" + categoryName + "/" + currentGameObject.Name + " (" + currentGameObject.Resref + ")";
                    listBoxAvailable.Items.Add(currentGameObject);
                }
            }
        }

        /// <summary>
        /// Handles adding the selected item when it's double clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAvailable_DoubleClick(object sender, EventArgs e)
        {
            buttonAdd.PerformClick();
        }

        /// <summary>
        /// Handles removing the selected item when it's double clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAdded_DoubleClick(object sender, EventArgs e)
        {
            buttonRemove.PerformClick();
        }

        #endregion



    }
}
