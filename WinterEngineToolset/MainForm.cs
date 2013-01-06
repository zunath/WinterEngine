using System;
using System.IO;
using System.Windows.Forms;
using WinterEngine.Hakpak.Builder;
using WinterEngine.Library.Enumerations;
using WinterEngine.Library.Factories;
using WinterEngine.Toolset.Controls.ControlHelpers;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.Toolset.Helpers;

namespace WinterEngine.Toolset
{
    public partial class MainForm : Form
    {
        #region Fields

        private HakBuilder hakpakBuilder; // Temporarily storing the hakpak builder form to ensure that only one instance is open at a time.


        #endregion

        #region Properties

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
            FileExtensionFactory winterExtensions = new FileExtensionFactory();
            WinterFileHelper fileHelper = new WinterFileHelper();
            string fileExtension = winterExtensions.GetFileExtension(FileType.Module);
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
            newModuleEntryForm.OnModuleCreationSuccess += new EventHandler<ModuleCreationEventArgs>(LoadModuleDataIntoToolset);
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
        /// Enables all controls related to module editing.
        /// Loads all data from the database into the appropriate controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadModuleDataIntoToolset(object sender, ModuleCreationEventArgs e)
        {
            areaView.RefreshControls();
            itemView.RefreshControls();
            creatureView.RefreshControls();
            placeableView.RefreshControls();
            ToggleModuleControlsEnabled(true);
        }

        /// <summary>
        /// Enables or disables all module-related controls.
        /// </summary>
        /// <param name="enabled"></param>
        private void ToggleModuleControlsEnabled(bool enabled)
        {
            toolStripMenuItemCloseModule.Enabled = enabled;
            toolStripMenuItemSaveModule.Enabled = enabled;
            toolStripMenuItemSaveAsModule.Enabled = enabled;
            toolStripMenuItemImportERF.Enabled = enabled;
            toolStripMenuItemExportERF.Enabled = enabled;
            toolStripMenuItemUndo.Enabled = enabled;
            toolStripMenuItemRedo.Enabled = enabled;
            toolStripMenuItemCopy.Enabled = enabled;
            toolStripMenuItemCut.Enabled = enabled;
            toolStripMenuItemPaste.Enabled = enabled;
            toolStripMenuItemModuleProperties.Enabled = enabled;
            toolStripMenuItemManageHakPaks.Enabled = enabled;
            tabControlMain.Enabled = enabled;
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
                hakpakBuilder = new HakBuilder();
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
