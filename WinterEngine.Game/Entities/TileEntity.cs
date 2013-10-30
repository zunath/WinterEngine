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
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class TileEntity
    {
        #region Properties

        public int SpriteSheetRow { get; private set; }
        public int SpriteSheetColumn { get; private set; }

        #endregion

        #region Events / Delegates

        public event EventHandler<PositionEventArgs> OnSelectTile;

        #endregion

        #region FRB Methods

        private void CustomInitialize()
		{
		}

		private void CustomActivity()
		{
            if (WasClickedThisFrame(GuiManager.Cursor))
            {
                if (OnSelectTile != null)
                {
                    OnSelectTile(this, new PositionEventArgs(SpriteSheetRow, SpriteSheetColumn));
                }
            }
		}

		private void CustomDestroy()
		{
            DestroySprite();
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Event Handling

        #endregion

        #region Methods

        public void InitializeSprite(Texture2D texture, int row, int column)
        {
            this.SpriteSheetRow = row;
            this.SpriteSheetColumn = column;
            SpriteInstance = new Sprite();
            this.X = column * (int)MappingEnum.TileWidth;
            this.Y = -(row * (int)MappingEnum.TileHeight);

            SpriteInstance.Texture = texture;
            SpriteInstance.PixelSize = 0.5f;
            SpriteInstance.TopTexturePixel = row * (int)MappingEnum.TileHeight;
            SpriteInstance.LeftTexturePixel = column * (int)MappingEnum.TileWidth;
            SpriteInstance.BottomTexturePixel = (row + 1) * (int)MappingEnum.TileHeight;
            SpriteInstance.RightTexturePixel = (column + 1) * (int)MappingEnum.TileWidth;
            
            SpriteManager.AddSprite(SpriteInstance);
            SpriteInstance.AttachTo(this, false);
        }

        public void DestroySprite()
        {
            SpriteInstance.Detach();
            SpriteManager.RemoveSprite(SpriteInstance);
        }

        #endregion
    }
}
