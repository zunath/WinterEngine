
using FlatRedBall;
using FlatRedBall.Graphics;

using Microsoft.Xna.Framework;
#if !FRB_MDX

#endif
using WinterEngine.Game.Services;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace WinterEngine.Game
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        private Form form;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
			
			#if WINDOWS_PHONE
			// Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            graphics.IsFullScreen = true;
			
			#endif
        }

        protected override void Initialize()
        {
            Renderer.UseRenderTargets = false;
            FlatRedBallServices.InitializeFlatRedBall(this, graphics);
			CameraSetup.SetupCamera(SpriteManager.Camera, graphics);
			GlobalContent.Initialize();

			FlatRedBall.Screens.ScreenManager.Start(typeof(WinterEngine.Game.Screens.ToolsetScreen));

            base.Initialize();
            Window.AllowUserResizing = true;
            
            FlatRedBallServices.Game.IsMouseVisible = true;
            FlatRedBallServices.GraphicsOptions.BackgroundColor = Microsoft.Xna.Framework.Color.LightGray;
            
            // Have to convert the game window to a form for some actions.
            form = Form.FromHandle(FlatRedBallServices.WindowHandle) as Form;
            form.MinimumSize = new Size(200, 200);
            form.Resize += HandleResize;
            //form.ResizeEnd += HandleResize;
            FlatRedBallServices.CornerGrabbingResize += ReactToResizing;
        }

        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.Update(gameTime);
            WinterEngineService.Update();
            FlatRedBall.Screens.ScreenManager.Activity();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            FlatRedBallServices.Draw();
            WinterEngineService.Draw();
            base.Draw(gameTime);
        }

        private void HandleResize(object sender, EventArgs e)
        {
            if (form.WindowState == FormWindowState.Minimized)
            {
                FlatRedBallServices.SuspendEngine();
            }
            else
            {
                FlatRedBallServices.UnsuspendEngine();
            }
        }


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


    }
}
