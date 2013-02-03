using System;
using System.IO;
using System.Windows.Forms;
using Ionic.Zip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.DataTransferObjects.Graphics;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Helpers;
using WinterEngine.Toolset.Controls.XnaControls.Shared;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset.Controls.XnaControls
{
    public partial class XNATilesetControl : GraphicsDeviceControl
    {
        #region Fields
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private SpriteSheet _spriteSheet;
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
        public SpriteSheet GraphicResource
        {
            get { return _spriteSheet; }
            set 
            {
                _spriteSheet = value;
                LoadSpriteSheet();
            }
        }

        /// <summary>
        /// Gets or sets the 2D texture that's displayed.
        /// </summary>
        private Texture2D GraphicTexture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        #endregion

        #region Constructors

        public XNATilesetControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events / Delegates

        #endregion

        #region Overrides
        protected override void Initialize()
        {
            _content = new ContentManager(Services);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        #endregion

        #region Methods

        public void ChangeTileset(object sender, TilesetEventArgs e)
        {
            GraphicResource = e.Tileset.TilesetSpriteSheet;
        }

        private void LoadSpriteSheet()
        {
            if (!Object.ReferenceEquals(GraphicResource, null))
            {
                GraphicFactory factory = new GraphicFactory();
                GraphicTexture = factory.GetSpriteSheet(Content, GraphicResource);
                Invalidate();
            }
        }

        /// <summary>
        /// Handles drawing to the editor control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.LightGray);

            Batch.Begin();

            if (!Object.ReferenceEquals(GraphicResource, null))
            {
                Vector2 origin = new Vector2(0, 0);
                Rectangle destination = new Rectangle(0, 0, GraphicTexture.Width, GraphicTexture.Height);
                Rectangle source = new Rectangle(0, 0, GraphicTexture.Width, GraphicTexture.Height);
                
                Batch.Draw(GraphicTexture, destination, source, Color.White, 0.0f, origin, SpriteEffects.None, 0.0f);
            }

            Batch.End();
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _content.Unload();

            base.Dispose(disposing);
        }

        #endregion

    }
}
