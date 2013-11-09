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
using WinterEngine.Editor.Extensions;
using WinterEngine.Library.Managers;
using WinterEngine.Editor.Utility;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Converters;
using WinterEngine.DataTransferObjects.UIObjects;


namespace WinterEngine.Game.Entities
{
	public partial class ToolsetUIEntity
    {
        #region Fields

        private readonly ToolsetViewModel _viewModel;
        private readonly FileExtensionFactory _extensionFactory;
        private readonly ModuleManager _moduleManager;
        private readonly JsonSerializerSettings _serializerSettings;
        
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IGameObjectFactory _gameObjectFacotry;
        private readonly IGameResourceManager _resourceManager;
        
        #endregion

        #region Properties

        private ToolsetViewModel ViewModel 
        {
            get
            {

                return _viewModel;
            }

            }

        //private JsonSerializerSettings JSONSerializerSettings
        //{
        //    get
        //    {
        //        if (_serializerSettings == null)
        //        {
        //            _serializerSettings = new JsonSerializerSettings
        //            {
        //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //                NullValueHandling = NullValueHandling.Ignore
        //            };
        //        }
        //        return _serializerSettings;
        //    }
        //}

        //private FileExtensionFactory ExtensionFactory 
        //{
        //    get
        //    {
        //        if (_extensionFactory == null)
        //        {
        //            _extensionFactory = new FileExtensionFactory();
        //        }

        //        return _extensionFactory;
        //    }
        //}

        //private ModuleManager ModuleManager
        //{
        //    get
        //    {
        //        if (_moduleManager == null)
        //        {
        //            _moduleManager = new ModuleManager();
        //        }

        //        return _moduleManager;
        //    }
        //}

        #endregion

        #region Events / Delegates

        public event EventHandler<ObjectModeChangedEventArgs> OnObjectModeChanged;

        // Area Editor Events
        public event EventHandler<ObjectSelectionEventArgs> OnAreaLoaded;

        // Tileset Editor Events
        public event EventHandler<TilesetSelectionEventArgs> OnTilesetLoaded;
        public event EventHandler<TilesetSelectionEventArgs> OnTilesetSaved;

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
            ModuleManager.ModuleName = e.Arguments[0];
            ModuleManager.ModuleTag = e.Arguments[1];
            ModuleManager.ModuleResref = e.Arguments[2];
            bool success = ModuleManager.CreateModule();
            _moduleManager.ModuleName = e.Arguments[0];
            _moduleManager.ModuleTag = e.Arguments[1];
            bool success = _moduleManager.CreateModule();
            PopulateToolsetViewModel();

            AsyncJavascriptCallback("NewModuleBoxOKClick_Callback", success);
        }

        private void OpenModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                string filePath = DirectoryPaths.ModuleDirectoryPath + e.Arguments[0] + _extensionFactory.GetFileExtension(FileTypeEnum.Module);
                _moduleManager.OpenModule(filePath);
                PopulateToolsetViewModel();

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

                using (GameModuleRepository repo = new GameModuleRepository())
                {
                    repo.Update(updatedModule);
                }

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
            List<ContentPackage> attachedContentPackages;
            List<ContentPackage> availableContentPackages = new List<ContentPackage>();

            var repo = _repositoryFactory.GetGenericRepository<ContentPackage>();
            attachedContentPackages = repo.GetAll().Where(x => x.IsSystemResource == false).ToList();
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

            //ViewModel.AvailableContentPackages = availableContentPackages;
            //ViewModel.AttachedContentPackages = attachedContentPackages;

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

                /*
                 * I don't think the repositories need to generate JSTreeNode.
                 */

                // Get each category's children for each object type
                areaRootNode = _repositoryFactory.GetGameObjectRepository<Area>().GenerateJSTreeHierarchy();
                creatureRootNode = _repositoryFactory.GetGameObjectRepository<Creature>().GenerateJSTreeHierarchy();
                itemRootNode = _repositoryFactory.GetGameObjectRepository<Item>().GenerateJSTreeHierarchy();
                placeableRootNode = _repositoryFactory.GetGameObjectRepository<Placeable>().GenerateJSTreeHierarchy();
                conversationRootNode = _repositoryFactory.GetGameObjectRepository<Conversation>().GenerateJSTreeHierarchy();
                scriptRootNode = _repositoryFactory.GetGameObjectRepository<Script>().GenerateJSTreeHierarchy();
                tilesetRootNode = _repositoryFactory.GetGameObjectRepository<Tileset>().GenerateJSTreeHierarchy();
                
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

            //using (CategoryRepository repo = new CategoryRepository())
            //{
            //    newCategory = repo.Add(newCategory);
            //}

            newCategory = _repositoryFactory.GetGenericRepository<Category>().Add(newCategory);

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
                //GameObjectFactory factory = new GameObjectFactory();
                
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
                //Why do we need to check if it exists? Can't we just save it?
                //if (factory.DoesObjectExistInDatabase(resref, gameObjectType))
                //{
                //    error = ErrorTypeEnum.ObjectResrefAlreadyExists;
                //}
                //else
                //{
                GameObjectBase newObject = _gameObjectFacotry.Create(gameObjectType);
                newObject.Name = name;
                newObject.Tag = tag;
                newObject.Resref = resref;
                    newObject.ResourceCategoryID = categoryID;

                resourceID = _repositoryFactory.GetRepository(gameObjectType).Save(newObject);

                
                //}

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

        //TODO: It would be nice to get rid of this
        private void DeleteCategory(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            int categoryID = (int)e.Arguments[0];
            //GameObjectFactory factory = new GameObjectFactory();
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[1]);
            

            //using(CategoryRepository repo = new CategoryRepository())
            //{
            //    categoryToRemove = repo.GetByID(categoryID);
            //}

            Category categoryToRemove = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);

            _repositoryFactory.GetRepository(gameObjectType).DeleteByCategory(categoryToRemove);
        
            if (gameObjectType == GameObjectTypeEnum.Area)
            {
                RefreshAreaEntity(this, new ObjectSelectionEventArgs(0));
            }

            AsyncJavascriptCallback("DeleteObject_Callback",
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error));
        }

        private void DeleteObject(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            //GameObjectFactory factory = new GameObjectFactory();
            int resourceID = (int)e.Arguments[0];
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[1]);

            //factory.DeleteFromDatabase(resourceID, gameObjectType);

            _repositoryFactory.GetRepository(gameObjectType).Delete(resourceID);

            if (gameObjectType == GameObjectTypeEnum.Area)
            {
                RefreshAreaEntity(this, new ObjectSelectionEventArgs(0));
            }

            AsyncJavascriptCallback("DeleteObject_Callback", 
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error));
        }

        //Todo: Why isn't a save good for this?
        private void RenameCategory(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            string name = e.Arguments[0];
            int categoryID = (int)e.Arguments[1];
            
            //using (CategoryRepository repo = new CategoryRepository())
            //{
            Category dbCategory = _repositoryFactory.GetGenericRepository<Category>().GetByID(categoryID);
                if (!dbCategory.IsSystemResource)
                {
                    dbCategory.Name = name;
                }
                else
                {
                    error = ErrorTypeEnum.CannotChangeSystemResource;
                }
            //}

            AsyncJavascriptCallback("RenameObject_Callback",
                error == ErrorTypeEnum.None ? true : false,
                EnumerationHelper.GetEnumerationDescription(error),
                name);
        }

        //Todo: Why isn't a save good for this?
        private void RenameObject(object sender, JavascriptMethodEventArgs e)
        {
            ErrorTypeEnum error = ErrorTypeEnum.None;
            //GameObjectFactory factory = new GameObjectFactory();
            string name = e.Arguments[0];
            int resourceID = (int)e.Arguments[1];
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[2]);

            //GameObjectBase dbObject = factory.GetFromDatabaseByID(resourceID, gameObjectType);

            var dbObject = (GameObjectBase)_repositoryFactory.GetRepository(gameObjectType).Load(resourceID);

            if (dbObject == null)
            {
                error = ErrorTypeEnum.ObjectResrefDoesNotExist;
            }
            else
            {
                if (!dbObject.IsSystemResource)
                {
                    dbObject.Name = name;
                    _repositoryFactory.GetRepository(gameObjectType).Save(dbObject);
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
            int resourceID = (int)e.Arguments[0];
            ObjectSelectionEventArgs eventArgs = new ObjectSelectionEventArgs(resourceID);

            var gameObject = _repositoryFactory.GetRepository(ViewModel.GameObjectType).Load(resourceID);
            Type type = Type.GetType("WinterEngine.DataTransferObjects" + ViewModel.GameObjectType.ToString() + "`1");
            var prop = ViewModel.GetType().GetProperties().Where(p => p.GetType() == type).Single();
            prop.SetValue(prop, gameObject, null);
            
            if (ViewModel.GameObjectType == GameObjectTypeEnum.Area)
            {
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

        private void SaveObjectData(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[0]);
                
                string jsonModel = e.Arguments[1];
                ToolsetViewModel model = JsonConvert.DeserializeObject<ToolsetViewModel>(jsonModel, _serializerSettings);
                
                Type type = Type.GetType("WinterEngine.DataTransferObjects" + gameObjectType.ToString() + "`1");

                var prop = model.GetType().GetProperties().Where(p => p.GetType() == type).Single();
                var gameObject = (GameObjectBase)prop.GetValue(model, null);

                _repositoryFactory.GetRepository(type.ToString()).Save(gameObject);
                
                if (gameObjectType == GameObjectTypeEnum.Area)
                {
                    factory.UpsertInDatabase(model.ActiveArea);
                    ViewModel.ActiveArea = model.ActiveArea;
                    ObjectSelectionEventArgs areaEventArgs = new ObjectSelectionEventArgs(model.ActiveArea.ResourceID);
                    RefreshAreaEntity(this, areaEventArgs);
                }
                else if (gameObjectType == GameObjectTypeEnum.Conversation)
                {
                    factory.UpsertInDatabase(model.ActiveConversation);
                    ViewModel.ActiveConversation = model.ActiveConversation;
                }
                else if (gameObjectType == GameObjectTypeEnum.Creature)
                {
                    factory.UpsertInDatabase(model.ActiveCreature);
                    ViewModel.ActiveCreature = model.ActiveCreature;
                }
                else if (gameObjectType == GameObjectTypeEnum.Item)
                {
                    factory.UpsertInDatabase(model.ActiveItem);
                    ViewModel.ActiveItem = model.ActiveItem;
                }
                else if (gameObjectType == GameObjectTypeEnum.Placeable)
                {
                    factory.UpsertInDatabase(model.ActivePlaceable);
                    ViewModel.ActivePlaceable = model.ActivePlaceable;
                }
                else if (gameObjectType == GameObjectTypeEnum.Script)
                {
                    factory.UpsertInDatabase(model.ActiveScript);
                    ViewModel.ActiveScript = model.ActiveScript;
                }
                else if (gameObjectType == GameObjectTypeEnum.Tileset)
                {
                    factory.UpsertInDatabase(model.ActiveTileset);
                    ViewModel.ActiveTileset = model.ActiveTileset;
                    RaiseTilesetSaveEvent();
                }

                AsyncJavascriptCallback("ObjectTabApplyChanges_Callback");
            }
            catch
            {
                throw;
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

            using (GameModuleRepository repo = new GameModuleRepository())
            {
                ViewModel.ActiveModule = repo.GetModule();
            }

            using (ContentPackageResourceRepository repo = new ContentPackageResourceRepository())
            {
                ViewModel.TilesetSpriteSheetsList = repo.GetAllUIObjects(ContentPackageResourceTypeEnum.Tileset, false);
            }
           
            //List<ContentPackageResource> resourceList = repo.GetAllByResourceType(ContentPackageResourceTypeEnum.Tileset);

            List<ContentPackageResource> resourceList = _repositoryFactory.GetGenericRepository<ContentPackageResource>().GetAll().Where(x => x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Tileset).ToList();
            resourceList.Insert(0, new ContentPackageResource
            {
                ContentPackageResourceType = ContentPackageResourceTypeEnum.Tileset,
                Name = "(None)",
                ResourceID = 0,
                ResourceType = ResourceTypeEnum.GameObject
            });
            ViewModel.TilesetSpriteSheetsList = resourceList;

            ViewModel.ItemList = _repositoryFactory.GetGenericRepository<Item>().GetAll();
            ViewModel.ScriptList = _repositoryFactory.GetGenericRepository<Script>().GetAll();
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
            ViewModel.CurrentObjectTabSelector = e.Arguments[2];
            string mode = e.Arguments[0];

            // Inform subscribers (AKA: The screen) that the object mode has changed.
            if (OnObjectModeChanged != null)
                {
                OnObjectModeChanged(this, new ObjectModeChangedEventArgs((GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), mode)));
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

        private void RaiseTilesetSaveEvent()
        {
            if (OnTilesetSaved != null)
        {
                OnTilesetSaved(this, new TilesetSelectionEventArgs(ViewModel.ActiveTileset.ResourceID, ViewModel.ActiveTileset.GraphicResourceID));
            }
        }

        #endregion

    }
}
