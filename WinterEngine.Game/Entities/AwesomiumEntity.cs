using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.UI.AwesomiumXNA;
using FlatRedBall.IO;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class AwesomiumEntity
    {
        #region Fields
        private AwesomiumUI awesomiumUI;
        private string _awesomiumURI;

        private int _renderWidth;
        private int _renderHeight;

        #endregion

        #region Properties

        public string URI
        {
            get { return _awesomiumURI; }
            set { _awesomiumURI = value; }
        }

        public int RenderWidth
        {
            get 
            {
                if (_renderWidth < 1)
                {
                    _renderWidth = 1;
                }

                return _renderWidth; 
            }
            set { _renderWidth = value; }
        }

        public int RenderHeight
        {
            get 
            {
                if (_renderHeight < 1)
                {
                    _renderHeight = 1;
                }

                return _renderHeight; 
            }
            set { _renderHeight = value; }
        }

        #endregion

        #region FRB Event Handling

        private void CustomInitialize()
		{
            SpriteManager.Camera.Z = 750;
            RenderHeight = FlatRedBallServices.ClientHeight;
            RenderWidth = FlatRedBallServices.ClientWidth;

            InitializeInputSystem();

            awesomiumUI = new AwesomiumUI();
            awesomiumUI.Initialize(FlatRedBallServices.GraphicsDevice, RenderWidth, RenderHeight, FileManager.RelativeDirectory);
            LoadUI();
            RenderedSprite.Texture = awesomiumUI.webTexture;
		}

		private void CustomActivity()
		{
            if (!Object.ReferenceEquals(awesomiumUI, null))
            {
                awesomiumUI.Update();
            }
		}

		private void CustomDestroy()
		{

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {

        }

        #endregion

        #region UI Event Handling

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            awesomiumUI.InjectMouseMove(InputManager.Mouse.X, InputManager.Mouse.Y);
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            awesomiumUI.InjectMouseDown(e.Button);
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            awesomiumUI.InjectMouseUp(e.Button);
        }

        private void FullKeyHandler(object sender, uint msg, IntPtr wParam, IntPtr lParam)
        {
            awesomiumUI.InjectKeyboardEvent((int)msg, (int)wParam, (int)lParam);
        }


        #endregion

        #region Methods

        public void LoadUI()
        {
            _awesomiumURI = "http://facebook.com";

            if (!String.IsNullOrWhiteSpace(_awesomiumURI))
            {
                awesomiumUI.Load(_awesomiumURI);
            }
        }

        #endregion

        #region Initialization Routines

        private void InitializeInputSystem()
        {
            if (!InputSystem.IsInitialized)
            {
                InputSystem.Initialize(FlatRedBallServices.Game.Window);
            }

            InputSystem.MouseMove += MouseMoveHandler;
            InputSystem.MouseDown += MouseDownHandler;
            InputSystem.MouseUp += MouseUpHandler;
            InputSystem.FullKeyHandler += FullKeyHandler;
        }

        #endregion
    }
}
