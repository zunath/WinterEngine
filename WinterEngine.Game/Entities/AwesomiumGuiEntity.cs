using System;
using FlatRedBall;
using FlatRedBall.Input;



#if FRB_XNA || SILVERLIGHT
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using System.Drawing;
using System.Drawing.Imaging;
using WinterEngine.UI.AwesomiumXNA;
using FlatRedBall.Math;



#endif

namespace WinterEngine.Game.Entities
{
	public partial class AwesomiumGuiEntity
    {
        #region Fields

        private WebView _webView;
        private Texture2D _texture;
        private Bitmap _frameBuffer;
        private Rectangle _drawRectangle;
        private byte[] _bytes;
        private bool _isLocalFile;
        
        #endregion

        #region Properties

        /// <summary>
        /// Returns the URI for the entity's resource path.
        /// </summary>
        public Uri URI
        {
            get { return new Uri(ResourcePath); }
        }

        /// <summary>
        /// Returns true if the resource path is for a local file.
        /// Returns false if the resource path is for an internet URL address.
        /// </summary>
        public bool IsLocalFile
        {
            get { return _isLocalFile; }
            set { _isLocalFile = value; }
        }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            // DEBUGGING
            SpriteManager.Camera.UsePixelCoordinates();
            ResourcePath = "file:///./Components/ServerList.html";
            Width = 800;
            Height = 600;

            this.X = 100;
            this.Y = 0;

            // END DEBUGGING

            _webView = WebCore.CreateWebView(Width, Height);
            if (IsTransparent)
            {
                _webView.IsTransparent = true;
            }
            _webView.Source = URI;
            SetUpDrawSurfaces();

            InitializeInputEventSubscriptions();

            // DEBUGGING

            _webView.FocusView();
            // END DEBUGGING

		}

		private void CustomActivity()
		{
            BitmapSurface surface = (BitmapSurface)_webView.Surface;

            // only render if the view needs it and the texture still exists
            if (!Object.ReferenceEquals(surface, null) && surface.IsDirty && !_texture.IsDisposed)
            {
                // create some bitmap data that we can draw to programmatically
                BitmapData bits = _frameBuffer.LockBits(_drawRectangle, ImageLockMode.ReadWrite, _frameBuffer.PixelFormat);
                
                surface.CopyTo(bits.Scan0, bits.Stride, 4, true, false);

                // create our pixel buffer
                _bytes = new byte[bits.Height * bits.Stride];

                // use interop to copy unmanaged memory into a managed type we can work with
                System.Runtime.InteropServices.Marshal.Copy(bits.Scan0, _bytes, 0, _bytes.Length);

                // unlock the bits and update texture
                _frameBuffer.UnlockBits(bits);
                _texture.SetData(_bytes);

                surface.IsDirty = true;
            }
		}

		private void CustomDestroy()
        {
            _texture.Dispose();
            _frameBuffer.Dispose();
            SpriteInstance.Texture = null;
            _texture = null;
            _bytes = null;
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        #endregion


        #region Rendering Methods

        private void SetUpDrawSurfaces()
        {
            _texture = new Texture2D(FlatRedBallServices.GraphicsDevice, Width, Height);
            _frameBuffer = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _drawRectangle = new Rectangle(0, 0, Width, Height);
            SpriteInstance.Texture = _texture;
        }

        public JSObject RunJavaScriptMethod(string methodName)
        {
            return _webView.ExecuteJavascriptWithResult(methodName);
        }

        #endregion

        #region Input handling

        private void InitializeInputEventSubscriptions()
        {
            InputSystem.Initialize(FlatRedBallServices.Game.Window);
            InputSystem.FullKeyHandler += FullKeyHandler;
            InputSystem.MouseMove += MouseMoveHandler;
            InputSystem.MouseDown += MouseDownHandler;
            InputSystem.MouseUp += MouseUpHandler;
        }

        public void FullKeyHandler(object sender, uint msg, IntPtr wParam, IntPtr lParam)
        {
            Modifiers modifiers = new Modifiers();
            WebKeyboardEvent keyEvent = new WebKeyboardEvent((uint)msg, (IntPtr)wParam, (IntPtr)lParam, modifiers);

            _webView.InjectKeyboardEvent(keyEvent);
        }

        public void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            _webView.InjectMouseMove(InputManager.Mouse.X, InputManager.Mouse.Y);
        }

        public void MouseDownHandler(object sender, MouseEventArgs e)
        {
            Console.WriteLine(InputManager.Mouse.WorldXAt(0.0f) + ", " + InputManager.Mouse.WorldYAt(0.0f));
            _webView.InjectMouseDown((Awesomium.Core.MouseButton)((int)e.Button - 1));
        }

        public void MouseUpHandler(object sender, MouseEventArgs e)
        {
            _webView.InjectMouseUp((Awesomium.Core.MouseButton)((int)e.Button - 1));
        }


        #endregion
    }
}
