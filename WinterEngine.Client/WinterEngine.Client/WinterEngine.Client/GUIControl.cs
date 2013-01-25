using System;
using System.Drawing;
using System.Drawing.Imaging;
using Awesomium.Core;
using FlatRedBall;
using Microsoft.Xna.Framework.Graphics;

namespace WinterEngine.Client
{
    //-------------------------------------------
    // THE GUICONTROL CLASS
    //-------------------------------------------
    // TODO:
    // Split functionality into two object types:
    // -an Awesomium object with as few dependencies as possible
    // -a FRB object (GuiEntity?) that wraps this for a really quick/easy usage
    public class GuiControl : IDisposable
    {
        WebView mView;
        Texture2D mTexture;
        Bitmap mFrameBuffer;
        Rectangle mDrawRect;
        int mWidth;
        int mHeight;
        byte[] mBytes;

        public Sprite Sprite { get; private set; }

        // TODO: overload that takes a string to the Source
        public GuiControl(int width, int height, Uri source) : this(width, height, source, new Sprite()) { }
        public GuiControl(int width, int height, Uri source, Sprite sprite) : this(width, height, true, source, sprite) { }
        public GuiControl(int width, int height, bool isTransparent, Uri source, Sprite sprite)
        {
            mWidth = width;
            mHeight = height;
            Sprite = sprite;
            mView = WebCore.CreateWebView(width, height);
            if (isTransparent)
            {
                mView.FlushAlpha = false;
                mView.IsTransparent = true;
            }
            mView.Source = source;
            SetUpDrawSurfaces();
        }

        // TODO: allow a forced rerender
        // TODO: look for more performance improvements w/out depending on specific GPU hardware
        // this needs to be called manually in the gameloop
        public void Update()
        {

            // only render if the view needs it and the texture still exists
            if (mView.IsDirty && !mTexture.IsDisposed)
            {
                // get the buffer from Awesomium
                RenderBuffer buffer = mView.Render();

                // create some bitmap data that we can draw to programmatically
                BitmapData bits = mFrameBuffer.LockBits(mDrawRect, ImageLockMode.ReadWrite, mFrameBuffer.PixelFormat);
                buffer.CopyTo(bits.Scan0, bits.Stride, 4, true);

                // create our pixel buffer
                mBytes = new byte[bits.Height * bits.Stride];

                // use interop to copy unmanaged memory into a managed type we can work with
                System.Runtime.InteropServices.Marshal.Copy(bits.Scan0, mBytes, 0, mBytes.Length);

                // unlock the bits and update texture
                mFrameBuffer.UnlockBits(bits);
                mTexture.SetData(mBytes);
            }
        }

        // prepare objects we need for drawing. If height
        private void SetUpDrawSurfaces()
        {
            mTexture = new Texture2D(FlatRedBallServices.GraphicsDevice, mWidth, mHeight);
            mFrameBuffer = new Bitmap(mWidth, mHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            mDrawRect = new Rectangle(0, 0, mWidth, mHeight);
            Sprite.Texture = mTexture;
        }

        public void Dispose()
        {
            mTexture.Dispose();
            mFrameBuffer.Dispose();
            Sprite.Texture = null;
            mTexture = null;
            mBytes = null;
        }

        // TODO: methods that could be useful
        // private void UpdateSource(URI newSource)
        // private void UpdateSize(int height, width)
        // private void ForceRedraw();
    }
    
}
