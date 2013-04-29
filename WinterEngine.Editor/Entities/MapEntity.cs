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


#endif

namespace WinterEngine.Editor.Entities
{
	public partial class MapEntity
    {
        #region Fields

        private Area _activeArea;
        private MapDrawableBatch _mapBatch;

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

        #endregion

        #region FRB Events

        private void CustomInitialize()
		{
            // DEBUGGING

            ActiveArea = new Area();
            ActiveArea.TileMap = new Map();
            ActiveArea.TileMap.Tiles = new Tile[20, 20];

            // END DEBUGGING


            Texture2D texture = FlatRedBallServices.Load<Texture2D>("testts.png");
            //SpriteManager.Camera.UsePixelCoordinates();

            MapBatch = new MapDrawableBatch(TileMap.NumberOfTilesHigh * TileMap.NumberOfTilesWide,
                (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight, texture);



            // Traditional Y axis behaviour. As Y increases, you move UP.
            for (int y = 0; y < TileMap.NumberOfTilesHigh; y++)
            {
                // Tiles are drawn backwards for X coordinates to prevent overlapping issues.
                for (int x = TileMap.NumberOfTilesWide - 1; x >= 0; x--)
                {
                    // Each tile must step 32 pixels left and 16 pixels up. 
                    // Tiles are in dimensions of 64x64.
                    int xPosition = ((x * (int)MappingEnum.TileWidth) + (y * (int)MappingEnum.TileWidth)) / 2;
                    int yPosition = ((y * (int)MappingEnum.TileHeight) - (x * (int)MappingEnum.TileHeight) ) / 4;

                    Vector3 bottomLeftPoint = new Vector3(xPosition, yPosition, 0);

                    MapBatch.AddTile(bottomLeftPoint, new Vector2((int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight),
                        0, 0, (int)MappingEnum.TileWidth, (int)MappingEnum.TileHeight);
                }
            }


            MapBatch.AddToManagers();

            
		}

        private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

    }
}
