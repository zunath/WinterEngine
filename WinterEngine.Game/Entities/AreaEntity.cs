using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.TileGraphics;
using Microsoft.Xna.Framework;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Utility;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Vector3 = Microsoft.Xna.Framework.Vector3;


namespace WinterEngine.Game.Entities
{
	public partial class AreaEntity
    {
        #region Fields

        private Texture2D _mapSpriteSheet;
        private GraphicHelper _graphicHelper;

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

        #endregion

        #region FRB Events
        private void CustomInitialize()
		{
            EditorSpritesheet = FlatRedBallServices.Load<Texture2D>("content/Editor/Icons/TilesetEditor_CellSpriteSheet.png");
        }

        private void CustomActivity()
		{
            if (InputManager.Mouse.IsInGameWindow() && !Object.ReferenceEquals(ActiveAreaBatch, null))
            {
                if (InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton))
                {

                    Vector2 currentTile = GetTileCoordinatesFromMouseCoordinates();

                    // NOTE: This version of PaintTile seems to be bugged. Look at 
                    // using the other overloaded method. Victor says that one should work.
                    //MapBatch.PaintTile((int)currentTile.X, (int)currentTile.Y, 1);
                    
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

        /// <summary>
        /// Returns the X coordinate of the next tile
        /// </summary>
        /// <param name="x">The current X position in the map.</param>
        /// <param name="y">The current Y position in the map.</param>
        /// <returns></returns>
        private int GetTileXScreenCoordinate(int x, int y)
        {
            return ((x * (int)MappingEnum.TileWidth) - (y * (int)MappingEnum.TileWidth)) / 2;
        }

        /// <summary>
        /// Returns the Y coordinate of the next tile.
        /// </summary>
        /// <param name="x">The current X position in the map.</param>
        /// <param name="y">The current Y position in the map.</param>
        /// <returns></returns>
        private int GetTileYScreenCoordinate(int x, int y)
        {
            return ((y * (int)MappingEnum.TileHeight) + (x * (int)MappingEnum.TileHeight)) / 4;
        }

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

            EmptyAreaBatch = new MapDrawableBatch(ActiveArea.TilesHigh * ActiveArea.TilesWide,
                (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight, EditorSpritesheet);

            //ActiveMapBatch = new MapDrawableBatch(ActiveMap.TilesHigh * ActiveMap.TilesWide,
            //    (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight, MapSpriteSheet);

            InitializeMapTiles(true);
            //InitializeMapTiles(false);
        }

        private void UnloadArea()
        {
            if (ActiveAreaBatch != null)
            {
                ActiveAreaBatch.RemoveSelfFromListsBelongingTo();
            }

            if (EmptyAreaBatch != null)
            {
                EmptyAreaBatch.RemoveSelfFromListsBelongingTo();
            }

            ActiveArea = null;
            ActiveAreaBatch = null;
            AreaSpriteSheet = null;
        }

        private void InitializeMapTiles(bool doEmptyMap)
        {
            // Traditional Y axis behaviour. As Y increases, you move UP.
            //for (int y = 0; y < TileMap.NumberOfTilesHigh; y++)
            for (int x = 0; x < ActiveArea.TilesWide; x++)
            {
                // Tiles are drawn backwards for X coordinates to prevent overlapping issues.
                //for (int x = 0; x < TileMap.NumberOfTilesWide; x++)
                for (int y = 0; y < ActiveArea.TilesHigh; y++)
                {
                    // Each tile must step 32 pixels left and 16 pixels up. 
                    // Tiles are in dimensions of 64x64.
                    int xPosition = GetTileXScreenCoordinate(x, y);
                    int yPosition = GetTileYScreenCoordinate(x, y);

                    Vector2 dimensions = new Vector2((int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                    Vector3 bottomLeftPoint = new Vector3(xPosition, yPosition, 0);
                    if (doEmptyMap)
                    {
                        EmptyAreaBatch.AddTile(bottomLeftPoint, dimensions, 0, 0, (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                    }
                    else
                    {
                        ActiveAreaBatch.AddTile(bottomLeftPoint, dimensions, 0, 0, (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                    }
                    
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
            int mouseX = (int)InputManager.Mouse.WorldXAt(0);
            int mouseY = (int)InputManager.Mouse.WorldYAt(0);

            //int tileX = mouseX / (int)MappingEnum.TileWidth;
            //int tileY = mouseY / (int)MappingEnum.TileHeight;

            int tileX = mouseX / 64;
            int tileY = mouseY / 64;

            if (tileX > ActiveArea.TilesWide)
            {
                tileX = ActiveArea.TilesWide;
            }
            else if (tileX < ActiveAreaBatch.X)
            {
                tileX = (int)ActiveAreaBatch.X;
            }

            if (tileY > ActiveArea.TilesHigh)
            {
                tileY = ActiveArea.TilesHigh;
            }
            else if (tileY < ActiveAreaBatch.Y)
            {
                tileY = (int)ActiveAreaBatch.Y;
            }

            return new Vector2(tileX, tileY);
            
        }

        #endregion


    }
}
