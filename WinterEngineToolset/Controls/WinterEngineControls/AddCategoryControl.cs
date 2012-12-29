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

        private int _resourceTypeID;

        #endregion

        #region Properties

        [Description("ID number of resource that will be used. Refer to database for values.")]
        public int ResourceTypeID
        {
            get { return _resourceTypeID; }
            set { _resourceTypeID = value; }
        }

        #endregion


        public AddCategoryControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles validation of text input by user.
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private bool Validation(string inputText)
        {
            bool isValid = true;

            return isValid;
        }

        private void Success()
        {
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
            InputMessageBox inputBox = new InputMessageBox("Enter the category's name.", "New Category", MinCategoryNameLength, MaxCategoryNameLength, Validation, Success, "Category Name");
            inputBox.ShowDialog();
        }

    }
}
