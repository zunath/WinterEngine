using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using Awesomium.Core;
using System.Diagnostics;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Factories;
using WinterEngine.Library.Managers;
using System.Windows.Forms;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.Editor.Managers;


#endif

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
            EntityJavascriptObject.Bind("SaveModuleButtonClick", false, SaveModuleButton);
            EntityJavascriptObject.Bind("SaveAsModuleButtonClick", false, SaveAsModuleButton);
            EntityJavascriptObject.Bind("CloseModuleButtonClick", false, OpenModuleButton);
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

        }

        #endregion

        #region General Methods

        private void SaveModule()
        {
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
                }
            }
            catch
            {
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
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                SaveModule();
            }
        }

        private void CloseModuleButton(object sender, JavascriptMethodEventArgs e)
        {
            ModuleManager.CloseModule();
        }

        private void ExitButton(object sender, JavascriptMethodEventArgs e)
        {

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

    }
}
