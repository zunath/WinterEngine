﻿using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.Enumerations;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using DejaVu;
using WinterEngine.Toolset.Factories;
using WinterEngine.Toolset.Helpers;

namespace WinterEngine.Toolset.Controls.ControlHelpers
{
    public partial class NewObjectEntry : Form
    {
        #region Fields
        
        private ResourceTypeEnum _resourceType;
        private ResourceCategoryDTO _resourceCategory;
        
        #endregion

        #region Properties
        
        public ResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        public ResourceCategoryDTO ResourceCategory
        {
            get { return _resourceCategory; }
            set { _resourceCategory = value; }
        }
        #endregion

        #region Constructors
        public NewObjectEntry(ResourceTypeEnum resourceType, ResourceCategoryDTO resourceCategory)
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

            string nameText = nameTextBoxEntry.Text;
            string tagText = tagTextBoxEntry.Text;
            string resrefText = resrefTextBoxEntry.Text;
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

            using (WinterObjectRepository repo = new WinterObjectRepository())
            {
                if (repo.DoesObjectExist(ResourceType, resrefText))
                {
                    errorProvider.SetError(resrefTextBoxEntry, "This resref is already in use!");

                    if (usePopUpForResrefDuplicate)
                        MessageBox.Show("This resref is already in use. Please select another.", "Resref in Use!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    succeed = false;
                }
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
                    UndoRedoManager.Start("Adding new object");

                    using (WinterObjectRepository repo = new WinterObjectRepository())
                    {
                        repo.AddObject(_resourceType, ResourceCategory.ResourceCategoryID, nameTextBoxEntry.Text, tagTextBoxEntry.Text, resrefTextBoxEntry.Text);
                    }
                    UndoRedoManager.Commit();
                    RefreshParentGUI(null, null);
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    UndoRedoManager.Cancel();
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
