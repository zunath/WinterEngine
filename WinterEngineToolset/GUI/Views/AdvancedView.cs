using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects.Graphics;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class AdvancedView : UserControl
    {

        #region Fields

        private WinterContext _context;

        #endregion

        #region Properties

        public WinterContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        #endregion

        #region Constructors


        public AdvancedView()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handling

        private void AdvancedView_Load(object sender, EventArgs e)
        {
            Context = new WinterContext(WinterConnectionInformation.ActiveConnectionString);

            comboBoxTable.Items.Add(new TableInfo { DisplayName = "Classes", TableType = TableTypeEnum.CharacterClass });
            comboBoxTable.Items.Add(new TableInfo { DisplayName = "Item Properties", TableType = TableTypeEnum.ItemProperty });
            comboBoxTable.Items.Add(new TableInfo { DisplayName = "Item Types", TableType = TableTypeEnum.Item });

            comboBoxTable.SelectedItem = comboBoxTable.Items[0];
        }

        
        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void dataGridViewAdvanced_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void buttonNewRow_Click(object sender, EventArgs e)
        {
            TableInfo tableInfo = comboBoxTable.SelectedItem as TableInfo;
            TableTypeEnum type = tableInfo.TableType;

            if (type == TableTypeEnum.CharacterClass)
            {
                CharacterClass characterClass = new CharacterClass { Name = "New Class", IsSystemResource = false, Comment = "" };
                Context.CharacterClasses.Add(characterClass);
            }
            else if (type == TableTypeEnum.Item)
            {
                ItemType itemType = new ItemType {Name = "New Item Type", IsSystemResource = false, Comment = "" };
                Context.ItemTypes.Add(itemType);
            }
            else if (type == TableTypeEnum.ItemProperty)
            {
                ItemProperty itemProperty = new ItemProperty { Name = "New Item Property", IsSystemResource = false, Comment = "" };
                Context.ItemProperties.Add(itemProperty);
            }

            Context.SaveChanges();
            RefreshDataGrid();
        }

        private void dataGridViewAdvanced_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Context.SaveChanges();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves data from the context and updates the data grid
        /// </summary>
        private void RefreshDataGrid()
        {
            TableInfo tableInfo = comboBoxTable.SelectedItem as TableInfo;
            TableTypeEnum type = tableInfo.TableType;

            if (type == TableTypeEnum.CharacterClass)
            {
                var query = from characterClass
                            in Context.CharacterClasses
                            where !characterClass.IsSystemResource
                            select characterClass;

                dataGridViewAdvanced.DataSource = query.ToList();
                dataGridViewAdvanced.Columns[0].ReadOnly = true;
            }
            else if (type == TableTypeEnum.ItemProperty)
            {
                var query = from itemPropertyType
                            in Context.ItemProperties
                            where !itemPropertyType.IsSystemResource
                            select itemPropertyType;

                dataGridViewAdvanced.DataSource = query.ToList();
                dataGridViewAdvanced.Columns[0].ReadOnly = true;
            }
            else if (type == TableTypeEnum.Item)
            {
                var query = from itemType
                            in Context.ItemTypes
                            where !itemType.IsSystemResource
                            select itemType;

                dataGridViewAdvanced.DataSource = query.ToList();
                dataGridViewAdvanced.Columns[0].ReadOnly = true;
            }
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            if (dataGridViewAdvanced.SelectedRows.Count > 0)
            {
                string message = "Are you sure you want to delete this row?";

                if (dataGridViewAdvanced.SelectedRows.Count > 1)
                {
                    message = "Are you sure you want to delete these rows?";
                }

                if (MessageBox.Show(message, "Really Delete?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    TableInfo tableInfo = comboBoxTable.SelectedItem as TableInfo;
                    TableTypeEnum type = tableInfo.TableType;

                    foreach (DataGridViewRow row in dataGridViewAdvanced.SelectedRows)
                    {
                        if (type == TableTypeEnum.CharacterClass)
                        {
                            CharacterClass characterClass = row.DataBoundItem as CharacterClass;
                            Context.CharacterClasses.Remove(characterClass);
                        }
                        else if (type == TableTypeEnum.Item)
                        {
                        }
                        else if (type == TableTypeEnum.ItemProperty)
                        {
                        }
                    }
                    Context.SaveChanges();
                    RefreshDataGrid();
                }
            }
        }

        #endregion


    }
}
