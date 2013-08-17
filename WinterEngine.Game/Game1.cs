
using FlatRedBall;
using FlatRedBall.Graphics;

using Microsoft.Xna.Framework;
#if !FRB_MDX

using Microsoft.Xna.Framework.Graphics;

#endif
using WinterEngine.UI.AwesomiumXNA;
using FlatRedBall.IO;
using WinterEngine.Game.Services;
using System.Windows.Forms;
using System.Drawing;

namespace WinterEngine.Game
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        private bool _wasMinimizedLastFrame;
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
			GlobalContent.Initialize();

			FlatRedBall.Screens.ScreenManager.Start(typeof(WinterEngine.Game.Screens.ToolsetScreen));

            base.Initialize();
            Window.AllowUserResizing = true;
            
            FlatRedBallServices.Game.IsMouseVisible = true;
            FlatRedBallServices.GraphicsOptions.BackgroundColor = Microsoft.Xna.Framework.Color.Black;
            
            // Have to convert the game window to a form for some actions.
            form = Form.FromHandle(FlatRedBallServices.WindowHandle) as Form;
            form.MinimumSize = new Size(200, 200);
        }


        protected override void Update(GameTime gameTime)
        {
            
            if (form.WindowState == FormWindowState.Minimized)
            {
                _wasMinimizedLastFrame = true;
                FlatRedBallServices.SuspendEngine();
            }
            else if (form.WindowState != FormWindowState.Minimized && _wasMinimizedLastFrame)
            {
                _wasMinimizedLastFrame = false;
                FlatRedBallServices.UnsuspendEngine();
            }

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
    }
}
