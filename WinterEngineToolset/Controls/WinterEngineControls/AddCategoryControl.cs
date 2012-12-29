using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;
using DejaVu;
using WinterEngine.Toolset.Controls.ControlHelpers;
using WinterEngine.Toolset.Data.Repositories;
using WinterEngine.Toolset.Enumerations;

namespace WinterEngine.Toolset.Controls.WinterEngineControls
{
    public partial class AddCategoryControl : UserControl
    {
        #region Constants

        // Minimum and maximum lengths category names can be.
        private const int MinCategoryNameLength = 1;
        private const int MaxCategoryNameLength = 32;

        #endregion

        #region Fields

        private ResourceTypeEnum _resourceTypeEnum;

        #endregion

        #region Properties

        [Description("The resource type to add the category to.")]
        [DefaultValue(ResourceTypeEnum.Area)]
        public ResourceTypeEnum ResourceType
        {
            get { return _resourceTypeEnum; }
            set { _resourceTypeEnum = value; }
        }

        #endregion


        public AddCategoryControl()
        {
            InitializeComponent();
            _resourceTypeEnum = ResourceTypeEnum.Area;
        }

        /// <summary>
        /// Business layer validation for adding a new category.
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private bool ValidationMethod(string inputText)
        {
            bool isValid = true;

            if (inputText.Length < MinCategoryNameLength) isValid = false;
            if (inputText.Length > MaxCategoryNameLength) isValid = false;

            return isValid;
        }

        private void SuccessMethod(string inputText)
        {
            bool success;
            UndoRedoManager.Start("Add Category: " + inputText);

            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                ResourceCategoryDTO resourceCategoryDTO = new ResourceCategoryDTO();
                resourceCategoryDTO.ResourceName = inputText;
                resourceCategoryDTO.ResourceTypeID = (int)ResourceType;

                success = repo.AddResourceCategory(resourceCategoryDTO);
            }
            

            UndoRedoManager.Commit();
        }

        /// <summary>
        /// Handles popping up a modal dialog box which asks user to input a category name.
        /// Once a category name has been entered, the database will be updated with the new category.
        /// The tree view is refreshed to show the change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            InputMessageBox inputBox = new InputMessageBox("Enter the category's name.", "New Category", MinCategoryNameLength, MaxCategoryNameLength, ValidationMethod, SuccessMethod, "Category Name");
            inputBox.ShowDialog();
        }

    }
}
