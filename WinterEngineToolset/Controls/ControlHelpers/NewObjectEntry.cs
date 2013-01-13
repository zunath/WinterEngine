using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Library.DataAccess.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.Enumerations;
using WinterEngine.Library.DataAccess.Repositories;
using WinterEngine.Library.DataAccess.DataTransferObjects.GameObjects;
using WinterEngine.Toolset.Factories;
using WinterEngine.Library.Helpers;

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
            set { _resourceType = value; }
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
        private bool Validation(bool usePopUpForResrefDuplicate = false)
        {
            errorProvider.Clear();

            string nameText = nameTextBoxEntry.NameText;
            string tagText = tagTextBoxEntry.TagText;
            string resrefText = resrefTextBoxEntry.ResrefText;
            Regex tagRegex = new Regex("^[a-zA-Z0-9_]*$");
            Regex resrefRegex = new Regex("^[a-zA-Z0-9_]*$");
            bool succeed = true;

            // No character restrictions on the name field. It just can't be blank
            if (nameText == "")
            {
                errorProvider.SetError(nameTextBoxEntry, "Invalid Name");
                succeed = false;
            }

            if (!tagRegex.IsMatch(tagText) || tagText == "")
            {
                errorProvider.SetError(tagTextBoxEntry, "Invalid Tag");
                succeed = false;
            }

            if (!resrefRegex.IsMatch(resrefText) || resrefText == "")
            {
                errorProvider.SetError(resrefTextBoxEntry, "Invalid Resref");
                succeed = false;
            }

            GameObjectFactory factory = new GameObjectFactory();
            if(factory.DoesObjectExistInDatabase(resrefText, ResourceType))  
            {
                errorProvider.SetError(resrefTextBoxEntry, "This resref is already in use!");

                if (usePopUpForResrefDuplicate)
                {
                    MessageBox.Show("This resref is already in use. Please select another.", "Resref in Use!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                succeed = false;
            }
            

            return succeed;
        }

        /// <summary>
        /// Handles validation of user input one last time before sending to the data layer.
        /// If all checks out, go ahead and add the new object to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool succeed = Validation(true);

            if (succeed)
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
        }

        private void nameTextBoxEntry_Leave(object sender, EventArgs e)
        {
            Validation(false);
        }

        private void tagTextBoxEntry_Leave(object sender, EventArgs e)
        {
            Validation(false);
        }

        private void resrefTextBoxEntry_Leave(object sender, EventArgs e)
        {
            Validation(false);
        }
        #endregion
    }
}
