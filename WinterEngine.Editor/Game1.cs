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
using System.Windows.Forms;
using System.Drawing;
using WinterEngine.Editor.Controls;

namespace WinterEngine.Editor
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        #region Custom fields
        ObjectBar _objectBar;
        MenuBarControl _menuBar;
        #endregion

        #region Custom properties

        /// <summary>
        /// Gets or sets the WinForms control for object selection.
        /// </summary>
        public ObjectBar ObjectSelectionBar
        {
            get { return _objectBar; }
            set { _objectBar = value; }
        }

        /// <summary>
        /// Gets or sets the WinForms control for the menu bar.
        /// </summary>
        public MenuBarControl MenuBar
        {
            get { return _menuBar; }
            set { _menuBar = value; }
        }

        #endregion

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
            InitializeFormControls();

			FlatRedBall.Screens.ScreenManager.Start(typeof(WinterEngine.Editor.Screens.EditorScreen));

            base.Initialize();
            Window.AllowUserResizing = true;
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

        /// <summary>
        /// Handles creating the form controls and drawing them in the appropriate locations
        /// </summary>
        private void InitializeFormControls()
        {
            MenuBar = new MenuBarControl();
            MenuBar.Location = new System.Drawing.Point(0, 0);
            MenuBar.BorderStyle = BorderStyle.None;
            MenuBar.Size = new Size(GraphicsDevice.Viewport.Width, 29);

            Control.FromHandle(Window.Handle).Controls.Add(MenuBar);

            ObjectSelectionBar = new ObjectBar();
            ObjectSelectionBar.Location = new System.Drawing.Point(0, MenuBar.Size.Height + 1);
            ObjectSelectionBar.BorderStyle = BorderStyle.None;
            ObjectSelectionBar.Size = new Size(GraphicsDevice.Viewport.Width, 29);
            
            Control.FromHandle(Window.Handle).Controls.Add(ObjectSelectionBar);
        }
    }
}
