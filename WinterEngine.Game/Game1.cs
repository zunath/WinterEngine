
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
    }
}
