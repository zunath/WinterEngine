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

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using System.Diagnostics;
using FlatRedBall.Screens;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class MainMenuUIEntity
	{
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

        #region Awesomium Event Handling

        private void OnDocumentReady(object sender, EventArgs eventArgs)
        {
            AwesomiumWebView.DocumentReady -= OnDocumentReady;

            GlobalJavascriptObject.Bind("FindServerButtonClick", false, FindServerButtonClick);
            GlobalJavascriptObject.Bind("ToolsetButtonClick", false, ToolsetButtonClick);
            GlobalJavascriptObject.Bind("SettingsButtonClick", false, SettingsButtonClick);
            GlobalJavascriptObject.Bind("ForumsButtonClick", false, ForumsButtonClick);
            GlobalJavascriptObject.Bind("ExitButtonClick", false, ExitButtonClick); 
        }

        #endregion

        #region UI Methods

        private void FindServerButtonClick(object sender, JavascriptMethodEventArgs args)
        {

        }

        private void ToolsetButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            
        }

        private void SettingsButtonClick(object sender, JavascriptMethodEventArgs args)
        {

        }

        private void ForumsButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            Process.Start("https://www.winterengine.com/forum");
        }

        private void ExitButtonClick(object sender, JavascriptMethodEventArgs args)
        {
            FlatRedBallServices.Game.Exit();
        }

        #endregion
    }
}
