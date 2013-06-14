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

            EntityJavascriptObject.Bind("ExitButtonClick", false, ExitButton);

            // Edit Menu Bindings

            // Content Menu Bindings

            // Help Menu Bindings
            EntityJavascriptObject.Bind("WinterEngineWebsiteButtonClick", false, WinterEngineWebsiteButton);
        }

        #endregion

        #region UI Methods - File Menu Bindings

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

    }
}
