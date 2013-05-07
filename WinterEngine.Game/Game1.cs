
using FlatRedBall;
using FlatRedBall.Graphics;

using Microsoft.Xna.Framework;
#if !FRB_MDX

using Microsoft.Xna.Framework.Graphics;

#endif
using WinterEngine.UI.AwesomiumXNA;
using FlatRedBall.IO;

namespace WinterEngine.Game
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

			FlatRedBall.Screens.ScreenManager.Start(typeof(WinterEngine.Game.Screens.ServerListScreen));

            base.Initialize();
            Window.AllowUserResizing = true;
            FlatRedBallServices.Game.IsMouseVisible = true;
            FlatRedBallServices.GraphicsOptions.BackgroundColor = Microsoft.Xna.Framework.Color.LightGray;
        }


        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.Update(gameTime);
            FlatRedBall.Screens.ScreenManager.Activity();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            FlatRedBallServices.Draw();
            base.Draw(gameTime);
        }
    }
}
