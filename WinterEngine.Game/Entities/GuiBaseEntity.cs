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
using System.Windows.Forms;
using FlatRedBall.Gui;



#endif

namespace WinterEngine.Game.Entities
{
	public partial class GuiBaseEntity
    {
        #region Fields

        private WebView _webView;
        private Texture2D _texture;
        private Bitmap _frameBuffer;
        private Rectangle _drawRectangle;
        private byte[] _bytes;
        
        #endregion

        #region Properties

        public WebView AwesomiumWebView
        {
            get { return _webView; }
            set { _webView = value; }
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
            _webView = WebCore.CreateWebView(SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);
            _webView.IsTransparent = true;
            
            _webView.Source = URI;
            SetUpDrawSurfaces();
            // DEBUGGING
            _webView.FocusView();
            // END DEBUGGING

		}

		private void CustomActivity()
		{
            RenderAwesomiumTexture();
            ManageUserInput();
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
            _texture = new Texture2D(FlatRedBallServices.GraphicsDevice, SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);
            _frameBuffer = new Bitmap(SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _drawRectangle = new Rectangle(0, 0, SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);
            SpriteInstance.Texture = _texture;
        }

        private void RenderAwesomiumTexture()
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

        public JSObject RunJavaScriptMethod(string methodName)
        {
            return _webView.ExecuteJavascriptWithResult(methodName);
        }


        #endregion

        #region Input handling

        private void ManageUserInput()
        {
            if (!_webView.IsLoading && InputManager.Mouse.IsInGameWindow())
            {
                int relativeMouseX = (int)GuiManager.Cursor.ScreenX;
                int relativeMouseY = (int)GuiManager.Cursor.ScreenY;

                Rectangle windowRectangle = new Rectangle(0, 0, SpriteManager.Camera.DestinationRectangle.Width, SpriteManager.Camera.DestinationRectangle.Height);


                // Mouse down and scroll wheel events only fire if the mouse is within the bounds of the
                // GUI window.
                if (windowRectangle.Contains(relativeMouseX, relativeMouseY))
                {
                    _webView.InjectMouseMove(relativeMouseX, relativeMouseY);

                    // Left mouse button pushed down.
                    if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
                    {
                        _webView.InjectMouseDown(MouseButton.Left);
                    }
                    // Right mouse button pushed down
                    if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.RightButton))
                    {
                        _webView.InjectMouseDown(MouseButton.Right);
                    }

                    // Mouse wheel scroll - multiply by 10 because FRB values are very small.
                    _webView.InjectMouseWheel((int)InputManager.Mouse.ScrollWheel * 10, 0);
                }

                // Left mouse button released.
                if (InputManager.Mouse.ButtonReleased(Mouse.MouseButtons.LeftButton))
                {
                    _webView.InjectMouseUp(MouseButton.Left);
                }
                // Right mouse button released.
                if (InputManager.Mouse.ButtonReleased(Mouse.MouseButtons.RightButton))
                {
                    _webView.InjectMouseUp(MouseButton.Right);
                }


                // Keyboard entry handling
                
            }
        }

        public void FullKeyHandler(object sender, uint msg, IntPtr wParam, IntPtr lParam)
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

        #endregion

    }
}
