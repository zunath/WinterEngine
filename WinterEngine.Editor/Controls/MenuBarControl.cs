using System;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Editor.ExtendedEventArgs;
using WinterEngine.Editor.Forms;
using WinterEngine.ERF;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Utility;

namespace WinterEngine.Editor.Controls
{
    public partial class MenuBarControl : UserControl
    {
        #region Fields
        
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

        public MenuBarControl()
        {
            InitializeComponent();

            ActiveModuleFactory = new WinterModuleFactory("", "", OnModuleOpened, OnModuleSaved, OnModuleClosed);

            FileExtensionFactory winterExtensions = new FileExtensionFactory();
            string fileExtension = winterExtensions.GetFileExtension(FileTypeEnum.Module);
            openFileDialog.Filter = "Winter Module Files (*" + fileExtension + ") | " + "*" + fileExtension;
            saveFileDialog.Filter = openFileDialog.Filter;
        }

        #endregion

        #region Events / Delegates

        public event EventHandler OnRefreshControls;
        public event EventHandler OnUnloadControls;
        public event EventHandler<ModuleControlsEventArgs> OnToggleControls;

        #endregion

        #region Methods

        /// <summary>
        /// Launches the ManageContentPackages form, which allows user to add or remove content packages from the module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemManageContentPackages_Click(object sender, EventArgs e)
        {
            ManageContentPackages contentPackageForm = new ManageContentPackages();
            contentPackageForm.ShowDialog();
        }

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
            toolStripMenuItemManageContentPackages.Enabled = enabled;

            if (!Object.ReferenceEquals(OnToggleControls, null))
            {
                OnToggleControls(this, new ModuleControlsEventArgs(enabled));
            }
        }

        /// <summary>
        /// Opens the Content Package Builder form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemContentBuilder_Click(object sender, EventArgs e)
        {
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
            System.Diagnostics.Process.Start("https://www.winterengine.com/");
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
            if (!Object.ReferenceEquals(OnRefreshControls, null))
            {
                OnRefreshControls(this, new EventArgs());
            }

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
            if (!Object.ReferenceEquals(OnRefreshControls, null))
            {
                OnRefreshControls(this, new EventArgs());
            }
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

            if (!Object.ReferenceEquals(OnUnloadControls, null))
            {
                OnUnloadControls(this, new EventArgs());
            }
        }

        /// <summary>
        /// Refreshes the tree views whenever an ERF is imported successfully.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnERFImportedSuccessfully(object sender, EventArgs e)
        {
            if (!Object.ReferenceEquals(OnRefreshControls, null))
            {
                OnRefreshControls(this, new EventArgs());
            }
        }

        #endregion


    }
}
