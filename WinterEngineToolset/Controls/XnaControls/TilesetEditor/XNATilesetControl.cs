using System;
using System.IO;
using System.Windows.Forms;
using Ionic.Zip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Graphics;
using WinterEngine.DataTransferObjects.Mapping;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Helpers;
using WinterEngine.Toolset.Controls.XnaControls.Shared;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset.Controls.XnaControls.TilesetEditor
{
    public partial class XNATilesetControl : GraphicsDeviceControl
    {
        #region Fields
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private SpriteSheet _spriteSheet;
        private Texture2D _texture;
        private Cell _selectedTile;
        private TilesetEditorModeTypeEnum _modeType;
        private Texture2D _notPassableTexture;
        private Texture2D _passableTexture;
        private Texture2D _selectedTileTexture;

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

        /// <summary>
        /// Gets or sets the active tile.
        /// </summary>
        public Cell SelectedTile
        {
            get { return _selectedTile; }
            set { _selectedTile = value; }
        }

        /// <summary>
        /// Gets or sets the current mode of the tileset control.
        /// </summary>
        public TilesetEditorModeTypeEnum Mode
        {
            get { return _modeType; }
            set { _modeType = value; }
        }

        /// <summary>
        /// Gets or sets the texture used for the "Non-Passable" icon.
        /// </summary>
        private Texture2D NotPassableTexture
        {
            get { return _notPassableTexture; }
            set { _notPassableTexture = value; }
        }

        /// <summary>
        /// Gets or sets the texture used for the "Passable" icon.
        /// </summary>
        private Texture2D PassableTexture
        {
            get { return _passableTexture; }
            set { _passableTexture = value; }
        }

        /// <summary>
        /// Gets or sets the texture used for the selected tileset icon.
        /// </summary>
        private Texture2D SelectedTileTexture
        {
            get { return _selectedTileTexture; }
            set { _selectedTileTexture = value; }
        }

        #endregion

        #region Constructors

        public XNATilesetControl()
        {
            InitializeComponent();

            // Invalidating the control will redraw graphics in the window.
            Application.Idle += delegate { Invalidate(); };
        }

        #endregion

        #region Events / Delegates

        #endregion

        #region Overrides

        /// <summary>
        /// Handles initializing the ContentManager and the SpriteBatch objects.
        /// </summary>
        protected override void Initialize()
        {
            _content = new ContentManager(Services);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Handles loading icon textures into memory and initialization of other variables.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            EngineResourceFactory resourceFactory = new EngineResourceFactory();

            // Load icons into memory.
            NotPassableTexture = Content.Load<Texture2D>(resourceFactory.GetResourcePath(EngineResourceEnum.Icon_NotPassable, false));
            PassableTexture = Content.Load<Texture2D>(resourceFactory.GetResourcePath(EngineResourceEnum.Icon_Passable, false));
            SelectedTileTexture = Content.Load<Texture2D>(resourceFactory.GetResourcePath(EngineResourceEnum.Icon_SelectedTile, false));

            // Create a new cell at position 0, 0
            SelectedTile = new Cell();
            SelectedTile.CellX = 0;
            SelectedTile.CellY = 0;

        }

        /// <summary>
        /// Handles changing the selected cell.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectedTile.CellX = e.X / SelectedTile.MaxTileWidth;
                SelectedTile.CellY = e.Y / SelectedTile.MaxTileHeight;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the graphic displayed in the XNA window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeTileset(object sender, TilesetEditorEventArgs e)
        {
            GraphicResource = e.SpriteSheet;
        }

        /// <summary>
        /// Unloads the tileset in the XNA control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UnloadTileset(object sender, EventArgs e)
        {
            GraphicResource = null;
        }

        private void LoadSpriteSheet()
        {
            if (!Object.ReferenceEquals(GraphicResource, null))
            {
                GraphicFactory graphicFactory = new GraphicFactory();
                GraphicTexture = graphicFactory.GetSpriteSheet(Content, GraphicResource);
            }
        }


        /// <summary>
        /// Handles drawing to the editor control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.LightGray);

            Batch.Begin();

            // If valid, display the tileset sprite sheet.
            if (!Object.ReferenceEquals(GraphicResource, null))
            {
                Vector2 origin = new Vector2(0, 0);
                Rectangle destination = new Rectangle(0, 0, GraphicTexture.Width, GraphicTexture.Height);
                Rectangle source = new Rectangle(0, 0, GraphicTexture.Width, GraphicTexture.Height);
                
                Batch.Draw(GraphicTexture, destination, source, Color.White, 0.0f, origin, SpriteEffects.None, 0.0f);

                // Display the selected cell, if available
                if (!Object.ReferenceEquals(SelectedTile, null))
                {
                    destination = new Rectangle(SelectedTile.TileX, SelectedTile.TileY, SelectedTile.MaxTileWidth, SelectedTile.MaxTileHeight);
                    source = new Rectangle(0, 0, SelectedTile.MaxTileWidth, SelectedTile.MaxTileHeight);
                    Batch.Draw(SelectedTileTexture, destination, source, Color.White, 0.0f, origin, SpriteEffects.None, 0.0f);
                }

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
