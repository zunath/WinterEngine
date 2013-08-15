using System;
using System.Collections.Generic;

using Awesomium.Core;
using System.Diagnostics;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Factories;
using WinterEngine.Library.Managers;
using System.Windows.Forms;

using WinterEngine.Editor.Managers;
using WinterEngine.DataAccess.FileAccess;
using System.Web.Script.Serialization;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Utility;


namespace WinterEngine.Game.Entities
{
	public partial class ToolsetUIEntity
    {
        #region Fields

        private FileExtensionFactory _extensionFactory;
        private ModuleManager _moduleManager;
        private SaveFileDialog _saveFile;
        private OpenFileDialog _openFile;

        #endregion

        #region Properties

        private string ModuleFilePath { get; set; }

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

        private GameModule ActiveModule { get; set; }

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

        private SaveFileDialog SaveFile
        {
            get
            {
                if (_saveFile == null)
                {
                    _saveFile = new SaveFileDialog();
                    _saveFile.AddExtension = true;
                }

                return _saveFile;
            }
        }

        private OpenFileDialog OpenFile
        {
            get
            {
                if (_openFile == null)
                {
                    _openFile = new OpenFileDialog();
                    _openFile.AddExtension = true;
                    _openFile.Multiselect = false;
                }

                return _openFile;
            }
        }

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
            EntityJavascriptObject.Bind("ImportButtonClick", false, ImportButtonClick);
            EntityJavascriptObject.Bind("ExportButtonClick", false, ExportButtonClick);
            EntityJavascriptObject.Bind("ExportToERFButtonClick", false, ExportToERFButtonClick);
            EntityJavascriptObject.Bind("ExitButtonClick", false, ExitButton);

            // Edit Menu Bindings

            // Content Menu Bindings
            EntityJavascriptObject.Bind("BuildModuleButtonClick", false, BuildModuleButton);

            // Help Menu Bindings
            EntityJavascriptObject.Bind("WinterEngineWebsiteButtonClick", false, WinterEngineWebsiteButton);

            // Object Menu Bindings
            EntityJavascriptObject.Bind("AreasButtonClick", false, AreasButton);
            EntityJavascriptObject.Bind("CreaturesButtonClick", false, CreaturesButton);
            EntityJavascriptObject.Bind("ItemsButtonClick", false, ItemsButton);
            EntityJavascriptObject.Bind("PlaceablesButtonClick", false, PlaceablesButton);
            EntityJavascriptObject.Bind("ConversationsButtonClick", false, ConversationsButton);
            EntityJavascriptObject.Bind("ScriptsButtonClick", false, ScriptsButton);
            EntityJavascriptObject.Bind("GraphicsButtonClick", false, GraphicsButton);

            // Treeview Bindings
            EntityJavascriptObject.Bind("LoadTreeViewData", false, LoadTreeViewData);
            EntityJavascriptObject.Bind("AddNewObject", false, AddNewObject);
            EntityJavascriptObject.Bind("AddNewCategory", false, AddNewCategory);
            EntityJavascriptObject.Bind("DeleteCategory", false, DeleteCategory);
            EntityJavascriptObject.Bind("DeleteObject", false, DeleteObject);
            EntityJavascriptObject.Bind("RenameCategory", false, RenameCategory);
            EntityJavascriptObject.Bind("RenameObject", false, RenameObject);
        }

        #endregion

        #region General Methods

        private void SaveModule()
        {
            ModuleManager.SaveModule(ModuleFilePath);
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
                OpenFile.Filter = ExtensionFactory.BuildModuleFileFilter();

                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    ModuleFilePath = OpenFile.FileName;
                    ModuleManager.OpenModule(ModuleFilePath);

                    AsyncJavascriptCallback("OpenModuleButtonClick_Callback", true);
                }
            }
            catch
            {
                AsyncJavascriptCallback("OpenModuleButtonClick_Callback", false);
                throw;
            }
        }

        private void SaveModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ModuleFilePath))
            {
                SaveAsModuleButton(sender, e);
            }
            else
            {
                SaveModule();
            }
        }

        private void SaveAsModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            SaveFile.Filter = ExtensionFactory.BuildModuleFileFilter();

            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                ModuleFilePath = SaveFile.FileName;
                SaveModule();
            }
        }

        private void CloseModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            ModuleManager.CloseModule();

            AsyncJavascriptCallback("CloseModuleButtonClick_Callback");
        }

        private void ImportButtonClick(object sender, JavascriptMethodEventArgs e)
        {
            OpenFile.Filter = ExtensionFactory.BuildERFFileFilter();

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<GameObjectBase> gameObjects;
                    using (ERFFileAccess repo = new ERFFileAccess())
                    {
                        gameObjects = repo.DeserializeERFFile(OpenFile.FileName);
                    }

                    string jsonGameObjects = new JavaScriptSerializer().Serialize(gameObjects);
                    AsyncJavascriptCallback("ImportButtonClick_Callback", jsonGameObjects);
                }
                catch
                {
                    throw;
                }

            }
        }

        private void ExportButtonClick(object sender, JavascriptMethodEventArgs e)
        {
            OpenFile.Filter = ExtensionFactory.BuildERFFileFilter();

            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                using (ERFFileAccess repo = new ERFFileAccess())
                {
                    List<GameObjectBase> gameObjects = repo.DeserializeERFFile(OpenFile.FileName);
                    string jsonGameObjects = new JavaScriptSerializer().Serialize(gameObjects);
                    AsyncJavascriptCallback("ExportButtonClick_Callback", jsonGameObjects);
                }
            }
        }

        private void ExportToERFButtonClick(object sender, JavascriptMethodEventArgs e)
        {

        }

        private void ExitButton(object sender, JavascriptMethodEventArgs e)
        {
            Application.Exit();
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
                using (GameResourceManager manager = new GameResourceManager())
                {
                    manager.RebuildModule();
                }

                success = true;
            }
            catch(Exception ex)
            {
                success = false;
                callbackException = ex.StackTrace;
            }

            AsyncJavascriptCallback("BuildModuleButtonClick_Callback", success, callbackException);
        }

        #endregion

        #region UI Methods - Help Menu Bindings

        private void WinterEngineWebsiteButton(object sender, JavascriptMethodEventArgs e)
        {
            Process.Start("https://www.winterengine.com");
        }


        #endregion

        #region UI Methods - Object Menu Bindings

        private void AreasButton(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("AreasButtonClick_Callback");
        }

        private void CreaturesButton(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("CreaturesButtonClick_Callback");
        }

        private void ItemsButton(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("ItemsButtonClick_Callback");
        }

        private void PlaceablesButton(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("PlaceablesButtonClick_Callback");
        }

        private void ConversationsButton(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("ConversationsButtonClick_Callback");
        }

        private void ScriptsButton(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("ScriptsButtonClick_Callback");
        }

        private void GraphicsButton(object sender, JavascriptMethodEventArgs e)
        {
            AsyncJavascriptCallback("GraphicsButtonClick_Callback");
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

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                
                AsyncJavascriptCallback("LoadTreeViews_Callback",
                    serializer.Serialize(areaRootNode),
                    serializer.Serialize(creatureRootNode),
                    serializer.Serialize(itemRootNode),
                    serializer.Serialize(placeableRootNode),
                    serializer.Serialize(conversationRootNode),
                    serializer.Serialize(scriptRootNode));
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
            ErrorTypeEnum error = ErrorTypeEnum.None;
            GameObjectFactory factory = new GameObjectFactory();
            string name = e.Arguments[0];
            string tag = e.Arguments[1];
            string resref = e.Arguments[2];
            int categoryID = (int)e.Arguments[3];
            GameObjectTypeEnum gameObjectType =  (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[4]);

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

                factory.AddToDatabase(newObject);
            }

            AsyncJavascriptCallback("CreateNewObject_Callback", 
                error == ErrorTypeEnum.None ? true : false, 
                EnumerationHelper.GetEnumerationDescription(error),
                (int)gameObjectType,
                name,
                resref);
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
            string resref = e.Arguments[0];
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[1]);
            
            if (!factory.DoesObjectExistInDatabase(resref, gameObjectType))
            {
                error = ErrorTypeEnum.ObjectResrefDoesNotExist;
            }
            else
            {
                factory.DeleteFromDatabase(resref, gameObjectType);
            }

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
            string resref = e.Arguments[1];
            GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), e.Arguments[2]);

            GameObjectBase dbObject = factory.GetFromDatabaseByResref(resref, gameObjectType);

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

    }
}
