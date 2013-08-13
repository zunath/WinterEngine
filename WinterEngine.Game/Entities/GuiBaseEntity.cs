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


#endif

namespace WinterEngine.Game.Entities
{
	public partial class GuiBaseEntity
    {
        #region Fields

        private WebView _webView;
        private Texture2D _texture;
        private SpriteBatch _batch;
        private JSObject _entityJavascriptObject;

        #endregion

        #region Properties

        public WebView AwesomiumWebView
        {
            get { return _webView; }
            set { _webView = value; }
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

        private void RenderAwesomiumTexture()
        {
            BitmapSurface surface = (BitmapSurface)_webView.Surface;

            if (surface != null)
            {
                _texture = surface.RenderTexture2D(_texture);
            }
        }

        public JSObject RunJavaScriptMethod(string methodName)
        {
            return _webView.ExecuteJavascriptWithResult(methodName);
        }


        #endregion

        #region Input handling

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (!AwesomiumWebView.IsDisposed)
            {
                _webView.InjectMouseMove(e.X, e.Y);
            }
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if (!AwesomiumWebView.IsDisposed)
            {
                MouseButton button = MouseButton.Left;
                if (e.Button == WinMouseButton.Right)
                {
                    button = MouseButton.Right;
                }
                else if (e.Button == WinMouseButton.Middle)
                {
                    button = MouseButton.Middle;
                }
                _webView.InjectMouseDown(button);
            }
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            if (!AwesomiumWebView.IsDisposed)
            {
                MouseButton button = MouseButton.Left;
                if (e.Button == WinMouseButton.Right)
                {
                    button = MouseButton.Right;
                }
                else if (e.Button == WinMouseButton.Middle)
                {
                    button = MouseButton.Middle;
                }
                _webView.InjectMouseUp(button);
            }
        }

        private void FullKeyHandler(object sender, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (!AwesomiumWebView.IsLoading && !AwesomiumWebView.IsDisposed)
            {
                Modifiers modifiers = new Modifiers();
                WebKeyboardEvent keyEvent = new WebKeyboardEvent((uint)msg, (IntPtr)wParam, (IntPtr)lParam, modifiers);

                _webView.InjectKeyboardEvent(keyEvent);
            }
        }

        #endregion

        #region Awesomium Event Handling

        private void InitializeAwesomium()
        {
            SetUpAwesomiumDimensions();

            if (!InputSystem.IsInitialized)
            {
                InputSystem.Initialize(FlatRedBallServices.Game.Window);
            }

            InputSystem.MouseMove += MouseMoveHandler;
            InputSystem.MouseDown += MouseDownHandler;
            InputSystem.MouseUp += MouseUpHandler;
            InputSystem.FullKeyHandler += FullKeyHandler;

            FlatRedBallServices.Game.Window.ClientSizeChanged += ResizeWindow;
            _webView.DocumentReady += OnDocumentReady;
            _webView.ShowJavascriptDialog += OnJavascriptDialog;
        }

        private void OnJavascriptDialog(object sender, JavascriptDialogEventArgs e)
        {
            WinForms.MessageBox.Show(e.Message, "Javascript Pop-Up");
        }

        private void DisposeAwesomium()
        {
            AwesomiumWebView.Dispose();

            InputSystem.MouseMove -= MouseMoveHandler;
            InputSystem.MouseDown -= MouseDownHandler;
            InputSystem.MouseUp -= MouseUpHandler;
            InputSystem.FullKeyHandler -= FullKeyHandler;

            FlatRedBallServices.Game.Window.ClientSizeChanged -= ResizeWindow;
        }

        private void OnDocumentReady(object sender, EventArgs e)
        {
            _webView.DocumentReady -= OnDocumentReady;
            _entityJavascriptObject = _webView.CreateGlobalJavascriptObject("Entity");
        }

        private void UpdateAwesomium(object sender, EventArgs e)
        {
            RenderAwesomiumTexture();
        }

        private void DrawAwesomium(object sender, EventArgs e)
        {
            Microsoft.Xna.Framework.Rectangle destinationRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0,
                SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);

            _batch.Begin();

            _batch.Draw(_texture, destinationRectangle, Microsoft.Xna.Framework.Color.White);
            
            _batch.End();
        }

        private void ResizeWindow(object sender, EventArgs e)
        {
            RefreshAwesomiumDimensions();
        }

        private void SetUpAwesomiumDimensions()
        {
            _webView = WebCore.CreateWebView(SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);
            _webView.IsTransparent = true;
            _webView.Source = URI;
            _batch = new SpriteBatch(FlatRedBallServices.GraphicsDevice);

            _texture = new Texture2D(FlatRedBallServices.GraphicsDevice, SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);

            _webView.FocusView();
        }

        private void RefreshAwesomiumDimensions()
        {
            _webView.Width = SpriteManager.Camera.DestinationRectangle.Width;
            _webView.Height = SpriteManager.Camera.DestinationRectangle.Height;
            _texture = new Texture2D(FlatRedBallServices.GraphicsDevice, SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);
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
            JSObject window = AwesomiumWebView.ExecuteJavascriptWithResult("window");

            using (window)
            {
                window.InvokeAsync(callback, args);
            }

        }

        #endregion

    }
}
