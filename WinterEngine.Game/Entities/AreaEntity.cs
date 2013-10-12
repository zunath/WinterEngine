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
using WinterEngine.Library.Utility;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using WinterEngine.Library.Extensions;


namespace WinterEngine.Game.Entities
{
	public partial class AreaEntity
    {
        #region Fields

        private Texture2D _mapSpriteSheet;

        #endregion

        #region Properties

        /// <summary>
        /// Currently displayed area.
        /// </summary>
        private Area ActiveArea { get; set; }

        /// <summary>
        /// Spritesheet texture for the active area.
        /// </summary>
        private Texture2D AreaSpriteSheet 
        {
            get
            {
                if (_mapSpriteSheet == null)
                {
                    _mapSpriteSheet = ActiveArea.GraphicResource.ToTexture2D();
                }

                return _mapSpriteSheet;
            }
            set
            {
                _mapSpriteSheet = value;
            }
        }

        #endregion

        #region FRB Events
        private void CustomInitialize()
		{
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
    }
}
