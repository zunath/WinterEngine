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
using System.Linq;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using FlatRedBall.Graphics;
using WinterEngine.Game.Interfaces;
using WinterEngine.Game.Factories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Repositories;


#endif

namespace WinterEngine.Game.Entities
{
	public partial class TileEntity : IEditorEntity
    {
        #region Properties

        public int TileID { get; set; }
        public int SpriteSheetRow { get; private set; }
        public int SpriteSheetColumn { get; private set; }

        #endregion

        #region Events / Delegates

        #endregion

        #region FRB Methods

        private void CustomInitialize()
		{
            TileCollisionBoxEntityFactory.Initialize(this.CollisionBoxList, ContentManagerName);
		}

		private void CustomActivity()
		{
            SpriteManager.ManualUpdate(SpriteInstance);

		}

        private void CustomDestroy()
		{
            CollisionBoxList.Clear();
            TileCollisionBoxEntityFactory.Destroy();
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

        private void DestroySprite()
        {
            SpriteInstance.Detach();
            SpriteManager.RemoveSprite(SpriteInstance);
        }

        public void InitializeCollisionBoxes(int tileID, List<TileCollisionBox> dbCollisionBoxes)
        {
            this.TileID = tileID;
            int collisionBoxWidth = (int)MappingEnum.TileWidth / (int)MappingEnum.CollisionBoxDivisor;
            int collisionBoxHeight = (int)MappingEnum.TileHeight / (int)MappingEnum.CollisionBoxDivisor;
            int numberOfCollisionBoxesRows = (int)MappingEnum.CollisionBoxDivisor;
            int numberOfCollisionBoxesColumns = (int)MappingEnum.CollisionBoxDivisor;

            // In FRB, the origin is at the center of the sprite. We need to compensate for this by shifting
            // the collision boxes to the left (negative X) and up (positive Y)
            int offsetX = ((int)MappingEnum.TileWidth / 2) - (collisionBoxWidth / 2);
            int offsetY = ((int)MappingEnum.TileHeight / 2) - (collisionBoxHeight / 2);

            int boxIndex = 1;
            for (int row = 0; row < numberOfCollisionBoxesRows; row++)
            {
                for (int column = 0; column < numberOfCollisionBoxesColumns; column++)
                {
                    TileCollisionBox dbBox = dbCollisionBoxes.SingleOrDefault(x => x.TileLocationIndex == boxIndex && x.TileID == TileID);
                    TileCollisionBoxEntity box = TileCollisionBoxEntityFactory.CreateNew();
                    box.TileRow = row;
                    box.TileColumn = column;
                    box.TileIndex = boxIndex;
                    box.IsPassable = dbBox == null ? false : dbBox.IsPassable;

                    box.X = (this.Position.X - offsetX) + (column * collisionBoxWidth);
                    box.Y = (this.Position.Y + offsetY) - (row * collisionBoxHeight);

                    boxIndex++;
                }
            }
        }

        #endregion

        #region Interface Methods

        public void HideEntity()
        {
            this.SpriteInstance.Visible = false;
        }

        public void ShowEntity()
        {
            this.SpriteInstance.Visible = true;
        }

        #endregion
    }
}
