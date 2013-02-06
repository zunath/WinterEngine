﻿using System;
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
        private Cell _activeCell;
        private TilesetEditorModeTypeEnum _modeType;
        private Texture2D _notPassableTexture;
        private Texture2D _passableTexture;

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
        /// Gets or sets the active cell.
        /// </summary>
        public Cell ActiveCell
        {
            get { return _activeCell; }
            set { _activeCell = value; }
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
        protected override void Initialize()
        {
            _content = new ContentManager(Services);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //NotPassableTexture = Content.Load<Texture2D>("./TilesetEditor_NotPassable");
            //PassableTexture = Content.Load<Texture2D>("./TilesetEditor_Passable");
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
                EngineResourceFactory resourceFactory = new EngineResourceFactory();

                GraphicTexture = graphicFactory.GetSpriteSheet(Content, GraphicResource);

                if (GraphicTexture.Width % 64 > 0 || GraphicTexture.Height % 64 > 0)
                {
                    GraphicTexture = Content.Load<Texture2D>(resourceFactory.GetResourcePath(EngineResourceEnum.Icon_InvalidDimensions, false));
                }
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