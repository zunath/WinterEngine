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
using Ionic.Zip;
using Ionic.Zlib;

namespace WinterEngine.Toolset
{
    public partial class MainForm : Form
    {
        #region Fields

        private HakBuilder _hakpakBuilder; // Temporarily storing the hakpak builder form to ensure that only one instance is open at a time.
        private string _saveFilePath;
        private string _temporaryDirectory;

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
            // Copy the temporary directory over, since we will need to reference it later.
            _temporaryDirectory = e.TemporaryPathDirectory;

            // Refresh the controls for all views. This will populate the tree views
            // and perform other GUI-related tasks.
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
            bool isNull = Object.ReferenceEquals(_hakpakBuilder, null);

            // Not instantiated or has been disposed
            if (isNull || _hakpakBuilder.IsDisposed)
            {
                _hakpakBuilder = new HakBuilder();
                _hakpakBuilder.Show();
            }
            // Window is already open. Focus on it.
            else if (!isNull)
            {
                _hakpakBuilder.Focus();
            }
        }

        /// <summary>
        /// Closes the module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCloseModule_Click(object sender, EventArgs e)
        {
            ToggleModuleControlsEnabled(false);
            // TO-DO: Unload module resources
            

            // Delete the temporary directory
            Directory.Delete(_temporaryDirectory);

            // Reset the file paths for the module and temporary directory
            _saveFilePath = null;
            _temporaryDirectory = null;
        }

        /// <summary>
        /// Handles saving the module.
        /// Copies the temporary directory to a zip file and replaces any existing file.
        /// </summary>
        private void SaveModule()
        {
            int index = 0;
            try
            {
                // Generate a unique file name just in case another one already exists.
                // This is used just in case something goes wrong during the new save.
                while (File.Exists(_saveFilePath + index))
                {
                    index++;
                }

                // Make a back up of the module file just in case something goes wrong.
                if (File.Exists(_saveFilePath))
                {
                    File.Copy(_saveFilePath, _saveFilePath + index);
                }

                File.Delete(_saveFilePath);

                using (ZipFile zipFile = new ZipFile(_saveFilePath))
                {
                    // Change compression level to none (speeds up loading in-game and toolset)
                    // Add the directory and save the zip file.
                    zipFile.CompressionLevel = CompressionLevel.None;
                    zipFile.AddDirectory(_temporaryDirectory, "");
                    zipFile.Save();

                    // Delete the backup since the new save was successful.
                    File.Delete(_saveFilePath + index);
                }
            }
            catch (Exception ex)
            {
                // Something screwed up during the save. Delete the new zip file, if any
                // and then move the backup back to where it was.
                if (File.Exists(_saveFilePath + index))
                {
                    File.Delete(_saveFilePath);
                    File.Move(_saveFilePath + index, _saveFilePath);
                }

                ErrorHelper.ShowErrorDialog("Error saving module: ", ex);
            }
        }

        /// <summary>
        /// Saves a module at the set location. If no location has been set,
        /// the "Save As" button will fire instead.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemSaveModule_Click(object sender, EventArgs e)
        {
            // No location set - perform a click on the Save As button.
            if (String.IsNullOrEmpty(_saveFilePath))
            {
                toolStripMenuItemSaveAsModule.PerformClick();
            }
            else
            {
                SaveModule();
            }
        }

        /// <summary>
        /// Handles taking a user's specified location and saving the module at the
        /// destination chosen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemSaveAsModule_Click(object sender, EventArgs e)
        {
            // Set the filter
            FileExtensionFactory factory = new FileExtensionFactory();
            string extension = factory.GetFileExtension(FileType.Module);
            saveFileDialog.Filter = "Module Files (*" + extension + ")|*" + extension;

            // Display the save file pop-up.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the path to the module file
                _saveFilePath = saveFileDialog.FileName;

                // Actually perform the save now.
                SaveModule();
            }
        }

        #endregion
    }
}
