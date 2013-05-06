using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects;
using System.IO;
using WinterEngine.Editor.Views;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.TileGraphics;
using Microsoft.Xna.Framework;
using FlatRedBall.Graphics;
using System.Windows;
using WinterEngine.DataTransferObjects.EventArgsExtended;


#endif

namespace WinterEngine.Editor.Entities
{
	public partial class MapEntity
    {
        #region Fields

        private Area _activeArea;
        private MapDrawableBatch _mapBatch;
        private Texture2D _editorSpritesheet;

        #endregion

        #region Properties

        public Area ActiveArea
        {
            get { return _activeArea; }
            set { _activeArea = value; }
        }

        private Map TileMap
        {
            get { return _activeArea.TileMap; }
        }

        private Tile[,] Tiles
        {
            get { return _activeArea.TileMap.Tiles; }
        }

        private MapDrawableBatch MapBatch
        {
            get { return _mapBatch; }
            set { _mapBatch = value; }
        }

        private Texture2D EditorSpritesheet
        {
            get { return _editorSpritesheet; }
            set { _editorSpritesheet = value; }
        }

        #endregion

        #region FRB Events
        private void CustomInitialize()
		{

            Area area =  new Area();
            ActiveArea = area;

            // DEBUGGING
            ActiveArea.TileMap = new Map();
            ActiveArea.TileMap.Tiles = new Tile[2, 2];

            // END DEBUGGING

            //EditorSpritesheet = FlatRedBallServices.Load<Texture2D>("content/Editor/Icons/TilesetEditor_CellSpriteSheet.png");
            EditorSpritesheet = FlatRedBallServices.Load<Texture2D>("content/Editor/Icons/testset.png");
            //LoadMap();
            LoadMapTest();
		}

        private void CustomActivity()
		{
            if (InputManager.Mouse.IsInGameWindow() && !Object.ReferenceEquals(MapBatch, null))
            {
                if (InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton))
                {

                    Vector2 currentTile = GetTileCoordinatesFromMouseCoordinates();

                    // NOTE: This version of PaintTile seems to be bugged. Look at 
                    // using the other overloaded method. Victor says that one should work.
                    MapBatch.PaintTile((int)currentTile.X, (int)currentTile.Y, 1);
                    
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

        #region Event Handling

        /// <summary>
        /// Fires when a tile is selected from the AreaPropertiesControl.
        /// This should be subscribed to the OnTileSelected event from that control in the EditorScreen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TileSelected(object sender, PositionEventArgs e)
        {

        }

        public void AreaLoaded(object sender, GameObjectEventArgs e)
        {
            Area area = e.GameObject as Area;
            ActiveArea = area;

            // DEBUGGING
            ActiveArea.TileMap = new Map();
            ActiveArea.TileMap.Tiles = new Tile[5, 5];

            // END DEBUGGING
            
            LoadMap();

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

        private void LoadMapTest()
        {
            int numberOfTilesWide = 2;
            int numberOfTilesHigh = 2;
            int spriteSheetCellWidth = 64;
            int spriteSheetCellHeight = 64;

            MapBatch = new FlatRedBall.TileGraphics.MapDrawableBatch(
                numberOfTilesWide * numberOfTilesHigh,
                spriteSheetCellWidth,
                spriteSheetCellHeight, EditorSpritesheet);

            for (int x = 0; x < numberOfTilesWide; x++)
            {
                for (int y = 0; y < numberOfTilesHigh; y++)
                {
                    Vector3 bottomLeftPoint = new Vector3(
                        x * spriteSheetCellWidth,
                        y * spriteSheetCellHeight,
                        0);

                    int cellLeft = 0;
                    int cellTop = 0;
                    int cellRight = cellLeft + spriteSheetCellWidth;
                    int cellBottom = cellTop + spriteSheetCellHeight;

                    MapBatch.AddTile(bottomLeftPoint, new Vector2(spriteSheetCellWidth, spriteSheetCellHeight),
                        cellLeft, cellTop, cellRight, cellBottom);
                }
            }
            MapBatch.AddToManagers();
        }

        public void LoadMap()
        {
            MapBatch = new MapDrawableBatch(TileMap.NumberOfTilesHigh * TileMap.NumberOfTilesWide,
                (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight, EditorSpritesheet);

            // Traditional Y axis behaviour. As Y increases, you move UP.
            //for (int y = 0; y < TileMap.NumberOfTilesHigh; y++)
            for (int x = 0; x < TileMap.NumberOfTilesWide; x++)
            {
                // Tiles are drawn backwards for X coordinates to prevent overlapping issues.
                //for (int x = 0; x < TileMap.NumberOfTilesWide; x++)
                for (int y = 0; y < TileMap.NumberOfTilesHigh; y++)
                {
                    // Each tile must step 32 pixels left and 16 pixels up. 
                    // Tiles are in dimensions of 64x64.
                    int xPosition = GetTileXScreenCoordinate(x, y);
                    int yPosition = GetTileYScreenCoordinate(x, y);

                    Vector2 dimensions = new Vector2((int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                    Vector3 bottomLeftPoint = new Vector3(xPosition, yPosition, 0);

                    MapBatch.AddTile(bottomLeftPoint, dimensions, 0, 0, (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                    
                }
            }

            MapBatch.AddToManagers();
        }

        private Vector2 GetTileCoordinatesFromMouseCoordinates()
        {
            int mouseX = (int)InputManager.Mouse.WorldXAt(0);
            int mouseY = (int)InputManager.Mouse.WorldYAt(0);

            //int tileX = mouseX / (int)MappingEnum.TileWidth;
            //int tileY = mouseY / (int)MappingEnum.TileHeight;

            int tileX = mouseX / 64;
            int tileY = mouseY / 64;

            if (tileX > TileMap.NumberOfTilesWide)
            {
                //tileX = TileMap.NumberOfTilesWide;
            }
            else if (tileX < MapBatch.X)
            {
                //tileX = (int)MapBatch.X;
            }

            if (tileY > TileMap.NumberOfTilesHigh)
            {
                //tileY = TileMap.NumberOfTilesHigh;
            }
            else if (tileY < MapBatch.Y)
            {
                //tileY = (int)MapBatch.Y;
            }

            return new Vector2(tileX, tileY);
        }

        #endregion


    }
}
