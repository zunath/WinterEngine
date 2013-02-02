﻿using System;
using System.IO;
using System.Windows.Forms;
using WinterEngine.Hakpak.Builder;
using WinterEngine.Library.Factories;
using WinterEngine.Toolset.Controls.ControlHelpers;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.Library.Helpers;
using Ionic.Zip;
using Ionic.Zlib;
using WinterEngine.ERF;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Toolset.Controls.XnaControls;

namespace WinterEngine.Toolset
{
    public partial class MainForm : Form
    {
        #region Fields

        private HakBuilder _hakpakBuilder; // Temporarily storing the hakpak builder form to ensure that only one instance is open at a time.
        private ExportERF _exportERF;
        private ImportERF _importERF;
        private ModuleProperties _moduleProperties;
        private WinterModuleFactory _activeModuleFactory;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the active module object used by this form.
        /// </summary>
        private WinterModuleFactory ActiveModuleFactory
        {
            get { return _activeModuleFactory; }
            set { _activeModuleFactory = value; }
        }

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
            ActiveModuleFactory = new WinterModuleFactory("", "", OnModuleOpened, OnModuleSaved, OnModuleClosed);

            FileExtensionFactory winterExtensions = new FileExtensionFactory();
            string fileExtension = winterExtensions.GetFileExtension(FileTypeEnum.Module);
            openFileDialog.Filter = "Winter Module Files (*" + fileExtension + ") | " + "*" + fileExtension;
            saveFileDialog.Filter = openFileDialog.Filter;
        }
        
        #endregion

        #region GUI methods

        /// <summary>
        /// Unloads all data in tree views and other controls.
        /// </summary>
        private void UnloadAllControls()
        {
            areaView.UnloadControls();
            itemView.UnloadControls();
            creatureView.UnloadControls();
            placeableView.UnloadControls();
        }

        /// <summary>
        /// Reloads all controls on the main form.
        /// </summary>
        private void RefreshAllControls()
        {
            areaView.RefreshControls();
            itemView.RefreshControls();
            creatureView.RefreshControls();
            placeableView.RefreshControls();
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
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ActiveModuleFactory.OpenModule(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error opening module. ", ex);
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
            newModuleEntryForm.OnModuleCreationSuccess += new EventHandler<ModuleCreationEventArgs>(OnModuleCreated);
            newModuleEntryForm.ShowDialog();
        }

        /// <summary>
        /// Pops up a text box giving some information about the toolset.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
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
            ActiveModuleFactory.CloseModule();

        }

        /// <summary>
        /// Saves a module at the set location. If no location has been set,
        /// the "Save As" button will fire instead.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemSaveModule_Click(object sender, EventArgs e)
        {
            // Path to module has not been set - prompt user to define it
            if (String.IsNullOrEmpty(ActiveModuleFactory.ModulePath))
            {
                toolStripMenuItemSaveAsModule.PerformClick();
            }
            // Otherwise, use the existing path.
            else
            {
                ActiveModuleFactory.SaveModule();
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
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ActiveModuleFactory.SaveModule(saveFileDialog.FileName);
            }
        }


        /// <summary>
        /// Handles loading the import ERF form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemImportERF_Click(object sender, EventArgs e)
        {
            _importERF = new ImportERF();
            _importERF.OnERFImported += OnERFImportedSuccessfully;
            _importERF.AttemptImport();
        }

        /// <summary>
        /// Handles loading the export ERF form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemExportERF_Click(object sender, EventArgs e)
        {
            _exportERF = new ExportERF();
            _exportERF.ShowDialog();
        }

        /// <summary>
        /// Handles loading the Winter Engine website in the system's default web browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemWebsite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.winterengine.com/");
        }

        /// <summary>
        /// Handles displaying the module properties window which contains data about the module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemModuleProperties_Click(object sender, EventArgs e)
        {
            if (Object.ReferenceEquals(_moduleProperties, null) || _moduleProperties.IsDisposed)
            {
                _moduleProperties = new ModuleProperties();
            }

            _moduleProperties.Show();
        }

        #endregion

        #region Module event methods

        /// <summary>
        /// Enables all controls related to module editing.
        /// Loads all data from the database into the appropriate controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModuleCreated(object sender, ModuleCreationEventArgs e)
        {
            ActiveModuleFactory.CloseModule();
            ActiveModuleFactory = e.ModuleFactory;
            ActiveModuleFactory.ModuleClosedMethod = OnModuleClosed;
            ActiveModuleFactory.ModuleOpenedMethod = OnModuleOpened;
            ActiveModuleFactory.ModuleSavedMethod = OnModuleSaved;

            // Refresh the controls for all views. This will populate the tree views
            // and perform other GUI-related tasks.
            RefreshAllControls();

            ToggleModuleControlsEnabled(true);
        }

        /// <summary>
        /// Runs when the module is saved successfully.
        /// Handles updating references to the active module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModuleSaved()
        {
        }

        /// <summary>
        /// Runs when the module is opened successfully.
        /// Handles updating the GUI with new data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModuleOpened()
        {
            ToggleModuleControlsEnabled(true);
            RefreshAllControls();
        }

        /// <summary>
        /// Runs when the module is closed successfully.
        /// Handles unloading the GUI with data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModuleClosed()
        {
            ToggleModuleControlsEnabled(false);
            UnloadAllControls();
        }

        #endregion

        #region ERF Import/Export Methods

        /// <summary>
        /// Refreshes the tree views whenever an ERF is imported successfully.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnERFImportedSuccessfully(object sender, EventArgs e)
        {
            areaView.RefreshControls();
            itemView.RefreshControls();
            creatureView.RefreshControls();
            placeableView.RefreshControls();
        }

        #endregion

        #region Form action methods

        /// <summary>
        /// Handles closing the form.
        /// Will fire the CloseModule methods when called normally.
        /// Normally = User-initiated, windows, or any parent forms closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // We only want to do this if the application closes down normally
            if (e.CloseReason == CloseReason.FormOwnerClosing || e.CloseReason == CloseReason.MdiFormClosing ||
                e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.WindowsShutDown)
            {
                // Close the module before the form closes. This will ensure that the temporary directory will be
                // deleted.
                toolStripMenuItemCloseModule.PerformClick();
            }
        }

        /// <summary>
        /// Handles closing the form. This is the same as clicking the X button at the top right of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}
