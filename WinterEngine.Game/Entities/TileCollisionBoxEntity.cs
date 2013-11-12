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
using Microsoft.Xna.Framework;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class TileCollisionBoxEntity
    {
        #region Fields

        private bool _isPassable;

        #endregion

        #region Properties

        public bool IsPassable 
        {
            get
            {
                return _isPassable;
            }
            set
            {
                _isPassable = value;
                UpdatePassability();
            }
        }

        public int TileRow { get; set; }
        public int TileColumn { get; set; }
        public int TileIndex { get; set; }
        private bool IsPainting { get; set; }

        #endregion


        #region FRB Events

        private void CustomInitialize()
		{

		}

		private void CustomActivity()
		{
            if (InputManager.Mouse.ButtonDown(Mouse.MouseButtons.LeftButton))
            {
                if (!IsPainting && this.HasCursorOver(GuiManager.Cursor))
                {
                    IsPassable = !IsPassable;

                    IsPainting = true;
                }
            }
            else
            {
                IsPainting = false;
            }
            
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        #endregion

        #region Methods

        private void UpdatePassability()
        {
            if (_isPassable)
            {
                this.SpriteInstance.Green = 0.5f;
                this.SpriteInstance.Blue = 0.0f;
                this.SpriteInstance.Red = 0.0f;
            }
            else
            {
                this.SpriteInstance.Green = 0.0f;
                this.SpriteInstance.Blue = 0.0f;
                this.SpriteInstance.Red = 0.5f;
            }
        }

        #endregion

    }
}
