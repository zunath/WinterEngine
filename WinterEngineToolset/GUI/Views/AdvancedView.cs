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
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class AdvancedView : UserControl
    {

        #region Fields

        #endregion

        #region Properties


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
            comboBoxTable.Items.Add(new TableInfo { DisplayName = "Classes", TableType = TableTypeEnum.CharacterClassTypes });
            comboBoxTable.Items.Add(new TableInfo { DisplayName = "Item Properties", TableType = TableTypeEnum.ItemPropertyTypes });
            comboBoxTable.Items.Add(new TableInfo { DisplayName = "Item Types", TableType = TableTypeEnum.ItemTypes });

            comboBoxTable.SelectedItem = comboBoxTable.Items[0];
        }


        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        #endregion

        #region Methods

        #endregion
    }
}
