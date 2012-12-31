using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using System.Windows.Forms;
using WinterEngine.Toolset.Controls.XnaControls.Shared;

namespace WinterEngine.Toolset.Controls.XnaControls
{
    public class ObjectViewer3D : GraphicsDeviceControl
    {
        #region Variables
        ContentManager content;
        SpriteBatch spriteBatch;
        
        #endregion

        #region Properties

        #endregion

        #region Overrides
        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadContent();
        }

        private void LoadContent()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                content.Unload();

            base.Dispose(disposing);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Handles updating the XNA embedded window. Call this whenever the user performs an action
        /// such as clicking the window or scrolling the view area.
        /// </summary>
        internal void XNAUpdate()
        {
            // Invalidating this control causes the Draw() method to fire.
            // I found out that you can't simply call Draw() because it doesn't actually repaint.
            // So basically the changes were taking effect but they weren't being drawn to the screen.
            // Go figure.
            Invalidate();
        }

        /// <summary>
        /// Handles drawing to the editor control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();

            
            // End the sprite batch
            spriteBatch.End();

        }

        #endregion
    }
}
