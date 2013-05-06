
using FlatRedBall;
using FlatRedBall.Graphics;

using Microsoft.Xna.Framework;
#if !FRB_MDX

using Microsoft.Xna.Framework.Graphics;

#endif
using WinterEngine.UI.AwesomiumUILib;
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

        AwesomiumUI ui;
        SpriteBatch batch;

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
            batch = new SpriteBatch(FlatRedBallServices.GraphicsDevice);
            ui = new AwesomiumUI();
            ui.Initialize(FlatRedBallServices.GraphicsDevice, 200, 200, FileManager.RelativeDirectory);
            ui.Load("ServerList.html");
        }


        protected override void Update(GameTime gameTime)
        {
            FlatRedBallServices.Update(gameTime);

            FlatRedBall.Screens.ScreenManager.Activity();
            base.Update(gameTime);

            ui.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            FlatRedBallServices.Draw();
            base.Draw(gameTime);

            batch.Begin();

            batch.Draw(ui.webTexture, new Rectangle(0, 0, 200, 200), Color.Blue);

            batch.End();
            
        }
    }
}
