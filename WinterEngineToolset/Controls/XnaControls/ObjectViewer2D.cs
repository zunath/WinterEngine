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
using WinterEngine.DataTransferObjects.Graphics;
using System.IO;
using System;
using WinterEngine.Library.Helpers;
using Ionic.Zip;

namespace WinterEngine.Toolset.Controls.XnaControls
{
    public class ObjectViewer2D : GraphicsDeviceControl
    {
        #region Variables
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private SpriteSheet _resource;
        private Texture2D _texture;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content manager used by this control.
        /// </summary>
        private ContentManager Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// Gets or sets the sprite batch used by this control.
        /// </summary>
        private SpriteBatch Batch
        {
            get { return _spriteBatch; }
            set { _spriteBatch = value; }
        }

        /// <summary>
        /// Gets or sets the graphic resource used by this control.
        /// </summary>
        public SpriteSheet Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        /// <summary>
        /// Gets or sets the 2D texture that's displayed.
        /// </summary>
        private Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        #endregion

        #region Overrides
        protected override void Initialize()
        {
            _content = new ContentManager(Services);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _content.Unload();

            base.Dispose(disposing);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Handles updating the XNA embedded window. Call this whenever the user performs an action
        /// such as clicking the window or scrolling the view area.
        /// </summary>
        private void XNAUpdate()
        {
            Invalidate();
        }

        /// <summary>
        /// Loads a graphic resource into the object viewer. 
        /// Resource should be a 2D graphic file such as a TGA, PNG, etc
        /// </summary>
        /// <param name="graphic"></param>
        public void LoadGraphic(SpriteSheet graphic)
        {
            string resourcePath = "";

            try
            {
                Cursor = Cursors.WaitCursor;

                // Unload any existing model.
                Texture = null;
                Content.Unload();

                using (ZipFile zipFile = new ZipFile(graphic.ResourcePackagePath))
                {
                    resourcePath = "./" + graphic.ResourceFileName;

                    ZipEntry entry = zipFile[graphic.ResourceFileName];
                    entry.Extract();
                    Texture = Content.Load<Texture2D>(Path.GetFileNameWithoutExtension(resourcePath));
                    File.Delete("./" + entry.FileName);
                }
                Resource = graphic;

                // Refresh the panel with the new graphic.
                Invalidate();

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error loading graphic file.", ex);
            }
        }


        /// <summary>
        /// Handles drawing to the editor control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.LightGray);

            Batch.Begin();

            if (!object.ReferenceEquals(Texture, null))
            {
                Rectangle destination = new Rectangle(0, 0, this.Width, this.Height);
                Batch.Draw(Texture, destination, Color.White);
            }

            // End the sprite batch
            Batch.End();
        }

        #endregion
    }
}
