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

        private List<ResourceTypeDTO> _resourceTypeList;
        private ResourceTypeDTO _selectedResourceType;

        #endregion

        #region Properties

        public ResourceTypeDTO ResourceType
        {
            get { return _selectedResourceType; }
            set { _selectedResourceType = value; }
        }

        #endregion


        public AddCategoryControl()
        {
            InitializeComponent();
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {

        }

        private void AddCategoryControl_Load(object sender, EventArgs e)
        {
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
        }
    }
}
