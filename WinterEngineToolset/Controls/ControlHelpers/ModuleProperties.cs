using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.Toolset.Controls.ControlHelpers
{
    public partial class ModuleProperties : Form
    {
        public ModuleProperties()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Update the module in the database from data entered by user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (nameTextBoxModule.IsValid && tagTextBoxModule.IsValid)
            {
                using (ModuleRepository repo = new ModuleRepository())
                {
                    GameModule module = new GameModule();
                    module.Comment = textBoxComments.Text;
                    module.Description = textBoxDescription.Text;
                    module.MaxLevel = (int)numericUpDownMaxLevel.Value;
                    module.ModuleName = nameTextBoxModule.NameText;
                    module.ModuleTag = tagTextBoxModule.TagText;

                    repo.UpdateModule(module);
                }

                this.Dispose();
            }
            else
            {
                MessageBox.Show("Please enter a valid name and tag.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!nameTextBoxModule.IsValid)
                {
                    nameTextBoxModule.Focus();
                }
                else
                {
                    tagTextBoxModule.Focus();
                }
            }
        }

        /// <summary>
        /// Populate fields based on information in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModuleProperties_Load(object sender, EventArgs e)
        {
            GameModule module;

            using (ModuleRepository repo = new ModuleRepository())
            {
                module = repo.GetModule();
            }

            nameTextBoxModule.NameText = module.ModuleName;
            tagTextBoxModule.TagText = module.ModuleTag;
            numericUpDownMaxLevel.Value = module.MaxLevel;
            textBoxDescription.Text = module.Description;
            textBoxComments.Text = module.Comment;
            
        }
    }
}
