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
using WinterEngine.Library.Extensions;
using WinterEngine.Editor.Managers;
using WinterEngine.Library.Managers;
using WinterEngine.Library.Utility;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Converters;
using WinterEngine.DataTransferObjects.UIObjects;
using System.Linq;
using System.Threading.Tasks;


namespace WinterEngine.Game.Entities
{
	public partial class ToolsetUIEntity
    {
        #region Fields

        private ToolsetViewModel _viewModel;
        private FileExtensionFactory _extensionFactory;
        private ModuleManager _moduleManager;
        private JsonSerializerSettings _serializerSettings;
        
        #endregion

        #region Properties

        private ToolsetViewModel ViewModel 
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new ToolsetViewModel();
                }
                return _viewModel;
            }
            set
            {
                _viewModel = new ToolsetViewModel();

            }
        }

        private JsonSerializerSettings JSONSerializerSettings
        {
            get
            {
                if (_serializerSettings == null)
                {
                    _serializerSettings = new JsonSerializerSettings 
                    { 
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    };
                }
                return _serializerSettings;
            }
        }

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

        public event EventHandler<ObjectModeChangedEventArgs> OnObjectModeChanged;

        // Area Editor Events
        public event EventHandler<ObjectSelectionEventArgs> OnAreaLoaded;

        // Tileset Editor Events
        public event EventHandler<TilesetSelectionEventArgs> OnTilesetLoaded;

        // Object save events
        public event EventHandler<GameObjectSaveEventArgs> OnSaveArea;
        public event EventHandler<GameObjectSaveEventArgs> OnSaveCreature;
        public event EventHandler<GameObjectSaveEventArgs> OnSaveItem;
        public event EventHandler<GameObjectSaveEventArgs> OnSavePlaceable;
        public event EventHandler<GameObjectSaveEventArgs> OnSaveConversation;
        public event EventHandler<GameObjectSaveEventArgs> OnSaveScript;
        public event EventHandler<GameObjectSaveEventArgs> OnSaveTileset;
        

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

            EntityJavascriptObject.Bind("NewModuleButtonClick", false, NewModuleButtonAsync);
            EntityJavascriptObject.Bind("OpenModuleButtonClick", false, OpenModuleButtonAsync);
            EntityJavascriptObject.Bind("CloseModuleButtonClick", false, CloseModuleButtonAsync);
            EntityJavascriptObject.Bind("SaveModuleButtonClick", false, SaveModuleButton);
            EntityJavascriptObject.Bind("SaveAsModuleButtonClick", false, SaveAsModuleButton);
            EntityJavascriptObject.Bind("ExitButtonClick", false, ExitButton);

            // Edit Menu Bindings
            EntityJavascriptObject.Bind("SaveModuleProperties", false, SaveModulePropertiesAsync);

            // Object Mode Bindings
            EntityJavascriptObject.Bind("ChangeObjectMode", false, ChangeObjectMode);

            // Content Menu Bindings
            EntityJavascriptObject.Bind("BuildModuleButtonClick", false, BuildModuleButton);
            EntityJavascriptObject.Bind("ManageContentPackagesButtonClick", false, ManageContentPackagesButton);
            EntityJavascriptObject.Bind("UpdateContentPackages", false, UpdateContentPackages);

            // Help Menu Bindings
            EntityJavascriptObject.Bind("WinterEngineWebsiteButtonClick", false, WinterEngineWebsiteButton);

            // Treeview Bindings
            EntityJavascriptObject.Bind("LoadTreeViewData", false, LoadTreeViewData);
            EntityJavascriptObject.Bind("AddNewObject", false, AddNewObject);
            EntityJavascriptObject.Bind("AddNewCategory", false, AddNewCategory);
            EntityJavascriptObject.Bind("DeleteCategory", false, DeleteCategory);
            EntityJavascriptObject.Bind("DeleteObject", false, DeleteObject);
            EntityJavascriptObject.Bind("RenameCategory", false, RenameCategory);

            // Data Manipulation Bindings 
            EntityJavascriptObject.Bind("GetModelJSON", true, GetModelJSON);

            EntityJavascriptObject.Bind("SaveArea", false, SaveArea);
            EntityJavascriptObject.Bind("SaveCreature", false, SaveCreature);
            EntityJavascriptObject.Bind("SaveItem", false, SaveItem);
            EntityJavascriptObject.Bind("SavePlaceable", false, SavePlaceable);
            EntityJavascriptObject.Bind("SaveConversation", false, SaveConversation);
            EntityJavascriptObject.Bind("SaveScript", false, SaveScript);
            EntityJavascriptObject.Bind("SaveTileset", false, SaveTileset);

            EntityJavascriptObject.Bind("LoadObjectData", false, LoadObjectData);
            EntityJavascriptObject.Bind("GetModulesList", true, GetModulesList);
            
            // Tileset Editor Bindings
            EntityJavascriptObject.Bind("LoadTilesetSpritesheet", false, LoadTilesetSpritesheet);

            RunJavaScriptMethod("Initialize();");
        }

        #endregion

        #region General Methods

        private async void SaveModuleAsync()
        {
            await TaskEx.Run(() =>
            {
                ModuleManager.SaveModule();
            });
        }

        #endregion

        #region UI Methods - File Menu Bindings

        private async void NewModuleButtonAsync(object sender, JavascriptMethodEventArgs e)
        {
            bool success = false;

            await TaskEx.Run(() =>
            {
                ModuleManager.ModuleName = e.Arguments[0];
                ModuleManager.ModuleTag = e.Arguments[1];
                ModuleManager.ModuleResref = e.Arguments[2];
                success = ModuleManager.CreateModule();
                PopulateToolsetViewModel();
                ViewModel.IsModuleOpened = success;
            });
            AsyncJavascriptCallback("NewModuleBoxOKClick_Callback", success);
        }

        private async void OpenModuleButtonAsync(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                await TaskEx.Run(() =>
                {
                    string filePath = DirectoryPaths.ModuleDirectoryPath + e.Arguments[0] + ExtensionFactory.GetFileExtension(FileTypeEnum.Module);
                    ModuleManager.OpenModule(filePath);
                    PopulateToolsetViewModel();
                    ViewModel.IsModuleOpened = true;
                });
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
                SaveModuleAsync();
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
                SaveModuleAsync();
                response = SaveAsResponseTypeEnum.SaveSuccessful;
            }

            AsyncJavascriptCallback("SaveAsModuleButtonClick_Callback", (int)response, e.Arguments[0]);
        }

        private async void CloseModuleButtonAsync(object sender, JavascriptMethodEventArgs e)
        {
            await TaskEx.Run(() =>
            {
                ModuleManager.CloseModule();
                ClearViewModelPopulation();
                ViewModel.IsModuleOpened = false;
            });
            AsyncJavascriptCallback("CloseModuleButtonClick_Callback");
        }

        private void ExitButton(object sender, JavascriptMethodEventArgs e)
        {
            FlatRedBallServices.Game.Exit();
        }

        #endregion

        #region UI Methods - Edit Menu Bindings

        private async void SaveModulePropertiesAsync(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                await TaskEx.Run(() =>
                {
                    GameModule updatedModule = JsonConvert.DeserializeObject<GameModule>(e.Arguments[0]);

                    using (GameModuleRepository repo = new GameModuleRepository())
                    {
                        repo.Update(updatedModule);
                    }

                    PopulateToolsetViewModel();
                });

                AsyncJavascriptCallback("SaveModuleProperties_Callback");
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving module properties", ex);
            }
        }

        #endregion

        #region UI Methods - Content Menu Bindings

        private void BuildModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            bool success = false;
            string callbackException = "";

            try
            {
                GameResourceManager.RebuildModule(ModuleRebuildModeEnum.UserResourcesOnly);
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
                attachedContentPackages = (from package
                                           in repo.GetAll()
                                           where package.IsSystemResource == false
                                           select package).ToList();

                // We don't need to send the resource list to the GUI because it could contain a lot of data.
                // We'll pick up the resource list when we go to do a save/rebuild of the module.
                attachedContentPackages.ForEach(a => a.ResourceList = null);
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

            ViewModel.AvailableContentPackages = availableContentPackages;
            ViewModel.AttachedContentPackages = attachedContentPackages;

            AsyncJavascriptCallback("ManageContentPackagesButton_Callback");
        }

        private void UpdateContentPackages(object sender, JavascriptMethodEventArgs e)
        {
            string jsonUpdatedContentPackages = e.Arguments[0];
            List<ContentPackage> contentPackageList = JsonConvert.DeserializeObject<List<ContentPackage>>(jsonUpdatedContentPackages);
            GameResourceManager.RebuildModule(contentPackageList, ModuleRebuildModeEnum.UserResourcesOnly);
            PopulateToolsetViewModel();

            AsyncJavascriptCallback("ManageContentPackagesSaveChanges_Callback");
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
                JSTreeNode tilesetRootNode;

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
                using (TilesetRepository repo = new TilesetRepository())
                {
                    tilesetRootNode = repo.GenerateJSTreeHierarchy();
                }
                
                AsyncJavascriptCallback("LoadTreeViews_Callback",
                    JsonConvert.SerializeObject(areaRootNode),
                    JsonConvert.SerializeObject(creatureRootNode),
                    JsonConvert.SerializeObject(itemRootNode),
                    JsonConvert.SerializeObject(placeableRootNode),
                    JsonConvert.SerializeObject(conversationRootNode),
                    JsonConvert.SerializeObject(scriptRootNode),
                    JsonConvert.SerializeObject(tilesetRootNode));
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
                    GameObjectBase newObject = factory.CreateObject(gameObjectType, name, tag, resref);
                    newObject.ResourceCategoryID = categoryID;

                    resourceID = factory.AddToDatabase(newObject).ResourceID;
                }

                PopulateToolsetViewModel();

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
        
            if (gameObjectType == GameObjectTypeEnum.Area)
            {
                RefreshAreaEntity(this, new ObjectSelectionEventArgs(0));
            }

            PopulateToolsetViewModel();

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

            if (gameObjectType == GameObjectTypeEnum.Area)
            {
                ViewModel.ActiveArea = new Area(true);
                RefreshAreaEntity(this, new ObjectSelectionEventArgs(0));
            }
            else if (gameObjectType == GameObjectTypeEnum.Conversation)
            {
                ViewModel.ActiveConversation = new Conversation(true);
            }
            else if (gameObjectType == GameObjectTypeEnum.Creature)
            {
                ViewModel.ActiveCreature = new Creature(true);
            }
            else if (gameObjectType == GameObjectTypeEnum.Item)
            {
                ViewModel.ActiveItem = new Item(true);
            }
            else if (gameObjectType == GameObjectTypeEnum.Placeable)
            {
                ViewModel.ActivePlaceable = new Placeable(true);
            }
            else if (gameObjectType == GameObjectTypeEnum.Script)
            {
                ViewModel.ActiveScript = new Script();
            }
            else if (gameObjectType == GameObjectTypeEnum.Tileset)
            {
                ViewModel.ActiveTileset = new Tileset();
            }

            PopulateToolsetViewModel();

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
            e.Result = JsonConvert.SerializeObject(ViewModel);
        }

        private void LoadObjectData(object sender, JavascriptMethodEventArgs e)
        {
            GameObjectFactory factory = new GameObjectFactory();
            int resourceID = (int)e.Arguments[0];
            GameObjectBase gameObject = factory.GetFromDatabaseByID(resourceID, ViewModel.GameObjectType);
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);

            if (ViewModel.GameObjectType == GameObjectTypeEnum.Area)
            {
                ViewModel.ActiveArea = gameObject as Area;
                RefreshAreaEntity(this, eventArgs);
            }
            else if (ViewModel.GameObjectType == GameObjectTypeEnum.Conversation)
            {
                ViewModel.ActiveConversation = gameObject as Conversation;
            }
            else if (ViewModel.GameObjectType == GameObjectTypeEnum.Creature)
            {
                ViewModel.ActiveCreature = gameObject as Creature;
            }
            else if (ViewModel.GameObjectType == GameObjectTypeEnum.Item)
            {
                ViewModel.ActiveItem = gameObject as Item;
            }
            else if (ViewModel.GameObjectType == GameObjectTypeEnum.Placeable)
            {
                ViewModel.ActivePlaceable = gameObject as Placeable;
            }
            else if (ViewModel.GameObjectType == GameObjectTypeEnum.Script)
            {
                ViewModel.ActiveScript = gameObject as Script;
            }
            else if (ViewModel.GameObjectType == GameObjectTypeEnum.Tileset)
            {
                ViewModel.ActiveTileset = gameObject as Tileset;
                RaiseTilesetLoadEvent(gameObject.GraphicResourceID);
            }

            AsyncJavascriptCallback("LoadObjectData_Callback");
        }

        private void RefreshAreaEntity(object sender, ObjectSelectionEventArgs e)
        {
            if (!Object.ReferenceEquals(OnAreaLoaded, null))
            {
                OnAreaLoaded(sender, e);
            }
        }

        private void SaveArea(object sender, JavascriptMethodEventArgs e)
        {
            if (OnSaveArea != null)
            {
                ViewModel.ActiveArea = JsonConvert.DeserializeObject<Area>(e.Arguments[0]);
                OnSaveArea(this, new GameObjectSaveEventArgs(ViewModel.ActiveArea));
                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
            }
        }

        private void SaveCreature(object sender, JavascriptMethodEventArgs e)
        {
            if (OnSaveCreature != null)
            {
                ViewModel.ActiveCreature = JsonConvert.DeserializeObject<Creature>(e.Arguments[0]);
                OnSaveCreature(this, new GameObjectSaveEventArgs(ViewModel.ActiveCreature));
                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
            }
        }

        private void SaveItem(object sender, JavascriptMethodEventArgs e)
        {
            if (OnSaveItem != null)
            {
                ViewModel.ActiveItem = JsonConvert.DeserializeObject<Item>(e.Arguments[0]);
                OnSaveItem(this, new GameObjectSaveEventArgs(ViewModel.ActiveItem));
                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
            }
        }

        private void SavePlaceable(object sender, JavascriptMethodEventArgs e)
        {
            if (OnSavePlaceable != null)
            {
                ViewModel.ActivePlaceable = JsonConvert.DeserializeObject<Placeable>(e.Arguments[0]);
                OnSavePlaceable(this, new GameObjectSaveEventArgs(ViewModel.ActivePlaceable));
                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
            }
        }

        private void SaveConversation(object sender, JavascriptMethodEventArgs e)
        {
            if (OnSaveConversation != null)
            {
                ViewModel.ActiveConversation = JsonConvert.DeserializeObject<Conversation>(e.Arguments[0]);
                OnSaveConversation(this, new GameObjectSaveEventArgs(ViewModel.ActiveConversation));
                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
            }
        }

        private void SaveScript(object sender, JavascriptMethodEventArgs e)
        {
            if (OnSaveScript != null)
            {
                ViewModel.ActiveScript = JsonConvert.DeserializeObject<Script>(e.Arguments[0]);
                OnSaveScript(this, new GameObjectSaveEventArgs(ViewModel.ActiveScript));
                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
            }
        }

        private void SaveTileset(object sender, JavascriptMethodEventArgs e)
        {
            if (OnSaveTileset != null)
            {
                ViewModel.ActiveTileset = JsonConvert.DeserializeObject<Tileset>(e.Arguments[0]);
                OnSaveTileset(this, new GameObjectSaveEventArgs(ViewModel.ActiveTileset));
                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
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

            ViewModel.ModuleList = moduleList;
        }

        private void PopulateToolsetViewModel()
        {
            ClearViewModelPopulation();

            using (GameModuleRepository repo = new GameModuleRepository())
            {
                ViewModel.ActiveModule = repo.GetModule();
            }

            using (ContentPackageResourceRepository repo = new ContentPackageResourceRepository())
            {
                ViewModel.TilesetSpriteSheetsList = repo.GetAllUIObjects(ContentPackageResourceTypeEnum.Tileset, false);
            }

            using (ItemRepository repo = new ItemRepository())
            {
                ViewModel.ItemList = repo.GetAllUIObjects();
            }

            using (ScriptRepository repo = new ScriptRepository())
            {
                ViewModel.ScriptList = repo.GetAllUIObjects();
            }

            using (GenderRepository repo = new GenderRepository())
            {
                ViewModel.GenderList = repo.GetAllUIObjects();
            }

            using (ConversationRepository repo = new ConversationRepository())
            {
                ViewModel.ConversationList = repo.GetAllUIObjects();
            }

            using (RaceRepository repo = new RaceRepository())
            {
                ViewModel.RaceList = repo.GetAllUIObjects();
            }

            using (FactionRepository repo = new FactionRepository())
            {
                ViewModel.FactionList = repo.GetAllUIObjects();
            }

            using (TilesetRepository repo = new TilesetRepository())
            {
                ViewModel.TilesetList = repo.GetAllUIObjects();
            }

            using (AbilityRepository repo = new AbilityRepository())
            {
                ViewModel.AbilityList = repo.GetAll();
            }

            using (SkillRepository repo = new SkillRepository())
            {
                ViewModel.SkillList = repo.GetAll();
            }

            using (LevelRequirementRepository repo = new LevelRequirementRepository())
            {
                ViewModel.LevelRequirementList = repo.GetAll();
            }
        }

        private void ClearViewModelPopulation()
        {
            ViewModel.ModuleList.Clear();
            ViewModel.AvailableContentPackages.Clear();
            ViewModel.AttachedContentPackages.Clear();
            ViewModel.TilesetSpriteSheetsList.Clear();
            ViewModel.ItemList.Clear();
            ViewModel.ScriptList.Clear();
            ViewModel.GenderList.Clear();
            ViewModel.ConversationList.Clear();
            ViewModel.RaceList.Clear();
            ViewModel.FactionList.Clear();
            ViewModel.TilesetList.Clear();
            ViewModel.AbilityList.Clear();
            ViewModel.LevelRequirementList.Clear();
            ViewModel.SkillList.Clear();
        }

        private void ChangeObjectMode(object sender, JavascriptMethodEventArgs e)
        {
            ViewModel.CurrentObjectMode = e.Arguments[0];
            ViewModel.CurrentObjectTreeSelector = e.Arguments[1];
            string mode = e.Arguments[0];
            GameObjectTypeEnum gameObjectType = GameObjectTypeEnum.Unknown;
            Enum.TryParse(mode, true, out gameObjectType);

            // Inform subscribers (AKA: The screen) that the object mode has changed.
            if (OnObjectModeChanged != null)
            {
                OnObjectModeChanged(this, new ObjectModeChangedEventArgs(gameObjectType));
            }

            AsyncJavascriptCallback("ChangeObjectMode_Callback");
            
        }

        #endregion

        #region UI Methods - Tileset Editor

        public void LoadTilesetSpritesheet(object sender, JavascriptMethodEventArgs e)
        {
            int graphicResourceID = (int)e.Arguments[0];
            RaiseTilesetLoadEvent(graphicResourceID);
        }

        private void RaiseTilesetLoadEvent(int graphicResourceID)
        {
            if (OnTilesetLoaded != null)
            {
                OnTilesetLoaded(this, new TilesetSelectionEventArgs(ViewModel.ActiveTileset.ResourceID, graphicResourceID));
            }
        }

        #endregion

    }
}
