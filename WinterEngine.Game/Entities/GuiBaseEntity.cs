using System;
using FlatRedBall;
using FlatRedBall.Input;
using System.Collections.Generic;


using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using System.Drawing;
using System.Drawing.Imaging;
using FlatRedBall.Math;
using WinterEngine.Network;
using FlatRedBall.Gui;
using WinForms = System.Windows.Forms;
using WinterEngine.Game.Services;
using Microsoft.Xna.Framework.Graphics;
using AwesomiumXNA;
using FlatRedBall.Graphics;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using WinterEngine.DataTransferObjects.Paths;

namespace WinterEngine.Game.Entities
{
	public partial class GuiBaseEntity
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
            get { return new Uri(ResourcePath); }
        }

        #endregion

        #region Events / Delegates

        public event EventHandler<TypeOfEventArgs> OnChangeScreen;

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
        {
            InitializeAwesomium();
        }

		private void CustomActivity()
		{
            this.LayerProvidedByContainer.UsePixelCoordinates();
            
            this.UISprite.Texture = AwesomeComponent.WebViewTexture;
            this.UISprite.Width = FlatRedBallServices.GraphicsDevice.Viewport.Bounds.Width;
            this.UISprite.Height = FlatRedBallServices.GraphicsDevice.Viewport.Bounds.Height;
		}

		private void CustomDestroy()
        {
            DisposeAwesomium();
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


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
            catch(Exception ex)
            {
                throw new Exception("Unable to load partial view.", ex);
            }

        }

        public void RaiseChangeScreenEvent(TypeOfEventArgs screenType)
        {
            if (!Object.ReferenceEquals(OnChangeScreen, null))
            {
                OnChangeScreen(this, screenType);
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

            this.UISprite.Texture = AwesomeComponent.WebViewTexture;
            this.UISprite.Width = FlatRedBallServices.GraphicsDevice.Viewport.Bounds.Width;
            this.UISprite.Height = FlatRedBallServices.GraphicsDevice.Viewport.Bounds.Height;
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
            FlatRedBallServices.Game.Components.Remove(AwesomeComponent);
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
