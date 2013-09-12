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

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.Game.Entities;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess;
#endif

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
            FlatRedBallServices.CornerGrabbingResize -= ReactToResizing;

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Event Handling

        private void BindEvents()
        {
            ToolsetUIEntityInstance.OnChangeScreen += base.ChangeScreen;
            FlatRedBallServices.CornerGrabbingResize += ReactToResizing;
            ToolsetUIEntityInstance.OnAreaLoaded += OpenArea;
        }

        #endregion

        #region Window resizing

        private void ReactToResizing(object sender, EventArgs e)
        {
            // Get the new client bounds (the area where things will be drawn)
            Microsoft.Xna.Framework.Rectangle displayRectangle =
                FlatRedBallServices.Game.Window.ClientBounds;

            // This tests if the user has minimized the window
            if (displayRectangle.Width == 0 || displayRectangle.Height == 0)
            {
                // The user has minimized the window.  Don't do anything in this case
                return;
            }

            // Do we need to update things?
            bool hasWindowChanged = (SpriteManager.Cameras[0].DestinationRectangle.Height != displayRectangle.Height) || 
                (SpriteManager.Cameras[0].DestinationRectangle.Width != displayRectangle.Width);

            if (hasWindowChanged)
            {
                // Resize the destination rectangle so the camera renders to the full screen
                // You may need to change this code if using a split screen view.
                SpriteManager.Cameras[0].DestinationRectangle = new Microsoft.Xna.Framework.Rectangle(
                    0, 0, displayRectangle.Width, displayRectangle.Height);

                #region Fix the Orthogonal values

                double unitPerPixel = SpriteManager.Camera.OrthogonalHeight /
                    SpriteManager.Cameras[0].DestinationRectangle.Height;

                SpriteManager.Camera.OrthogonalHeight = (float)(displayRectangle.Height * unitPerPixel);
                SpriteManager.Camera.OrthogonalWidth = (float)(displayRectangle.Width * unitPerPixel);

                #endregion

                #region Fix the 3D (FieldOfView and AspectRatio) values

                // These values represent the field of view at 600 pixels.
                // Increase the values (decrease the number that PI is divided by) to
                // make the view wider (and make things appear smaller)
                double yAt600 = Math.Sin(Math.PI / 8.0);
                double xAt600 = Math.Cos(Math.PI / 8.0);
                double desiredYAt600 = yAt600 * (double)displayRectangle.Height / 600.0;
                float desiredAngle = (float)Math.Atan2(desiredYAt600, xAt600);
                SpriteManager.Cameras[0].FieldOfView = 2 * desiredAngle;

                SpriteManager.Cameras[0].FixAspectRatioYConstant();

                #endregion
            }
        }

        #endregion

        #region Controls

        private void HandleUserInput()
        {
            HandleScrollWheel();
            HandleCameraDrag();
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
