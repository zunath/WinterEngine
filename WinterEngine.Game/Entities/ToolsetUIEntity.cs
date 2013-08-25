using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Awesomium.Core;
using FlatRedBall;
using Newtonsoft.Json;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.ViewModels;
using WinterEngine.Editor.Managers;
using WinterEngine.Library.Managers;
using WinterEngine.Library.Utility;


namespace WinterEngine.Game.Entities
{
	public partial class ToolsetUIEntity
    {
        #region Fields

        private FileExtensionFactory _extensionFactory;
        private ModuleManager _moduleManager;

        #endregion

        #region Properties

        private FileExtensionFactory ExtensionFactory 
        {
            get
            {
                if (_extensionFactory == null)
                {
                    _extensionFactory = new FileExtensionFactory();
                }

                return _extensionFactory;
            }
        }

        private ModuleManager ModuleManager
        {
            get
            {
                if (_moduleManager == null)
                {
                    _moduleManager = new ModuleManager();
                }

                return _moduleManager;
            }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<ObjectSelectionEventArgs> OnAreaLoaded;

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
        {
            AwesomiumWebView.DocumentReady += OnDocumentReady;
		}

		private void CustomActivity()
		{
		}

		private void CustomDestroy()
		{
            ModuleManager.CloseModule();
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {
        }

        #endregion

        #region Awesomium Event Handling

        private void OnDocumentReady(object sender, EventArgs e)
        {
            AwesomiumWebView.DocumentReady -= OnDocumentReady;

            // File Menu Bindings

            EntityJavascriptObject.Bind("NewModuleButtonClick", false, NewModuleButton);
            EntityJavascriptObject.Bind("OpenModuleButtonClick", false, OpenModuleButton);
            EntityJavascriptObject.Bind("CloseModuleButtonClick", false, CloseModuleButton);
            EntityJavascriptObject.Bind("SaveModuleButtonClick", false, SaveModuleButton);
            EntityJavascriptObject.Bind("SaveAsModuleButtonClick", false, SaveAsModuleButton);
            EntityJavascriptObject.Bind("ExitButtonClick", false, ExitButton);

            // Edit Menu Bindings

            // Content Menu Bindings
            EntityJavascriptObject.Bind("BuildModuleButtonClick", false, BuildModuleButton);
            EntityJavascriptObject.Bind("ManageContentPackagesButtonClick", false, ManageContentPackagesButton);

            // Help Menu Bindings
            EntityJavascriptObject.Bind("WinterEngineWebsiteButtonClick", false, WinterEngineWebsiteButton);

            // Treeview Bindings
            EntityJavascriptObject.Bind("LoadTreeViewData", false, LoadTreeViewData);
            EntityJavascriptObject.Bind("AddNewObject", false, AddNewObject);
            EntityJavascriptObject.Bind("AddNewCategory", false, AddNewCategory);
            EntityJavascriptObject.Bind("DeleteCategory", false, DeleteCategory);
            EntityJavascriptObject.Bind("DeleteObject", false, DeleteObject);
            EntityJavascriptObject.Bind("RenameCategory", false, RenameCategory);
            EntityJavascriptObject.Bind("RenameObject", false, RenameObject);

            // Data Manipulation Bindings 
            EntityJavascriptObject.Bind("GetModelJSON", true, GetModelJSON);
            EntityJavascriptObject.Bind("SaveObjectData", false, SaveObjectData);
            EntityJavascriptObject.Bind("LoadObjectData", false, LoadObjectData);
            EntityJavascriptObject.Bind("GetModulesList", true, GetModulesList);

            RunJavaScriptMethod("Initialize();");
        }

        #endregion

        #region General Methods

        private void SaveModule()
        {
            ModuleManager.SaveModule();
        }

        #endregion

        #region UI Methods - File Menu Bindings

        private void NewModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            ModuleManager.ModuleName = e.Arguments[0];
            ModuleManager.ModuleTag = e.Arguments[1];
            bool success = ModuleManager.CreateModule();

            AsyncJavascriptCallback("NewModuleBoxOKClick_Callback", success);
        }

        private void OpenModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                string filePath = DirectoryPaths.ModuleDirectoryPath + e.Arguments[0] + ExtensionFactory.GetFileExtension(FileTypeEnum.Module);
                ModuleManager.OpenModule(filePath);
                AsyncJavascriptCallback("OpenModuleButtonClick_Callback", true);

            }
            catch
            {
                AsyncJavascriptCallback("OpenModuleButtonClick_Callback", false);
                throw;
            }
        }

        private void SaveModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ModuleManager.ModulePath))
            {
                AsyncJavascriptCallback("ShowSaveAsModulePopUp");
            }
            else
            {
                SaveModule();
            }
        }

        private void SaveAsModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            SaveAsResponseTypeEnum response = SaveAsResponseTypeEnum.SaveFailed;
            string filePath = DirectoryPaths.ModuleDirectoryPath + e.Arguments[0] 
                + ExtensionFactory.GetFileExtension(FileTypeEnum.Module);
            bool forceOverwrite = e.Arguments[1];

            if (File.Exists(filePath) && !forceOverwrite)
            {
                response = SaveAsResponseTypeEnum.FileNameAlreadyExists;
            }
            else
            {
                ModuleManager.ModulePath = filePath;
                SaveModule();
                response = SaveAsResponseTypeEnum.SaveSuccessful;
            }

            AsyncJavascriptCallback("SaveAsModuleButtonClick_Callback", (int)response, e.Arguments[0]);
        }

        private void CloseModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            ModuleManager.CloseModule();
            AsyncJavascriptCallback("CloseModuleButtonClick_Callback");
        }

        private void ExitButton(object sender, JavascriptMethodEventArgs e)
        {
            FlatRedBallServices.Game.Exit();
        }

        #endregion

        #region UI Methods - Edit Menu Bindings

        #endregion

        #region UI Methods - Content Menu Bindings

        private void BuildModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            bool success = false;
            string callbackException = "";

            try
            {
                GameResourceManager.RebuildModule();
                success = true;
            }
            catch(Exception ex)
            {
                success = false;
                callbackException = ex.StackTrace;
            }

            AsyncJavascriptCallback("BuildModuleButtonClick_Callback", success, callbackException);
        }

        private void ManageContentPackagesButton(object sender, JavascriptMethodEventArgs e)
        {
            List<ContentPackage> attachedContentPackages;
            List<ContentPackage> availableContentPackages = new List<ContentPackage>();

            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                attachedContentPackages = repo.GetAll();
            }

            string[] files = Directory.GetFiles(DirectoryPaths.ContentPackageDirectoryPath, "*" + ExtensionFactory.GetFileExtension(FileTypeEnum.ContentPackage));
            foreach (string currentPackage in files)
            {
                ContentPackage package = new ContentPackage
                {
                    FileName = Path.GetFileName(currentPackage),
                    ResourceType = ResourceTypeEnum.ContentPackage,
                    Name = Path.GetFileNameWithoutExtension(currentPackage)
                };

                availableContentPackages.Add(package);
            }

            string jsonAttachedContentPackages = JsonConvert.SerializeObject(attachedContentPackages);
            string jsonAvailableContentPackages = JsonConvert.SerializeObject(availableContentPackages);

            AsyncJavascriptCallback("ManageContentPackagesButton_Callback", jsonAttachedContentPackages, jsonAvailableContentPackages);
        }

        #endregion

        #region UI Methods - Help Menu Bindings

        private void WinterEngineWebsiteButton(object sender, JavascriptMethodEventArgs e)
        {
            Process.Start("https://www.winterengine.com");
        }


        #endregion

        #region UI Methods - Tree Views

        private void LoadTreeViewData(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                JSTreeNode areaRootNode;
                JSTreeNode creatureRootNode;
                JSTreeNode itemRootNode;
                JSTreeNode placeableRootNode;
                JSTreeNode conversationRootNode;
                JSTreeNode scriptRootNode;

                // Get each category's children for each object type
                using (AreaRepository repo = new AreaRepository())
                {
                    areaRootNode = repo.GenerateJSTreeHierarchy();
                }
                using (CreatureRepository repo = new CreatureRepository())
                {
                    creatureRootNode = repo.GenerateJSTreeHierarchy();
                }
                using (ItemRepository repo = new ItemRepository())
                {
                    itemRootNode = repo.GenerateJSTreeHierarchy();
                }
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    placeableRootNode = repo.GenerateJSTreeHierarchy();
                }
                using (ConversationRepository repo = new ConversationRepository())
                {
                    conversationRootNode = repo.GenerateJSTreeHierarchy();
                }
                using (ScriptRepository repo = new ScriptRepository())
                {
                    scriptRootNode = repo.GenerateJSTreeHierarchy();
                }

                
                AsyncJavascriptCallback("LoadTreeViews_Callback",
                    JsonConvert.SerializeObject(areaRootNode),
                    JsonConvert.SerializeObject(creatureRootNode),
                    JsonConvert.SerializeObject(itemRootNode),
                    JsonConvert.SerializeObject(placeableRootNode),
                    JsonConvert.SerializeObject(conversationRootNode),
                    JsonConvert.SerializeObject(scriptRootNode));
            }
            catch
            {
                throw;
            }
        }

        private void AddNewCategory(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            string name = e.Arguments[0];
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[1]);
            Category newCategory = new Category
            {
                IsSystemResource = false,
                Name = name,
                GameObjectType = gameObjectType
            };

            using (CategoryRepository repo = new CategoryRepository())
            {
                newCategory = repo.Add(newCategory);
            }

            AsyncJavascriptCallback("CreateNewCategory_Callback",
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error),
                name,
                newCategory.ResourceID);
        }

        private void AddNewObject(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                ErrorTypeEnum error = ErrorTypeEnum.None;
                GameObjectFactory factory = new GameObjectFactory();
                string name = e.Arguments[0];
                string tag = e.Arguments[1];
                string resref = e.Arguments[2];
                int categoryID = (int)e.Arguments[3];
                GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[4]);
                int resourceID = 0;

                if (factory.DoesObjectExistInDatabase(resref, gameObjectType))
                {
                    error = ErrorTypeEnum.ObjectResrefAlreadyExists;
                }
                else
                {
                    GameObjectBase newObject = factory.CreateObject(gameObjectType);
                    newObject.Name = name;
                    newObject.Tag = tag;
                    newObject.Resref = resref;
                    newObject.ResourceCategoryID = categoryID;

                    resourceID = factory.AddToDatabase(newObject).ResourceID;
                }

                AsyncJavascriptCallback("CreateNewObject_Callback",
                    error == ErrorTypeEnum.None ? true : false,
                    EnumerationHelper.GetEnumerationDescription(error),
                    (int)gameObjectType,
                    name,
                    resourceID);
            }
            catch
            {
                throw;
            }
        }

        private void DeleteCategory(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            int categoryID = (int)e.Arguments[0];
            GameObjectFactory factory = new GameObjectFactory();
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[1]);
            Category categoryToRemove;

            using(CategoryRepository repo = new CategoryRepository())
            {
                categoryToRemove = repo.GetByID(categoryID);
            }

            factory.DeleteFromDatabaseByCategory(categoryToRemove, gameObjectType);

            AsyncJavascriptCallback("DeleteObject_Callback",
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error));
        }

        private void DeleteObject(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            GameObjectFactory factory = new GameObjectFactory();
            int resourceID = (int)e.Arguments[0];
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[1]);
            
            factory.DeleteFromDatabase(resourceID, gameObjectType);
            
            AsyncJavascriptCallback("DeleteObject_Callback", 
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error));
        }

        private void RenameCategory(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            string name = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            
            using (CategoryRepository repo = new CategoryRepository())
            {
                Category dbCategory = repo.GetByID(categoryID);
                if (!dbCategory.IsSystemResource)
                {
                    dbCategory.Name = name;
                }
                else
                {
                    error = ErrorTypeEnum.CannotChangeSystemResource;
                }
            }

            AsyncJavascriptCallback("RenameObject_Callback",
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error),
                name);
        }

        private void RenameObject(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            GameObjectFactory factory = new GameObjectFactory();
            string name = e.Arguments[0];
            int resourceID = (int)e.Arguments[1];
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[2]);

            GameObjectBase dbObject = factory.GetFromDatabaseByID(resourceID, gameObjectType);

            if (dbObject == null)
            {
                error = ErrorTypeEnum.ObjectResrefDoesNotExist;
            }
            else
            {
                if (!dbObject.IsSystemResource)
                {
                    dbObject.Name = name;
                    factory.UpdateInDatabase(dbObject);
                }
                else
                {
                    error = ErrorTypeEnum.CannotChangeSystemResource;
                }
            }

            AsyncJavascriptCallback("RenameObject_Callback",
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error),
                name);
        }

        #endregion

        #region UI Methods - Data Manipulation

        /// <summary>
        /// Sends a JSON result to the UI containing model information.
        /// This lets us get away with defining the model one time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetModelJSON(object sender, JavascriptMethodEventArgs e)
        {
            e.Result = JsonConvert.SerializeObject(new ToolsetViewModel());
        }

        private void LoadObjectData(object sender, JavascriptMethodEventArgs e)
        {
            GameObjectFactory factory = new GameObjectFactory();
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[0]);
            int resourceID = (int)e.Arguments[1];
            GameObjectBase gameObject = factory.GetFromDatabaseByID(resourceID, gameObjectType);
            string jsonObject = JsonConvert.SerializeObject(gameObject);
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);

            if (gameObjectType == GameObjectTypeEnum.Area)
            {
                if (!Object.ReferenceEquals(OnAreaLoaded, null))
                {
                    OnAreaLoaded(this, eventArgs);
                }
            }

            AsyncJavascriptCallback("LoadObjectData_Callback", jsonObject);
        }

        private void SaveObjectData(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                GameObjectFactory factory = new GameObjectFactory();
                GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[0]);
                string jsonModel = e.Arguments[1];
                ToolsetViewModel model = JsonConvert.DeserializeObject<ToolsetViewModel>(jsonModel, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                if (gameObjectType == GameObjectTypeEnum.Area)
                {
                    using (AreaRepository repo = new AreaRepository())
                    {
                        repo.Upsert(model.ActiveArea);
                    }
                }
                else if (gameObjectType == GameObjectTypeEnum.Conversation)
                {

                }
                else if (gameObjectType == GameObjectTypeEnum.Creature)
                {
                }
                else if (gameObjectType == GameObjectTypeEnum.Item)
                {
                }
                else if (gameObjectType == GameObjectTypeEnum.Placeable)
                {
                }
                else if (gameObjectType == GameObjectTypeEnum.Script)
                {
                }
            }
            catch
            {
                throw;
            }
        }

        private void GetModulesList(object sender, JavascriptMethodEventArgs e)
        {
            string[] files = Directory.GetFiles(DirectoryPaths.ModuleDirectoryPath, "*"
                + ExtensionFactory.GetFileExtension(FileTypeEnum.Module));
            List<GameModule> moduleList = new List<GameModule>();
            foreach (string current in files)
            {
                GameModule module = new GameModule
                {
                    FileName = Path.GetFileNameWithoutExtension(current)
                };
                moduleList.Add(module);
            }

            e.Result = JsonConvert.SerializeObject(moduleList);
        }

        #endregion

    }
}
