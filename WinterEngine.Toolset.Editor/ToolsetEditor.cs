using System;
using System.Collections.Generic;

using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Utilities;

using Microsoft.Xna.Framework;
#if !FRB_MDX
using System.Linq;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endif
using FlatRedBall.Screens;

namespace WinterEngine.Toolset.Editor
{
    public class ToolsetEditor : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public ContentManager content;

        public ToolsetEditor(Action onInitializeComplete)
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            // give the base class a reference to our graphics manager
            IGraphicsDeviceManager graphicsDeviceManager = (IGraphicsDeviceManager)graphics;
            if (graphicsDeviceManager != null)
            {
                graphicsDeviceManager.CreateDevice();
                this.GetType().BaseType.GetField("graphicsDeviceManager",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic).SetValue(this, graphicsDeviceManager);
            }

            // standard FRB setup
            Content.RootDirectory = "Content";
            BackStack<string> bs = new BackStack<string>();
            bs.Current = string.Empty;

            // pass the callback to our Initilization
            Initialize(onInitializeComplete);
        }

        protected void Initialize(Action onInitializeComplete)
        {
            // manually call the base Initialize and begin the game running
            base.Initialize();
            this.BeginRun();

            Renderer.UseRenderTargets = false;
            FlatRedBallServices.InitializeFlatRedBall(this, graphics);
            GlobalContent.Initialize();


            // get the GameTime property from the base Game class
            GameTime gt = this.GetType().BaseType.GetField("gameTime",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic).GetValue(this) as GameTime;

            // manually call the first Update
            this.Update(gt);

            // notify base class that the first update has been completed
            this.GetType().BaseType.GetField("doneFirstUpdate",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic).SetValue(this, true);

            // navigate to our starting screen
			FlatRedBall.Screens.ScreenManager.Start(typeof(WinterEngine.Toolset.Editor.Screens.Editor));

            // we should be ready to run, fire the callback
            onInitializeComplete();
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
        protected override void Dispose(bool disposing)
        {
            this.EndRun();
            base.Dispose(disposing);
        }
    }
}
