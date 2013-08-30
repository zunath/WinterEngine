using System;
using System.Collections.Generic;
using Winforms = System.Windows.Forms;
using FlatRedBall;
using FlatRedBall.Gui;
using FlatRedBall.Input;
using FlatRedBall.TileGraphics;
using Microsoft.Xna.Framework;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Utility;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using WinterEngine.Library.Extensions;


namespace WinterEngine.Game.Entities
{
	public partial class AreaEntity
    {
        #region Fields

        private Texture2D _mapSpriteSheet;
        private GraphicHelper _graphicHelper;
        private Dictionary<Vector2, int> _tileLookupDictionary;

        #endregion

        #region Properties

        /// <summary>
        /// Currently displayed area.
        /// </summary>
        private Area ActiveArea { get; set; }

        /// <summary>
        /// Map batch used for empty cells.
        /// </summary>
        private MapDrawableBatch EmptyAreaBatch { get; set; }

        /// <summary>
        /// Map batch used for the active area.
        /// </summary>
        private MapDrawableBatch ActiveAreaBatch { get; set; }

        /// <summary>
        /// Spritesheet texture for empty/active cell sprites.
        /// </summary>
        private Texture2D EditorSpritesheet { get; set; }

        private GraphicHelper GraphicHelper
        {
            get
            {
                if (_graphicHelper == null)
                {
                    _graphicHelper = new GraphicHelper();
                }

                return _graphicHelper;
            }
        }

        /// <summary>
        /// Spritesheet texture for the active area.
        /// </summary>
        private Texture2D AreaSpriteSheet 
        {
            get
            {
                if (_mapSpriteSheet == null)
                {
                    _mapSpriteSheet = GraphicHelper.ContentPackageResourceToTexture2D(ActiveArea.GraphicResource);
                }

                return _mapSpriteSheet;
            }
            set
            {
                _mapSpriteSheet = value;
            }
        }

        private Dictionary<Vector2, int> TileLookupDictionary 
        {
            get
            {
                if (_tileLookupDictionary == null)
                {
                    _tileLookupDictionary = new Dictionary<Vector2, int>();
                }

                return _tileLookupDictionary;
            }
            set
            {
                _tileLookupDictionary = value;
            }
        }

        #endregion

        #region FRB Events
        private void CustomInitialize()
		{
            EditorSpritesheet = FlatRedBallServices.Load<Texture2D>("content/Editor/Icons/TilesetEditor_CellSpriteSheet.png");
        }

        private void CustomActivity()
		{
            //if (InputManager.Mouse.IsInGameWindow() && !Object.ReferenceEquals(ActiveAreaBatch, null))
            if (InputManager.Mouse.IsInGameWindow() && !Object.ReferenceEquals(EmptyAreaBatch, null))
            {
                if (InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton))
                {

                    Vector2 currentTile = GetTileCoordinatesFromMouseCoordinates();

                    // NOTE: This version of PaintTile seems to be bugged. Look at 
                    // using the other overloaded method. Victor says that one should work.
                    //MapBatch.PaintTile((int)currentTile.X, (int)currentTile.Y, 1);

                    int index = GetTileIndexByAreaCellPosition((int)currentTile.X, (int)currentTile.Y);

                    //Console.WriteLine("i = " + index + ", (" + currentTile.X + ", " + currentTile.Y + ")");
                    
                    //EmptyAreaBatch.PaintTile(index, 0);

                    EmptyAreaBatch.PaintTileTextureCoordinates(index, 64, 0);

                }
            }
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Methods

        public void ChangeArea(Area activeArea)
        {
            try
            {
                UnloadArea();
                ActiveArea = activeArea;
                LoadArea();
            }
            catch
            {
                throw;
            }
        }

        private void LoadArea()
        {
            if (ActiveArea == null) return;

            // DEBUG
            AreaSpriteSheet = FlatRedBallServices.Load<Texture2D>("content/Game/(Tileset) Wilderness.png");
            // END DEBUG

            EmptyAreaBatch = new MapDrawableBatch(ActiveArea.TilesHigh * ActiveArea.TilesWide,
                (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight, EditorSpritesheet);

            ActiveAreaBatch = new MapDrawableBatch(ActiveArea.TilesHigh * ActiveArea.TilesWide,
                (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight, AreaSpriteSheet);

            //InitializeMapTiles(true);
            InitializeMapTiles(false);
        }

        private void UnloadArea()
        {
            if (ActiveAreaBatch != null)
            {
                SpriteManager.RemoveDrawableBatch(ActiveAreaBatch);
            }

            if (EmptyAreaBatch != null)
            {
                SpriteManager.RemoveDrawableBatch(EmptyAreaBatch);
            }

            TileLookupDictionary.Clear();
            ActiveArea = null;
            ActiveAreaBatch = null;
            AreaSpriteSheet = null;
        }

        private void InitializeMapTiles(bool doEmptyMap)
        {
            int index = 0;

            for (int y = 0; y < ActiveArea.TilesHigh; y++)
            {
                //for (int y = ActiveArea.TilesHigh - 1; y >= 0; y--)
                for (int x = ActiveArea.TilesWide - 1; x >= 0; x--)
                {
                    int xPosition = (y * (int)MappingEnum.TileWidth / 2) + (x * (int)MappingEnum.TileWidth / 2);
                    int yPosition = (x * (int)MappingEnum.TileHeight / 4) - (y * (int)MappingEnum.TileHeight / 4);   

                    Vector2 dimensions = new Vector2((int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                    Vector3 bottomLeftPoint = new Vector3(xPosition, yPosition, 0);

                    if (doEmptyMap)
                    {
                        EmptyAreaBatch.AddTile(bottomLeftPoint, dimensions, 0, 0, (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                        TileLookupDictionary.Add(new Vector2(x, y), index);
                    }
                    else
                    {
                        int texX = 64 * 4;
                        int texY = 64 * 3;
                        ActiveAreaBatch.AddTile(bottomLeftPoint, dimensions, texX, texY, (int)MappingEnum.TileWidth + texX, (int)MappingEnum.TileHeight + texY);
                        TileLookupDictionary.Add(new Vector2(x, y), index);
                    }

                    index++;
                }
            }

            if (doEmptyMap)
            {
                EmptyAreaBatch.AddToManagers();
            }
            else
            {
                ActiveAreaBatch.AddToManagers();
            }
        }

        private Vector2 GetTileCoordinatesFromMouseCoordinates()
        {
            int mouseWorldX = (int)InputManager.Mouse.WorldXAt(0);
            int mouseWorldY = (int)InputManager.Mouse.WorldYAt(0);

            int tileX = (mouseWorldY * (int)MappingEnum.TileWidth / 2) + (mouseWorldX * (int)MappingEnum.TileWidth / 2);
            int tileY = (mouseWorldX * (int)MappingEnum.TileHeight / 4) - (mouseWorldY * (int)MappingEnum.TileHeight / 4);

            
            //int tileX = (2 * mouseWorldX - 4 * mouseWorldY) / (int)MappingEnum.TileWidth / 2; 
            //int tileY = (mouseWorldX * 2 / (int)MappingEnum.TileWidth) - tileX;

            if (tileX < 0) tileX = 0;
            else if (tileX > ActiveArea.TilesWide - 1) tileX = ActiveArea.TilesWide - 1;

            if (tileY < 0) tileY = 0;
            else if (tileY > ActiveArea.TilesHigh - 1) tileY = ActiveArea.TilesHigh - 1;

            return new Vector2(tileX, tileY);
            
        }

        private int GetTileIndexByAreaCellPosition(int x, int y)
        {
            return TileLookupDictionary[new Vector2(x, y)];
        }

        #endregion

    }
}
