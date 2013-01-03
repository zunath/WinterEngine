using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls;
using WinterEngine.Toolset.GUI.Views;
using DejaVu;
using WinterEngine.Toolset.Helpers;
using WinterEngine.Toolset.Enumerations;
using System.IO;
using WinterEngine.Toolset.Controls.ControlHelpers;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset
{
    public partial class MainForm : Form
    {
        #region Fields

        private string _temporaryDirectory;
        private HakpakBuilder hakpakBuilder; // Temporarily storing the hakpak builder form to ensure that only one instance is open at a time.


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the temporary directory used by the module.
        /// The temporary directory contains all module files (database, additional files, but not graphics)
        /// and are modified as changes are made by user. Changes are not copied over to the real module
        /// file until the user clicks "Save".
        /// </summary>
        public string TemporaryDirectory
        {
            get { return _temporaryDirectory; }
            set { _temporaryDirectory = value; }
        }

        #endregion


        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Menu Strip methods

        /// <summary>
        /// Opens module selection window. If user selects a module file,
        /// the toolset will open that module's data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                    // TO-DO: load into toolset.
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorDialog("Error opening module. Path: " + directoryInfo.FullName, ex);
                }
            }

        }

        /// <summary>
        /// Displays the New Module form which the user can use to create a new module.
        /// Once created, the module is loaded into the toolset.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemNewModule_Click(object sender, EventArgs e)
        {
            NewModuleEntry newModuleEntryForm = new NewModuleEntry();
            newModuleEntryForm.OnModuleCreationSuccess += new EventHandler<ModuleCreationEventArgs>(LoadModule);
            newModuleEntryForm.ShowDialog();
        }

        /// <summary>
        /// Pops up a text box giving some information about the toolset.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            // Not much info to display right now. Version number and other details to be added later.
            MessageBox.Show("Winter Engine Toolset\n\nDeveloped by Zunath.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// When module is created in child form, this form's TemporaryDirectory property
        /// must get set so that the toolset knows where to modify files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadModule(object sender, ModuleCreationEventArgs e)
        {
            this.TemporaryDirectory = e.TemporaryPathDirectory;
        }

        /// <summary>
        /// Opens the Hakpak Builder form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemHakpakBuilder_Click(object sender, EventArgs e)
        {
            bool isNull = Object.ReferenceEquals(hakpakBuilder, null);

            // Not instantiated or has been disposed
            if (isNull || hakpakBuilder.IsDisposed)
            {
                hakpakBuilder = new HakpakBuilder();
                hakpakBuilder.Show();
            }
            // Window is already open. Focus on it.
            else if (!isNull)
            {
                hakpakBuilder.Focus();
            }
        }

        #endregion


    }
}
