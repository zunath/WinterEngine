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

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class ToolsetUIEntity
    {
        #region Fields

        #endregion

        #region Properties

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
            EntityJavascriptObject.Bind("ExitButtonClick", false, ExitButton);

            // Edit Menu Bindings

            // Content Menu Bindings

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

        #region UI Methods - File Menu Bindings

        private void NewModuleButton(object sender, JavascriptMethodEventArgs e)
        {
        }

        private void ExitButton(object sender, JavascriptMethodEventArgs e)
        {

        }

        #endregion

        #region UI Methods - Edit Menu Bindings

        #endregion

        #region UI Methods - Content Menu Bindings

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
