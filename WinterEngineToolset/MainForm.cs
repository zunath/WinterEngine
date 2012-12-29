using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.Data;
using WinterEngine.Toolset.Views;
using DejaVu;
using WinterEngine.Toolset.CustomAutoMapping;

namespace WinterEngine.Toolset
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            CustomMappings.Initialize();
            InitializeComponent();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonSaveChangesAreaDetails_Click(object sender, EventArgs e)
        {
            using (UndoRedoManager.Start("Changed"))
            {
                UndoRedoManager.Commit();
            }
        }

        private void buttonDiscardChangesAreaDetails_Click(object sender, EventArgs e)
        {
            UndoRedoManager.Undo();
        }
    }
}
