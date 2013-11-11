using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using Awesomium.Core;
using System.IO;
using WinterEngine.DataTransferObjects.Paths;
using Microsoft.Xna.Framework.Graphics;
using AwesomiumXNA;
using WinterEngine.Game.Services;
using WinForms = System.Windows.Forms;

namespace WinterEngine.Game.Screens
{
    public partial class BaseScreen
    {
        #region Fields

        private AwesomiumComponent _awesomium;
        private SpriteBatch _batch;
        private JSObject _entityJavascriptObject;

        #endregion

        #region Properties

        public bool IsMouseOverUI { get; set; }

        private AwesomiumComponent AwesomeComponent
        {
            get { return _awesomium; }
            set { _awesomium = value; }
        }

        public WebView AwesomiumWebView
        {
            get { return _awesomium.WebView; }
        }

        public JSObject EntityJavascriptObject
        {
            get { return _entityJavascriptObject; }
        }

        /// <summary>
        /// Returns the URI for the entity's resource path.
        /// </summary>
        public Uri URI
        {
            get { return new Uri(UIResourcePath); }
        }

        #endregion


        #region FRB Events

        void CustomInitialize()
        {
            _batch = new SpriteBatch(FlatRedBallServices.GraphicsDevice);
            InitializeAwesomium();

            WinterEngineService.OnXNADraw += DrawAwesomium;

        }

        void CustomActivity(bool firstTimeCalled)
        {


        }

        void CustomDestroy()
        {
            DisposeAwesomium();

            WinterEngineService.OnXNADraw -= DrawAwesomium;
        }

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Event Handling


        protected void ChangeScreen(object sender, TypeOfEventArgs e)
        {
            this.MoveToScreen(e.ObjectType);
        }

        #endregion

        #region Methods


        private void GetPartialViewHTML(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                string partialViewPath = DirectoryPaths.PartialViewsDirectoryPath + e.Arguments[0];
                string html = File.ReadAllText(partialViewPath);

                e.Result = html;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load partial view.", ex);
            }

        }

        #endregion

        #region Awesomium Event Handling

        private void InitializeAwesomium()
        {
            AwesomeComponent = new AwesomiumComponent(FlatRedBallServices.Game, FlatRedBallServices.GraphicsDevice.Viewport.Bounds);
            AwesomeComponent.WebView.ParentWindow = FlatRedBallServices.WindowHandle;
            AwesomeComponent.WebView.Source = URI;
            FlatRedBallServices.Game.Components.Add(AwesomeComponent);

            FlatRedBallServices.Game.Window.ClientSizeChanged += ResizeWindow;
            AwesomeComponent.WebView.DocumentReady += OnDocumentReady;
            AwesomeComponent.WebView.ShowJavascriptDialog += OnJavascriptDialog;
            AwesomeComponent.WebView.ConsoleMessage += OnConsoleMessage;
        }

        private void OnConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            WinForms.MessageBox.Show("( Source: " + e.Source + " ) Line: " + e.LineNumber + ". Message: " + e.Message, "Console Message");
        }

        private void OnJavascriptDialog(object sender, JavascriptDialogEventArgs e)
        {
            WinForms.MessageBox.Show(e.Message, "Javascript Pop-Up");
        }

        private void DisposeAwesomium()
        {
            FlatRedBallServices.Game.Window.ClientSizeChanged -= ResizeWindow;
            AwesomeComponent.WebView.Dispose();
        }

        private void OnDocumentReady(object sender, EventArgs e)
        {
            AwesomeComponent.WebView.DocumentReady -= OnDocumentReady;
            _entityJavascriptObject = AwesomeComponent.WebView.CreateGlobalJavascriptObject("Entity");

            EntityJavascriptObject.Bind("MouseEnterUI", false, SetMouseIsInUITrue);
            EntityJavascriptObject.Bind("MouseExitUI", false, SetMouseIsInUIFalse);
            EntityJavascriptObject.Bind("GetPartialViewHTML", true, GetPartialViewHTML);

            RunJavaScriptMethod("Awesomium_LoadPartialViews();");
        }

        private void SetMouseIsInUITrue(object sender, JavascriptMethodEventArgs e)
        {
            IsMouseOverUI = true;
        }

        private void SetMouseIsInUIFalse(object sender, JavascriptMethodEventArgs e)
        {
            IsMouseOverUI = false;
        }

        private void DrawAwesomium(object sender, EventArgs e)
        {
            if (AwesomeComponent.WebViewTexture != null)
            {
                _batch.Begin();
                _batch.Draw(AwesomeComponent.WebViewTexture,
                    FlatRedBallServices.GraphicsDevice.Viewport.Bounds,
                    Microsoft.Xna.Framework.Color.White);
                _batch.End();
            }
        }

        private void ResizeWindow(object sender, EventArgs e)
        {
            AwesomeComponent.Resize(FlatRedBallServices.GraphicsDevice.Viewport.Width,
                FlatRedBallServices.GraphicsDevice.Viewport.Height);
        }


        #endregion

        #region Awesomium API Wrappers

        /// <summary>
        /// Performs an asynchronous callback to the active web view.
        /// This prevents the UI from being blocked on slow C# methods (such as web service calls)
        /// </summary>
        /// <param name="callback">The name of the javascript function to call.</param>
        /// <param name="args">The arguments to pass to the javascript function.</param>
        protected void AsyncJavascriptCallback(string callback, params JSValue[] args)
        {
            JSObject window = AwesomeComponent.WebView.ExecuteJavascriptWithResult("window");

            using (window)
            {
                window.InvokeAsync(callback, args);
            }

        }

        public JSObject RunJavaScriptMethod(string methodName)
        {
            return AwesomeComponent.WebView.ExecuteJavascriptWithResult(methodName);
        }

        #endregion
    }
}
