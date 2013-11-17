using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.Game.Entities;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataAccess.Repositories;
using System.Linq;

namespace WinterEngine.Game.Screens
{
	public partial class ToolsetScreen
    {
        private readonly IRepositoryFactory _repositoryFactory;

        #region Constants

        const int MaxZoomDistance = 1000;
        const int MinZoomDistance = 100;

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            SpriteManager.Camera.Z = MaxZoomDistance; // Initial zoom distance
            BindEvents();
		}

		private void CustomActivity(bool firstTimeCalled)
		{
            HandleUserInput();

		}

		private void CustomDestroy()
		{
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Event Handling

        private void BindEvents()
        {
            ToolsetUIEntityInstance.OnChangeScreen += base.ChangeScreen;
            
            // Changed object mode event
            ToolsetUIEntityInstance.OnObjectModeChanged += HandleModeChangeEvent;

            // Area Editor
            ToolsetUIEntityInstance.OnAreaLoaded += HandleAreaLoadEvent;
        
            // Tileset Editor
            ToolsetUIEntityInstance.OnTilesetLoaded += TilesetEditorEntityInstance.HandleLoadTilesetSpritesheetEvent;

            // Object save events
            ToolsetUIEntityInstance.OnSaveArea += SaveArea;
            ToolsetUIEntityInstance.OnSaveConversation += SaveConversation;
            ToolsetUIEntityInstance.OnSaveCreature += SaveCreature;
            ToolsetUIEntityInstance.OnSaveItem += SaveItem;
            ToolsetUIEntityInstance.OnSavePlaceable += SavePlaceable;
            ToolsetUIEntityInstance.OnSaveScript += SaveScript;
            ToolsetUIEntityInstance.OnSaveTileset += SaveTileset;

        }

        private void HandleModeChangeEvent(object sender, ObjectModeChangedEventArgs e)
        {
            HideAllEditors();
            if (e.GameObjectType == GameObjectTypeEnum.Area)
            {
                AreaEntityInstance.ShowEntity();
            }
            else if (e.GameObjectType == GameObjectTypeEnum.Tileset)
            {
                TilesetEditorEntityInstance.ShowEntity();
            }
        }

        #endregion

        #region Controls

        private void HandleUserInput()
        {
            if (FlatRedBallServices.Game.IsActive)
            {
                HandleScrollWheel();
                HandleCameraDrag();
            }
        }

        private void HandleScrollWheel()
        {
            if (!ToolsetUIEntityInstance.IsMouseOverUI)
            {
                SpriteManager.Camera.Z -= InputManager.Mouse.ScrollWheel * 50;
                if (SpriteManager.Camera.Z > MaxZoomDistance)
                {
                    SpriteManager.Camera.Z = MaxZoomDistance;
                }
                else if (SpriteManager.Camera.Z < MinZoomDistance)
                {
                    SpriteManager.Camera.Z = MinZoomDistance;
                }
            }
        }

        private void HandleCameraDrag()
        {
            if (!ToolsetUIEntityInstance.IsMouseOverUI)
            {
                if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.RightButton))
                {
                    SpriteManager.Camera.X -= InputManager.Mouse.XChange;
                    SpriteManager.Camera.Y += InputManager.Mouse.YChange;
                }
            }
        }

        #endregion

        #region User Actions

        private void HandleAreaLoadEvent(object sender, ObjectSelectionEventArgs e)
        {
            Area selectedArea = _repositoryFactory.GetGameObjectRepository<Area>().GetByID(e.ResourceID);
            AreaEntityInstance.ChangeArea(selectedArea);
        }

        #endregion

        #region Methods

        private void HideAllEditors()
        {
            AreaEntityInstance.HideEntity();
            TilesetEditorEntityInstance.HideEntity();
        }

        #endregion

        #region Save Methods

        private void SaveArea(object sender, GameObjectSaveEventArgs e)
        {
            }
        private void SaveCreature(object sender, GameObjectSaveEventArgs e)
        {
        }
        private void SaveItem(object sender, GameObjectSaveEventArgs e)
        {
        }
        private void SavePlaceable(object sender, GameObjectSaveEventArgs e)
        {
        }
        private void SaveConversation(object sender, GameObjectSaveEventArgs e)
        {
        }
        private void SaveScript(object sender, GameObjectSaveEventArgs e)
        {
        }
        private void SaveTileset(object sender, GameObjectSaveEventArgs e)
        {
            e.ActiveTileset.TileList = (from tile
                                        in TilesetEditorEntityInstance.TileList
                                        select new Tile
                                        {
                                            TextureCellX = tile.SpriteSheetColumn,
                                            TextureCellY = tile.SpriteSheetRow,
                                            TilesetID = e.ActiveTileset.ResourceID,
                                            CollisionBoxes = (from box
                                                              in tile.CollisionBoxList
                                                              select new TileCollisionBox
                                                              {
                                                                  IsPassable = box.IsPassable,
                                                                  TileLocationIndex = box.TileIndex
                                                              }).ToList()
                                        }).ToList();

            _repositoryFactory.GetGameObjectRepository<Tileset>().Save(e.ActiveTileset);
        }

        #endregion
    }
}
