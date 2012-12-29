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

namespace WinterEngine.Toolset.Controls.WinterEngineControls
{
    public partial class AddCategoryControl : UserControl
    {
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
        /// Handles popping up a modal dialog box which asks user to input a category name.
        /// Once a category name has been entered, the database will be updated with the new category.
        /// The tree view is refreshed to show the change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = Microsoft.VisualBasic.Interaction.InputBox("Category Name: ", "Add Category", "Default");



        }

        private void AddCategoryControl_Load(object sender, EventArgs e)
        {

            // Running into an issue where I can't pull from the database inside the VS2010 GUI designer.
            // Commented out but left here so I can look at it again later.
            /*
            UndoRedoManager.Start("Resource Type - Load");
            try
            {
                _resourceTypeList = new List<ResourceTypeDTO>();
                using (WinterContext context = new WinterContext())
                {
                
                    // Retrieve the list of resource types from database using LINQ.
                    // Then, map them to a list of ResourceTypeDTO and return the list.
                    var resourceTypes = from resourceType
                                        in context.ResourceTypes
                                        select resourceType;
                    _resourceTypeList = Mapper.Map(resourceTypes.ToList<ResourceType>(), _resourceTypeList);

                    UndoRedoManager.Commit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving resource types.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UndoRedoManager.Cancel();
            }
            */
        }
    }
}
