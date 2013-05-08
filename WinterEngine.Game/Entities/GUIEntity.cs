using System;
using FlatRedBall;
using FlatRedBall.Input;



#if FRB_XNA || SILVERLIGHT
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Awesomium.Core;
using System.Drawing;
using System.Drawing.Imaging;



#endif

namespace WinterEngine.Game.Entities
{
	public partial class GUIEntity
    {
        #region Fields

        WebView mView;
        Texture2D mTexture;
        Bitmap mFrameBuffer;
        Rectangle mDrawRect;
        byte[] mBytes;
        #endregion

        #region Properties

        public Uri URI
        {
            get { return new Uri(ResourcePath); }
        }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            // DEBUGGING

            ResourcePath = "http://www.google.com";
            Width = 800;
            Height = 600;

            // END DEBUGGING

            mView = WebCore.CreateWebView(Width, Height);
            if (IsTransparent)
            {
                mView.IsTransparent = true;
            }
            mView.Source = URI;
            SetUpDrawSurfaces();

            // DEBUGGING

            mView.FocusView();

            // END DEBUGGING

		}

		private void CustomActivity()
		{
            BitmapSurface surface = (BitmapSurface)mView.Surface;

            // only render if the view needs it and the texture still exists
            if (!Object.ReferenceEquals(surface, null) && surface.IsDirty && !mTexture.IsDisposed)
            {
                HandleInput();

                // create some bitmap data that we can draw to programmatically
                BitmapData bits = mFrameBuffer.LockBits(mDrawRect, ImageLockMode.ReadWrite, mFrameBuffer.PixelFormat);
                
                surface.CopyTo(bits.Scan0, bits.Stride, 4, true, false);

                // create our pixel buffer
                mBytes = new byte[bits.Height * bits.Stride];

                // use interop to copy unmanaged memory into a managed type we can work with
                System.Runtime.InteropServices.Marshal.Copy(bits.Scan0, mBytes, 0, mBytes.Length);

                // unlock the bits and update texture
                mFrameBuffer.UnlockBits(bits);
                mTexture.SetData(mBytes);

                surface.IsDirty = true;
            }
		}

		private void CustomDestroy()
        {
            mTexture.Dispose();
            mFrameBuffer.Dispose();
            SpriteInstance.Texture = null;
            mTexture = null;
            mBytes = null;
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        #endregion


        #region Methods

        // prepare objects we need for drawing. If height
        private void SetUpDrawSurfaces()
        {
            mTexture = new Texture2D(FlatRedBallServices.GraphicsDevice, Width, Height);
            mFrameBuffer = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            mDrawRect = new Rectangle(0, 0, Width, Height);
            SpriteInstance.Texture = mTexture;
        }



        private void HandleInput()
        {
            mView.InjectMouseMove(InputManager.Mouse.X, InputManager.Mouse.Y);

            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
            {
                mView.InjectMouseDown(MouseButton.Left);
                Console.WriteLine(InputManager.Mouse.X + ", " + InputManager.Mouse.Y);
                
            }
            if (InputManager.Mouse.ButtonReleased(Mouse.MouseButtons.LeftButton))
            {
                mView.InjectMouseUp(MouseButton.Left);
            }

        }


        #endregion
    }
}
