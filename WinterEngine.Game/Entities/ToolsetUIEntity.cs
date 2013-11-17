using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using WinterEngine.Library.Managers;
using WinterEngine.Editor.Utility;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Converters;
using WinterEngine.DataTransferObjects.UIObjects;
using System.Linq;
using Ninject;


namespace WinterEngine.Game.Entities
{
	public partial class ToolsetUIEntity
    {
        #region Fields

        public ToolsetViewModel _viewModel { get; set; }
        public FileExtensionFactory _extensionFactory { get; set; }
        public JsonSerializerSettings _serializerSettings { get; set; }
        
        [Inject]
        public ModuleManager _moduleManager { get; set; }
        [Inject]
        public IRepositoryFactory _repositoryFactory { get; set; }
        [Inject]
        public IGameObjectFactory _gameObjectFactory { get; set; }
        [Inject]
        public IGameResourceManager _resourceManager { get; set; }
        [Inject]
        public IUITreeObjectRepository _uiTreeObjectRepository { get; set; }
        [Inject]
        public IGameModuleRepository _gameModuleRepository { get; set; }
        [Inject]
        public IUIObjectRepository _uiObjectRepository { get; set; }

        #endregion

        #region Properties

        private ToolsetViewModel ViewModel 
        {
            get
            {

                return _viewModel;
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
            _moduleManager.CloseModule();
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
            EntityJavascriptObject.Bind("SaveModuleProperties", false, SaveModuleProperties);

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

            // Data Manipulation Bindings 
            EntityJavascriptObject.Bind("GetModelJSON", true, GetModelJSON);

            EntityJavascriptObject.Bind("SaveArea", false, SaveArea);
            EntityJavascriptObject.Bind("SaveCreature", false, SaveCreature);
            EntityJavascriptObject.Bind("SaveItem", false, SaveItem);
            EntityJavascriptObject.Bind("SavePlaceable", false, SavePlaceable);
            EntityJavascriptObject.Bind("SaveConversation", false, SaveConversation);
            EntityJavascriptObject.Bind("SaveScript", false, SaveScript);
            EntityJavascriptObject.Bind("SaveTileset", false, SaveTileset);
            EntityJavascriptObject.Bind("SaveCategory", false, SaveCategory);

            EntityJavascriptObject.Bind("LoadArea", false, LoadArea);
            EntityJavascriptObject.Bind("LoadCreature", false, LoadCreature);
            EntityJavascriptObject.Bind("LoadItem", false, LoadItem);
            EntityJavascriptObject.Bind("LoadPlaceable", false, LoadPlaceable);
            EntityJavascriptObject.Bind("LoadConversation", false, LoadConversation);
            EntityJavascriptObject.Bind("LoadScript", false, LoadScript);
            EntityJavascriptObject.Bind("LoadTileset", false, LoadTileset);

            EntityJavascriptObject.Bind("DeleteArea", false, DeleteArea);
            EntityJavascriptObject.Bind("DeleteCreature", false, DeleteCreature);
            EntityJavascriptObject.Bind("DeleteItem", false, DeleteItem);
            EntityJavascriptObject.Bind("DeletePlaceable", false, DeletePlaceable);
            EntityJavascriptObject.Bind("DeleteConversation", false, DeleteConversation);
            EntityJavascriptObject.Bind("DeleteScript", false, DeleteScript);
            EntityJavascriptObject.Bind("DeleteTileset", false, DeleteTileset);

            EntityJavascriptObject.Bind("DeleteCategoryArea", false, DeleteCategoryArea);
            EntityJavascriptObject.Bind("DeleteCategoryCreature", false, DeleteCategoryCreature);
            EntityJavascriptObject.Bind("DeleteCategoryItem", false, DeleteCategoryItem);
            EntityJavascriptObject.Bind("DeleteCategoryPlaceable", false, DeleteCategoryPlaceable);
            EntityJavascriptObject.Bind("DeleteCategoryConversation", false, DeleteCategoryConversation);
            EntityJavascriptObject.Bind("DeleteCategoryScript", false, DeleteCategoryScript);
            EntityJavascriptObject.Bind("DeleteCategoryTileset", false, DeleteCategoryTileset);

            EntityJavascriptObject.Bind("GetModulesList", true, GetModulesList);
            
            // Tileset Editor Bindings
            EntityJavascriptObject.Bind("LoadTilesetSpritesheet", false, LoadTilesetSpritesheet);

            RunJavaScriptMethod("Initialize();");
        }

        #endregion

        #region General Methods

        private void SaveModule()
        {
            _moduleManager.SaveModule();
        }

        #endregion

        #region UI Methods - File Menu Bindings

        private void NewModuleButton(object sender, JavascriptMethodEventArgs e)
        {

            _moduleManager.ModuleName = e.Arguments[0];
            _moduleManager.ModuleTag = e.Arguments[1];
            _moduleManager.ModuleResref = e.Arguments[2];

            bool success = _moduleManager.CreateModule();

            PopulateToolsetViewModel();
            ViewModel.IsModuleOpened = success;

            AsyncJavascriptCallback("NewModuleBoxOKClick_Callback", success);
        }

        private void OpenModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                string filePath = DirectoryPaths.ModuleDirectoryPath + e.Arguments[0] + _extensionFactory.GetFileExtension(FileTypeEnum.Module);
                _moduleManager.OpenModule(filePath);
                PopulateToolsetViewModel();
                ViewModel.IsModuleOpened = true;

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
            if (String.IsNullOrWhiteSpace(_moduleManager.ModulePath))
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
                + _extensionFactory.GetFileExtension(FileTypeEnum.Module);
            bool forceOverwrite = e.Arguments[1];

            if (File.Exists(filePath) && !forceOverwrite)
            {
                response = SaveAsResponseTypeEnum.FileNameAlreadyExists;
            }
            else
            {
                _moduleManager.ModulePath = filePath;
                SaveModule();
                response = SaveAsResponseTypeEnum.SaveSuccessful;
            }

            AsyncJavascriptCallback("SaveAsModuleButtonClick_Callback", (int)response, e.Arguments[0]);
        }

        private void CloseModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            _moduleManager.CloseModule();
            ClearViewModelPopulation();
            ViewModel.IsModuleOpened = false;

            AsyncJavascriptCallback("CloseModuleButtonClick_Callback");
        }

        private void ExitButton(object sender, JavascriptMethodEventArgs e)
        {
            FlatRedBallServices.Game.Exit();
        }

        #endregion

        #region UI Methods - Edit Menu Bindings

        private void SaveModuleProperties(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                GameModule updatedModule = JsonConvert.DeserializeObject<GameModule>(e.Arguments[0]);
                _repositoryFactory.GetGenericRepository<GameModule>().Save(updatedModule);

                PopulateToolsetViewModel();
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
                _resourceManager.RebuildModule(ModuleRebuildModeEnum.UserResourcesOnly);
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
            List<ContentPackage> availableContentPackages = new List<ContentPackage>();
            List<ContentPackage> attachedContentPackages = (from package
                                        in _repositoryFactory.GetGenericRepository<ContentPackage>().GetAll()
                                        where package.IsSystemResource == false
                                        select package).ToList();

            // We don't need to send the resource list to the GUI because it could contain a lot of data.
            // We'll pick up the resource list when we go to do a save/rebuild of the module.
            attachedContentPackages.ForEach(a => a.ResourceList = null);
            

            string[] files = Directory.GetFiles(DirectoryPaths.ContentPackageDirectoryPath, "*" + _extensionFactory.GetFileExtension(FileTypeEnum.ContentPackage));
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
            _resourceManager.RebuildModule(contentPackageList, ModuleRebuildModeEnum.UserResourcesOnly);
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
                areaRootNode = _uiTreeObjectRepository.GenerateJSTreeHierarchy<Area>();
                creatureRootNode = _uiTreeObjectRepository.GenerateJSTreeHierarchy<Creature>();
                itemRootNode = _uiTreeObjectRepository.GenerateJSTreeHierarchy<Item>();
                placeableRootNode = _uiTreeObjectRepository.GenerateJSTreeHierarchy<Placeable>();
                conversationRootNode = _uiTreeObjectRepository.GenerateJSTreeHierarchy<Conversation>();
                scriptRootNode = _uiTreeObjectRepository.GenerateJSTreeHierarchy<Script>();
                tilesetRootNode = _uiTreeObjectRepository.GenerateJSTreeHierarchy<Tileset>();
                
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

        private void RefreshAreaEntity(object sender, ObjectSelectionEventArgs e)
        {
            if (!Object.ReferenceEquals(OnAreaLoaded, null))
            {
                OnAreaLoaded(sender, e);
            }
        }

        private void GetModulesList(object sender, JavascriptMethodEventArgs e)
        {
            string[] files = Directory.GetFiles(DirectoryPaths.ModuleDirectoryPath, "*"
                + _extensionFactory.GetFileExtension(FileTypeEnum.Module));
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

            // TODO: Consider adding new interface
            ViewModel.ActiveModule = _gameModuleRepository.GetModule();
            ViewModel.TilesetSpriteSheetsList = _uiObjectRepository.GetAllUIObjects().ToList(); // TODO: Type checking
            ViewModel.ItemList = _uiObjectRepository.GetAllUIObjects().ToList();
            ViewModel.ScriptList = _uiObjectRepository.GetAllUIObjects().ToList();
            ViewModel.GenderList = _uiObjectRepository.GetAllUIObjects().ToList();
            ViewModel.ConversationList = _uiObjectRepository.GetAllUIObjects().ToList();
            ViewModel.RaceList = _uiObjectRepository.GetAllUIObjects().ToList();
            ViewModel.FactionList = _uiObjectRepository.GetAllUIObjects().ToList();
            ViewModel.TilesetList = _uiObjectRepository.GetAllUIObjects().ToList();
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

        #region UI Methods - Object Save Methods

        private void SaveArea(object sender, JavascriptMethodEventArgs e)
        {
            Area area = JsonConvert.DeserializeObject<Area>(e.Arguments[1]);
            area = _repositoryFactory.GetGameObjectRepository<Area>().Save(area);
            bool setActiveArea = e.Arguments.Count() > 2 ? (bool)e.Arguments[2] : false;
            if (setActiveArea)
            {
                ViewModel.ActiveArea = area;
            }

            if (OnSaveArea != null)
            {
                OnSaveArea(this, new GameObjectSaveEventArgs(ViewModel.ActiveArea));
            }

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }

        private void SaveCreature(object sender, JavascriptMethodEventArgs e)
        {
            Creature creature = JsonConvert.DeserializeObject<Creature>(e.Arguments[1]);
            creature = _repositoryFactory.GetGameObjectRepository<Creature>().Save(creature);
            bool setActiveCreature = e.Arguments.Count() > 2 ? (bool)e.Arguments[2] : false;

            if (setActiveCreature)
            {
                ViewModel.ActiveCreature = creature;
            }

            if (OnSaveCreature != null)
            {
                OnSaveCreature(this, new GameObjectSaveEventArgs(ViewModel.ActiveCreature)); 
            }

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }

        private void SaveItem(object sender, JavascriptMethodEventArgs e)
        {
            Item item = JsonConvert.DeserializeObject<Item>(e.Arguments[1]);
            item = _repositoryFactory.GetGameObjectRepository<Item>().Save(item);
            bool setActiveItem = e.Arguments.Count() > 2 ? (bool)e.Arguments[2] : false;

            if (setActiveItem)
            {
                ViewModel.ActiveItem = item;
            }

            if (OnSaveItem != null)
            {
                OnSaveItem(this, new GameObjectSaveEventArgs(ViewModel.ActiveItem));
            }

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }

        private void SavePlaceable(object sender, JavascriptMethodEventArgs e)
        {
            Placeable placeable = JsonConvert.DeserializeObject<Placeable>(e.Arguments[1]);
            placeable = _repositoryFactory.GetGameObjectRepository<Placeable>().Save(placeable);
            bool setActivePlaceable = e.Arguments.Count() > 2 ? (bool)e.Arguments[2] : false;

            if (setActivePlaceable)
            {
                ViewModel.ActivePlaceable = placeable;
            }

            if (OnSavePlaceable != null)
            {
                OnSavePlaceable(this, new GameObjectSaveEventArgs(ViewModel.ActivePlaceable));
            }

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }

        private void SaveConversation(object sender, JavascriptMethodEventArgs e)
        {
            Conversation conversation = JsonConvert.DeserializeObject<Conversation>(e.Arguments[1]);
            conversation = _repositoryFactory.GetGameObjectRepository<Conversation>().Save(conversation);
            bool setActiveConversation = e.Arguments.Count() > 2 ? (bool)e.Arguments[2] : false;

            if (setActiveConversation)
            {
                ViewModel.ActiveConversation = conversation;
            }

            if (OnSaveConversation != null)
            {
                OnSaveConversation(this, new GameObjectSaveEventArgs(ViewModel.ActiveConversation));
            }

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }

        private void SaveScript(object sender, JavascriptMethodEventArgs e)
        {
            Script script = JsonConvert.DeserializeObject<Script>(e.Arguments[1]);
            script = _repositoryFactory.GetGameObjectRepository<Script>().Save(script);
            bool setActiveScript = e.Arguments.Count() > 2 ? (bool)e.Arguments[2] : false;

            if (setActiveScript)
            {
                ViewModel.ActiveScript = script;
            }

            if (OnSaveScript != null)
            {
                OnSaveScript(this, new GameObjectSaveEventArgs(ViewModel.ActiveScript));
            }

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }

        private void SaveTileset(object sender, JavascriptMethodEventArgs e)
        {
            Tileset tileset = JsonConvert.DeserializeObject<Tileset>(e.Arguments[1]);
            tileset = _repositoryFactory.GetGameObjectRepository<Tileset>().Save(tileset);
            bool setActiveTileset = e.Arguments.Count() > 2 ? (bool)e.Arguments[2] : false;

            if (setActiveTileset)
            {
                ViewModel.ActiveTileset = tileset;
            }

            if (OnSaveTileset != null)
            {
                OnSaveTileset(this, new GameObjectSaveEventArgs(ViewModel.ActiveTileset));
            }

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }

        private void SaveCategory(object sender, JavascriptMethodEventArgs e)
        {
            Category category = JsonConvert.DeserializeObject<Category>(e.Arguments[1]);
            category = _repositoryFactory.GetGenericRepository<Category>().Save(category);

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0], JsonConvert.SerializeObject(category));
            }
        }

        #endregion

        #region UI Methods - Object Load Methods

        private void LoadArea(object sender, JavascriptMethodEventArgs e)
        {
            int resourceID = (int)e.Arguments[1];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);
            ViewModel.ActiveArea = _repositoryFactory.GetGameObjectRepository<Area>().GetByID(resourceID);

            RefreshAreaEntity(this, eventArgs);
            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }
        private void LoadItem(object sender, JavascriptMethodEventArgs e)
        {
            int resourceID = (int)e.Arguments[1];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);
            ViewModel.ActiveItem = _repositoryFactory.GetGameObjectRepository<Item>().GetByID(resourceID);

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }
        private void LoadCreature(object sender, JavascriptMethodEventArgs e)
        {
            int resourceID = (int)e.Arguments[1];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);
            ViewModel.ActiveCreature = _repositoryFactory.GetGameObjectRepository<Creature>().GetByID(resourceID);

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }
        private void LoadPlaceable(object sender, JavascriptMethodEventArgs e)
        {
            int resourceID = (int)e.Arguments[1];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);
            ViewModel.ActivePlaceable = _repositoryFactory.GetGameObjectRepository<Placeable>().GetByID(resourceID);

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }
        private void LoadConversation(object sender, JavascriptMethodEventArgs e)
        {
            int resourceID = (int)e.Arguments[1];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);
            ViewModel.ActiveConversation = _repositoryFactory.GetGameObjectRepository<Conversation>().GetByID(resourceID);

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }
        private void LoadScript(object sender, JavascriptMethodEventArgs e)
        {
            int resourceID = (int)e.Arguments[1];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);
            ViewModel.ActiveScript = _repositoryFactory.GetGameObjectRepository<Script>().GetByID(resourceID);

            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }
        private void LoadTileset(object sender, JavascriptMethodEventArgs e)
        {
            int resourceID = (int)e.Arguments[1];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);
            ViewModel.ActiveTileset = _repositoryFactory.GetGameObjectRepository<Tileset>().GetByID(resourceID);

            RaiseTilesetLoadEvent(ViewModel.ActiveTileset.GraphicResourceID);
            if (!String.IsNullOrWhiteSpace(e.Arguments[0]))
            {
                AsyncJavascriptCallback(e.Arguments[0]);
            }
        }
        
        #endregion

        #region UI Methods - Object Delete Methods

        private void DeleteArea(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int areaID = (int)e.Arguments[1];
            _repositoryFactory.GetGameObjectRepository<Area>().Delete(areaID);
            ViewModel.ActiveArea = new Area(true);
            RefreshAreaEntity(this, new ObjectSelectionEventArgs(0));

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteItem(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int itemID = (int)e.Arguments[1];
            _repositoryFactory.GetGameObjectRepository<Item>().Delete(itemID);
            ViewModel.ActiveItem = new Item(true);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCreature(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int creatureID = (int)e.Arguments[1];
            _repositoryFactory.GetGameObjectRepository<Creature>().Delete(creatureID);
            ViewModel.ActiveCreature = new Creature(true);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeletePlaceable(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int placeableID = (int)e.Arguments[1];
            _repositoryFactory.GetGameObjectRepository<Placeable>().Delete(placeableID);
            ViewModel.ActivePlaceable = new Placeable(true);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteConversation(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int conversationID = (int)e.Arguments[1];
            _repositoryFactory.GetGameObjectRepository<Conversation>().Delete(conversationID);
            ViewModel.ActiveConversation = new Conversation(true);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteScript(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int scriptID = (int)e.Arguments[1];
            _repositoryFactory.GetGameObjectRepository<Script>().Delete(scriptID);
            ViewModel.ActiveScript = new Script();

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteTileset(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int tilesetID = (int)e.Arguments[1];
            _repositoryFactory.GetGameObjectRepository<Tileset>().Delete(tilesetID);
            ViewModel.ActiveTileset = new Tileset();
            
            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCategoryArea(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            Category category = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
            IEnumerable<Area> areas = _repositoryFactory.GetGameObjectRepository<Area>().GetAllByResourceCategory(category);
            _repositoryFactory.GetGameObjectRepository<Area>().Delete(areas);
            RefreshAreaEntity(this, new ObjectSelectionEventArgs(0));

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCategoryItem(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            Category category = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
            IEnumerable<Item> items = _repositoryFactory.GetGameObjectRepository<Item>().GetAllByResourceCategory(category);
            _repositoryFactory.GetGameObjectRepository<Item>().Delete(items);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCategoryCreature(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            Category category = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
            IEnumerable<Creature> creatures = _repositoryFactory.GetGameObjectRepository<Creature>().GetAllByResourceCategory(category);
            _repositoryFactory.GetGameObjectRepository<Creature>().Delete(creatures);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCategoryPlaceable(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            Category category = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
            IEnumerable<Placeable> placeables = _repositoryFactory.GetGameObjectRepository<Placeable>().GetAllByResourceCategory(category);
            _repositoryFactory.GetGameObjectRepository<Placeable>().Delete(placeables);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCategoryConversation(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            Category category = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
            IEnumerable<Conversation> conversations = _repositoryFactory.GetGameObjectRepository<Conversation>().GetAllByResourceCategory(category);
            _repositoryFactory.GetGameObjectRepository<Conversation>().Delete(conversations);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCategoryScript(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            Category category = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
            IEnumerable<Script> scripts = _repositoryFactory.GetGameObjectRepository<Script>().GetAllByResourceCategory(category);
            _repositoryFactory.GetGameObjectRepository<Script>().Delete(scripts);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
        }

        private void DeleteCategoryTileset(object sender, JavascriptMethodEventArgs e)
        {
            string callbackJSFunction = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            Category category = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
            IEnumerable<Tileset> tilesets = _repositoryFactory.GetGameObjectRepository<Tileset>().GetAllByResourceCategory(category);
            _repositoryFactory.GetGameObjectRepository<Tileset>().Delete(tilesets);

            PopulateToolsetViewModel();
            AsyncJavascriptCallback(callbackJSFunction);
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
