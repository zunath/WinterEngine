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
using WinterEngine.Toolset.GUI.Views;
using DejaVu;
using WinterEngine.Toolset.Helpers;
using WinterEngine.Toolset.Enumerations;
using System.IO;

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

        /// <summary>
        /// Opens module selection window. If user selects a module file,
        /// the toolset will open that module's data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Change openFileDialog's settings before opening it.
            openFileDialog.RestoreDirectory = false;

            WinterFileHelper fileHelper = new WinterFileHelper();
            string fileExtension = fileHelper.getFileExtension(FileType.Module);
            openFileDialog.Filter = "Winter Module Files (*" + fileExtension + ") | " + "*" + fileExtension;

            DialogResult result = openFileDialog.ShowDialog();
            // Pop up file selection dialog box.
            if (result == DialogResult.OK)
            {
                // Create temporary directory to decompress files to
                DirectoryInfo directoryInfo = Directory.CreateDirectory("./WE_Temp");
                // File was selected. Attempt to load it.
                try
                {
                    fileHelper.DecompressModule(openFileDialog.FileName, directoryInfo.FullName);
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorDialog("Error opening module. Path: " + directoryInfo.FullName, ex);
                }
            }

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
