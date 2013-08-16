using System;
using FlatRedBall;
using FlatRedBall.Input;
using System.Collections.Generic;


#if FRB_XNA || SILVERLIGHT
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using System.Drawing;
using System.Drawing.Imaging;
using WinterEngine.UI.AwesomiumXNA;
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


#endif

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
            _batch = new SpriteBatch(FlatRedBallServices.GraphicsDevice);
            InitializeAwesomium();

            WinterEngineService.OnXNAUpdate += UpdateAwesomium;
            WinterEngineService.OnXNADraw += DrawAwesomium;
        }

		private void CustomActivity()
		{
		}

		private void CustomDestroy()
        {
            DisposeAwesomium();

            WinterEngineService.OnXNAUpdate -= UpdateAwesomium;
            WinterEngineService.OnXNADraw -= DrawAwesomium;
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        #endregion

        #region Methods


        public void RaiseChangeScreenEvent(TypeOfEventArgs screenType)
        {
            if (!Object.ReferenceEquals(OnChangeScreen, null))
            {
                OnChangeScreen(this, screenType);
            }
        }


        #endregion

        #region Rendering Methods


        public JSObject RunJavaScriptMethod(string methodName)
        {
            return AwesomeComponent.WebView.ExecuteJavascriptWithResult(methodName);
        }


        #endregion

        #region Input handling


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
            WinForms.MessageBox.Show(e.Message, "Console Message");
        }

        private void OnJavascriptDialog(object sender, JavascriptDialogEventArgs e)
        {
            WinForms.MessageBox.Show(e.Message, "Javascript Pop-Up");
        }

        private void DisposeAwesomium()
        {
            FlatRedBallServices.Game.Window.ClientSizeChanged -= ResizeWindow;
        }

        private void OnDocumentReady(object sender, EventArgs e)
        {
            AwesomeComponent.WebView.DocumentReady -= OnDocumentReady;
            _entityJavascriptObject = AwesomeComponent.WebView.CreateGlobalJavascriptObject("Entity");
        }

        private void UpdateAwesomium(object sender, EventArgs e)
        {
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

        #endregion

    }
}
