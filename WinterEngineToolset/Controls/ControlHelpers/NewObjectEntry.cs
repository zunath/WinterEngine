using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Helpers;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.Toolset.Controls.ControlHelpers
{
    public partial class NewObjectEntry : Form
    {
        #region Fields
        
        private ResourceTypeEnum _resourceType;
        private ResourceCategory _resourceCategory;
        
        #endregion

        #region Properties
        
        public ResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set 
            {
                resrefTextBoxEntry.ResourceType = value;
                _resourceType = value; 
            }
        }

        public ResourceCategory ResourceCategory
        {
            get { return _resourceCategory; }
            set { _resourceCategory = value; }
        }
        #endregion

        #region Constructors
        public NewObjectEntry(ResourceTypeEnum resourceType, ResourceCategory resourceCategory)
        {
            InitializeComponent();
            this.ResourceType = resourceType;
            this.ResourceCategory = resourceCategory;
        }
        #endregion

        #region Delegates / Events

        public event EventHandler RefreshParentGUI;

        #endregion

        #region Methods

        /// <summary>
        /// Handles validation of user input one last time before sending to the data layer.
        /// If all checks out, go ahead and add the new object to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (nameTextBoxEntry.IsValid && tagTextBoxEntry.IsValid && resrefTextBoxEntry.IsValid)
            {
                try
                {
                    // Build a new object
                    GameObjectFactory factory = new GameObjectFactory();
                    GameObject winterObject = factory.CreateObject(ResourceType);
                    winterObject.Name = nameTextBoxEntry.NameText;
                    winterObject.Tag = tagTextBoxEntry.TagText;
                    winterObject.Resref = resrefTextBoxEntry.ResrefText;
                    winterObject.ResourceCategoryID = ResourceCategory.ResourceCategoryID;
                    winterObject.ResourceType = ResourceType;

                    // Add it to the database.
                    factory.AddToDatabase(winterObject, ResourceType);

                    // Refresh the GUI and dispose of this form.
                    RefreshParentGUI(null, null);
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorDialog("Error adding new object. (Method: buttonOK_Click)", ex);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid name, tag, and resref.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (!nameTextBoxEntry.IsValid)
                {
                    nameTextBoxEntry.Focus();
                }
                else if (!tagTextBoxEntry.IsValid)
                {
                    tagTextBoxEntry.Focus();
                }
                else if (!resrefTextBoxEntry.IsValid)
                {
                    resrefTextBoxEntry.Focus();
                }
            }
        }

        #endregion
    }
}
