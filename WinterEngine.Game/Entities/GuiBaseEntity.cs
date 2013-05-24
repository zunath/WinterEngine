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
using WinterEngine.Network.Entities;
using FlatRedBall.Gui;
using WinterEngine.Game.Services;
using Microsoft.Xna.Framework.Graphics;
using AwesomiumXNA;
using FlatRedBall.Graphics;


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
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        #endregion

        #region Methods

        #endregion

        #region Rendering Methods

        private void RenderAwesomiumTexture()
        {
            BitmapSurface surface = (BitmapSurface)_webView.Surface;

            if (surface != null)
            {
                surface.RenderTexture2D(_texture);
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
            _webView.InjectMouseMove(e.X, e.Y);
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            _webView.InjectMouseDown((Awesomium.Core.MouseButton)((int)e.Button - 1));
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            _webView.InjectMouseUp((Awesomium.Core.MouseButton)((int)e.Button - 1));
        }

        private void FullKeyHandler(object sender, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (!_webView.IsLoading)
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

    }
}
