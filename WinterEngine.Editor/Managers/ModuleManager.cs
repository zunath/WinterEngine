using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.FileAccess;
using WinterEngine.Library.Utility;
using WinterEngine.FileAccess.Repositories;

namespace WinterEngine.Editor.Managers
{
    public class ModuleManager
    {
        #region Fields

        private string _moduleName;
        private string _moduleTag;

        private string _modulePath;
        private string _tempDirectoryPath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the module's name.
        /// </summary>
        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        /// <summary>
        /// Gets or sets the module's tag.
        /// </summary>
        public string ModuleTag
        {
            get { return _moduleTag; }
            set { _moduleTag = value; }
        }
        
        /// <summary>
        /// Gets or sets the path to the module file.
        /// </summary>
        public string ModulePath
        {
            get { return _modulePath; }
            set { _modulePath = value; }
        }

        /// <summary>
        /// Gets or sets the path to the temporary directory for the module.
        /// </summary>
        public string TemporaryDirectoryPath
        {
            get { return _tempDirectoryPath; }
            set { _tempDirectoryPath = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor which creates a blank WinterModule.
        /// Be sure to set the ModuleOpened, ModuleSaved, and ModuleClosed delegates
        /// or you will get a null reference exception.
        /// </summary>
        public ModuleManager(string moduleName, string moduleTag)
        {
            ModuleName = moduleName;
            ModuleTag = moduleTag;
        }

        /// <summary>
        /// Constructor which builds a WinterModule object.
        /// </summary>
        /// <param name="OnModuleOpened">The method to fire when the module is opened/</param>
        /// <param name="OnModuleSaved">The method to fire when the module is saved.</param>
        /// <param name="OnModuleClosed">The method to fire when the module is closed.</param>
        public ModuleManager(string moduleName, string moduleTag, ModuleOpened OnModuleOpened, ModuleSaved OnModuleSaved, ModuleClosed OnModuleClosed)
        {
            ModuleName = moduleName;
            ModuleTag = moduleTag;
            _moduleClosedMethod = OnModuleClosed;
            _moduleOpenedMethod = OnModuleOpened;
            _moduleSavedMethod = OnModuleSaved;
        }

        #endregion

        #region Events / Delegates
        
        public delegate void ModuleOpened();
        public delegate void ModuleSaved();
        public delegate void ModuleClosed();

        private ModuleOpened _moduleOpenedMethod;
        private ModuleSaved _moduleSavedMethod;
        private ModuleClosed _moduleClosedMethod;

        /// <summary>
        /// Gets or sets the method fired when the module has finished opening.
        /// </summary>
        public ModuleOpened ModuleOpenedMethod
        {
            get { return _moduleOpenedMethod; }
            set { _moduleOpenedMethod = value; }
        }

        /// <summary>
        /// Gets or sets the method fired when the module has finished saving.
        /// </summary>
        public ModuleSaved ModuleSavedMethod
        {
            get { return _moduleSavedMethod; }
            set { _moduleSavedMethod = value; }
        }

        /// <summary>
        /// Gets or sets the method fired when the module has finished closing.
        /// </summary>
        public ModuleClosed ModuleClosedMethod
        {
            get { return _moduleClosedMethod; }
            set { _moduleClosedMethod = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new module in the temporary directory.
        /// Note that this module will not become permanent until a call to SaveModule() is made.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tempPath"></param>
        public void CreateModule()
        {
            using(FileArchiveManager manager = new FileArchiveManager())
            {
                TemporaryDirectoryPath = manager.CreateUniqueDirectory();
            }
            // Build a new database file and structure.
            using (DatabaseRepository repo = new DatabaseRepository())
            {
                repo.CreateNewDatabase(TemporaryDirectoryPath, "WinterEngineDB", true);
            }

            // Add the module details to the correct table.
            using (ModuleRepository repo = new ModuleRepository())
            {
                GameModule gameModule = new GameModule();
                gameModule.ModuleName = ModuleName;
                gameModule.ModuleTag = ModuleTag;

                repo.Add(gameModule);
            }

            InitializeData();
            LoadResourcePacks();
        }

        /// <summary>
        /// Saves the module using a new path.
        /// </summary>
        /// <param name="path"></param>
        public void SaveModule(string path)
        {
            using (FileArchiveManager manager = new FileArchiveManager())
            {
                // Update the path to the module file
                if (!String.IsNullOrEmpty(path))
                {
                    ModulePath = path;
                }
                string backupPath = manager.GenerateUniqueFileName(ModulePath);

                // Make a back up of the module file just in case something goes wrong.
                if (File.Exists(ModulePath))
                {
                    File.Copy(ModulePath, backupPath);
                }

                File.Delete(ModulePath);
                manager.ArchiveDirectory(TemporaryDirectoryPath, ModulePath);

                // Delete the backup since the new save was successful.
                File.Delete(backupPath);

                _moduleSavedMethod();
            }
        }

        /// <summary>
        /// Saves the module using the existing path.
        /// </summary>
        public void SaveModule()
        {
            SaveModule(ModulePath);
        }

        /// <summary>
        /// Handles prompting user for choosing a file and then handles opening it.
        /// </summary>
        /// <param name="path"></param>
        public void OpenModule(string path)
        {
            WinterConnectionInformation.ActiveModuleDirectoryPath = path;
            ModulePath = path;

            using(FileArchiveManager manager = new FileArchiveManager())
            {
                TemporaryDirectoryPath = manager.CreateUniqueDirectory();
                // Extract all files contained in the module zip file to the temporary directory.
                manager.ExtractArchive(ModulePath, TemporaryDirectoryPath);
            }

            FileHelper fileHelper = new FileHelper();
            string databaseFilePath = fileHelper.GetDatabaseFileInDirectory(TemporaryDirectoryPath);

            // Change the database connection to the file located in the extracted module folder.
            using (DatabaseRepository repo = new DatabaseRepository())
            {
                repo.ChangeDatabaseConnection(databaseFilePath);
            }

            if(CheckForMissingContentPackages())
            {
                _moduleOpenedMethod();
            }
        }

        /// <summary>
        /// Closes the active module, deleting the temporary directory.
        /// </summary>
        public void CloseModule()
        {
            // Delete the directory, if it exists
            if (Directory.Exists(TemporaryDirectoryPath))
            {
                Directory.Delete(TemporaryDirectoryPath, true);
            }

            // Reset object properties for next use.
            this.ModulePath = "";
            this.TemporaryDirectoryPath = "";

            _moduleClosedMethod();
        }

        /// <summary>
        /// Checks the ContentPackages directory for the required content packs that the module uses.
        /// If any of them are missing, a pop-up window will display and this method will return false.
        /// </summary>
        /// <returns></returns>
        public bool CheckForMissingContentPackages()
        {
            List<string> missingContentPackages;
            List<string> fileContentPackages;
            List<string> moduleContentPackages;

            // Retrieve the existing content packages (ones which are in the ContentPackages directory)
            using (ContentPackageFileRepository manager = new ContentPackageFileRepository())
            {
                fileContentPackages = manager.GetAllContentPackageFileNames();
            }

            // Retrieve the required content packages (ones which are attached to the module)
            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                moduleContentPackages = repo.GetAllFileNames();
            }

            // Determine which content packages do not exist on disk that are required by this module.
            missingContentPackages = moduleContentPackages.Except(fileContentPackages).ToList();

            if (missingContentPackages.Count > 0)
            {
                string errorMessage = "Unable to locate the following content packages:\n\n";

                foreach (string current in missingContentPackages)
                {
                    errorMessage += current + "\n";
                }

                errorMessage += "\nPlease place the missing content packages in the ContentPacks folder and try again.";

                MessageBox.Show(errorMessage, "Missing Content Packages", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Module Initialization Methods

        /// <summary>
        /// Handles loading the standardized resource pack files into the database for the module.
        /// These are graphic files for each resource type.
        /// </summary>
        private void LoadResourcePacks()
        {
        }

        /// <summary>
        /// Handles setting up system data in the module's database.
        /// These are core pieces of data needed to ensure everything runs correctly.
        /// </summary>
        private void InitializeData()
        {
            // Add the "Uncategorized" category for each resource type.
            using (CategoryRepository repo = new CategoryRepository())
            {
                Category category = new Category { VisibleName = "*Uncategorized", GameObjectType = GameObjectTypeEnum.Area, IsSystemResource = true };
                repo.Add(category);
                category.GameObjectType = GameObjectTypeEnum.Conversation;
                repo.Add(category);
                category.GameObjectType = GameObjectTypeEnum.Creature;
                repo.Add(category);
                category.GameObjectType = GameObjectTypeEnum.Item;
                repo.Add(category);
                category.GameObjectType = GameObjectTypeEnum.Placeable;
                repo.Add(category);
                category.GameObjectType = GameObjectTypeEnum.Script;
                repo.Add(category);
            }
        }

        #endregion

    }
}
