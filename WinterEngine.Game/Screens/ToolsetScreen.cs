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

namespace WinterEngine.Game.Screens
{
	public partial class ToolsetScreen
    {
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
            
            // Area Editor
            ToolsetUIEntityInstance.OnAreaLoaded += OpenArea;
        
            // Tileset Editor
            ToolsetUIEntityInstance.OnTilesetSpritesheetLoaded += TilesetEditorEntityInstance.LoadTilesetSpritesheet;
            TilesetEditorEntityInstance.OnTileSelected += ToolsetUIEntityInstance.LoadTile;
            ToolsetUIEntityInstance.OnTilesetEditorOpened += TilesetEditorEntityInstance.LoadTilesetEditor;
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

        private void OpenArea(object sender, ObjectSelectionEventArgs e)
        {
            Area selectedArea;
            using (AreaRepository repo = new AreaRepository())
            {
                selectedArea = repo.GetByID(e.ResourceID);
            }

            AreaEntityInstance.ChangeArea(selectedArea);
        }

        #endregion
    }
}
