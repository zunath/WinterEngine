using System;
using System.Collections.Generic;
using Winforms = System.Windows.Forms;
using FlatRedBall;
using FlatRedBall.Gui;
using FlatRedBall.Input;
using FlatRedBall.TileGraphics;
using Microsoft.Xna.Framework;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Editor.Utility;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using WinterEngine.Library.Extensions;
using WinterEngine.Game.Interfaces;
using WinterEngine.Editor.Extensions;


namespace WinterEngine.Game.Entities
{
	public partial class AreaEditorEntity: IEditorEntity
    {
        #region Fields

        #endregion

        #region Properties


        #endregion

        #region FRB Events
        private void CustomInitialize()
		{

            //for (int x = 0; x <= 10; x++)
            //{
            //    for (int y = 0; y <= 10; y++)
            //    {
            //        Sprite sprite = new Sprite();
            //        sprite.PixelSize = 0.5f;
            //        sprite.Texture = FlatRedBallServices.Load<Texture2D>(@"Content/Editor/Icons/EmptyCell.png");
            //        sprite.X = x * 32;
            //        sprite.Y = y * 32;

            //        SpriteManager.AddSprite(sprite);
            //    }
            //}
        }

        private void CustomActivity()
		{
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Methods

        public void ChangeArea(Area newArea)
        {
        }

        #endregion

        #region Interface Methods

        public void HideEntity()
        {
            
        }

        public void ShowEntity()
        {
        }

        #endregion
    }
}
