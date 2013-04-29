using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework.Graphics;

namespace WinterEngine.Editor.TileGraphics
{
    public class SpriteSheet
    {
        #region Properties
        /// <summary>
        /// Size of the sprites within the sprite sheet
        /// </summary>
        public FlatRedBall.Math.Geometry.Point SpriteSize { get; set; }

        /// <summary>
        /// Texture used as the sprite sheet.
        /// </summary>
        public Texture2D Texture { get; set; }

        public int SpriteCountX
        {
            get
            {
                if (Texture != null && SpriteSize.X != 0)
                {
                    return Texture.Width / Convert.ToInt32(SpriteSize.X);
                }
                else
                    return 0;
            }
        }

        public int SpriteCountY
        {
            get
            {
                if (Texture != null && SpriteSize.Y != 0)
                {
                    return Texture.Width / Convert.ToInt32(SpriteSize.Y);
                }
                else
                    return 0;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of the SpriteSheet class.
        /// </summary>
        /// <param name="texture">Resource to load as the sprite sheet.</param>
        public SpriteSheet(string texture)
        {
            Texture = FlatRedBallServices.Load<Texture2D>(texture);
        }

        /// <summary>
        /// Create a new instance of the SpriteSheet class.
        /// </summary>
        /// <param name="texture">Resource to load as the sprite sheet.</param>
        /// <param name="spriteSizePixelWidth">Width of a sprite in the sheet, in pixels.</param>
        /// <param name="spriteSizePixelHeight">Height of a sprite in the sheet, in pixels.</param>
        public SpriteSheet(string texture, int spriteSizePixelWidth, int spriteSizePixelHeight)
        {
            Texture = FlatRedBallServices.Load<Texture2D>(texture);
            SpriteSize = new FlatRedBall.Math.Geometry.Point(spriteSizePixelWidth, spriteSizePixelHeight);
        }

        /// <summary>
        /// Create a new instance of the SpriteSheet class.
        /// </summary>
        /// <param name="texture">Existing resource to reference as the sprite sheet.</param>
        public SpriteSheet(ref Texture2D texture)
        {
            Texture = texture;
        }

        /// <summary>
        /// Create a new instance of the SpriteSheet class.
        /// </summary>
        /// <param name="texture">Existing resource to reference as the sprite sheet.</param>
        /// <param name="spriteSizePixelWidth">Width of a sprite in the sheet, in pixels.</param>
        /// <param name="spriteSizePixelHeight">Height of a sprite in the sheet, in pixels.</param>
        public SpriteSheet(ref Texture2D texture, int spriteSizePixelWidth, int spriteSizePixelHeight)
        {
            Texture = texture;
            SpriteSize = new FlatRedBall.Math.Geometry.Point(spriteSizePixelWidth, spriteSizePixelHeight);
        }
        #endregion

        #region Methods

        #region GetCustomSprite
        /// <summary>
        /// Returns a sprite object from the sprite sheet based on the pixel location and size passed in
        /// </summary>
        /// <param name="location">X,Y of the pixel to start the sprite at</param>
        /// <param name="size">Size of the sprite in pixels</param>
        /// <param name="addToSpriteManager">Create by adding to SpriteManager first</param>
        /// <returns>Sprite</returns>
        public Sprite GetCustomSprite(FlatRedBall.Math.Geometry.Point location, FlatRedBall.Math.Geometry.Point size, bool addToSpriteManager)
        {
            if (Texture == null)
                throw new Exception("Texture property is null.");

            Sprite sprite;
            if (addToSpriteManager)
                sprite = SpriteManager.AddSprite(Texture);
            else
            {
                sprite = new Sprite();
                sprite.Texture = Texture;
            }

            sprite.Vertices[0].TextureCoordinate.X = (float)location.X / Texture.Width;
            sprite.Vertices[0].TextureCoordinate.Y = (float)location.Y / Texture.Height;

            sprite.Vertices[1].TextureCoordinate.X = sprite.Vertices[0].TextureCoordinate.X + (float)size.X / Texture.Width;
            sprite.Vertices[1].TextureCoordinate.Y = sprite.Vertices[0].TextureCoordinate.Y;

            sprite.Vertices[2].TextureCoordinate.X = sprite.Vertices[1].TextureCoordinate.X;
            sprite.Vertices[2].TextureCoordinate.Y = sprite.Vertices[0].TextureCoordinate.Y + (float)size.Y / Texture.Height;

            sprite.Vertices[3].TextureCoordinate.X = sprite.Vertices[0].TextureCoordinate.X;
            sprite.Vertices[3].TextureCoordinate.Y = sprite.Vertices[0].TextureCoordinate.Y + (float)size.Y / Texture.Height;

            return sprite;
        }

        public Sprite GetCustomSprite(FlatRedBall.Math.Geometry.Point location, FlatRedBall.Math.Geometry.Point size)
        {
            return GetCustomSprite(location, size, false);
        }

        public Sprite GetCustomSprite(int x, int y, int width, int height)
        {
            return GetCustomSprite(new FlatRedBall.Math.Geometry.Point(x, y), new FlatRedBall.Math.Geometry.Point(width, height), false);
        }

        public Sprite GetCustomSprite(int x, int y, int width, int height, bool addToSpriteManager)
        {
            return GetCustomSprite(new FlatRedBall.Math.Geometry.Point(x, y), new FlatRedBall.Math.Geometry.Point(width, height), addToSpriteManager);
        }
        #endregion

        /// <summary>
        /// Recaulates an existing sprite's texture coordinates to match the X,Y in the sprite sheet. Does not reset the sprite sheet on the sprite if they do not match.
        /// </summary>
        /// <param name="sprite">Sprite to modify</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void UpdateSprite(Sprite sprite, int x, int y)
        {
            UpdateSprite(sprite, x, y, false);
        }

        /// <summary>
        /// Recaulates an existing sprite's texture coordinates to match the X,Y in the sprite sheet.
        /// </summary>
        /// <param name="sprite">Sprite to modify</param>
        /// <param name="resetSpriteSheet">If the passed in sprite isn't using the same sprite sheet as this one, true will set it to use it, while false will just return.</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void UpdateSprite(Sprite sprite, int x, int y, bool resetSpriteSheet)
        {
            if (sprite == null)
                return;
            else if (sprite.Texture != Texture)
            {
                if (resetSpriteSheet)
                    sprite.Texture = this.Texture;
                else
                    return;
            }

            sprite.Vertices[0].TextureCoordinate.X = (float)x * (int)SpriteSize.X / Texture.Width;
            sprite.Vertices[0].TextureCoordinate.Y = (float)y * (int)SpriteSize.Y / Texture.Height;

            sprite.Vertices[1].TextureCoordinate.X = sprite.Vertices[0].TextureCoordinate.X + (float)SpriteSize.X / Texture.Width;
            sprite.Vertices[1].TextureCoordinate.Y = sprite.Vertices[0].TextureCoordinate.Y;

            sprite.Vertices[2].TextureCoordinate.X = sprite.Vertices[1].TextureCoordinate.X;
            sprite.Vertices[2].TextureCoordinate.Y = sprite.Vertices[0].TextureCoordinate.Y + (float)SpriteSize.Y / Texture.Height;

            sprite.Vertices[3].TextureCoordinate.X = sprite.Vertices[0].TextureCoordinate.X;
            sprite.Vertices[3].TextureCoordinate.Y = sprite.Vertices[0].TextureCoordinate.Y + (float)SpriteSize.Y / Texture.Height;
        }

        /// <summary>
        /// Zero-based coordinate location of a sprite within the sprite sheet
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Sprite</returns>
        public Sprite GetSprite(int x, int y)
        {
            if (SpriteSize != (new FlatRedBall.Math.Geometry.Point()))
            {
                return GetCustomSprite(x * (int)SpriteSize.X, y * (int)SpriteSize.Y, (int)SpriteSize.X, (int)SpriteSize.Y);
            }
            return null;
        }

        public Sprite GetSprite(int x, int y, bool addToSpriteManager)
        {
            if (SpriteSize != (new FlatRedBall.Math.Geometry.Point()))
            {
                return GetCustomSprite(x * (int)SpriteSize.X, y * (int)SpriteSize.Y, (int)SpriteSize.X, (int)SpriteSize.Y, addToSpriteManager);
            }
            return null;
        }

        public FlatRedBall.Math.Geometry.FloatRectangle GetTextureCoordinates(int x, int y)
        {
            //TODO: GetTextureCoordinates will have to change to not create temp sprite.
            Sprite temp = new Sprite();
            temp.Vertices[0].TextureCoordinate.X = (float)x * (int)SpriteSize.X / Texture.Width;
            temp.Vertices[0].TextureCoordinate.Y = (float)y * (int)SpriteSize.Y / Texture.Height;

            temp.Vertices[1].TextureCoordinate.X = temp.Vertices[0].TextureCoordinate.X + (float)SpriteSize.X / Texture.Width;
            temp.Vertices[1].TextureCoordinate.Y = temp.Vertices[0].TextureCoordinate.Y;

            temp.Vertices[2].TextureCoordinate.X = temp.Vertices[1].TextureCoordinate.X;
            temp.Vertices[2].TextureCoordinate.Y = temp.Vertices[0].TextureCoordinate.Y + (float)SpriteSize.Y / Texture.Height;

            temp.Vertices[3].TextureCoordinate.X = temp.Vertices[0].TextureCoordinate.X;
            temp.Vertices[3].TextureCoordinate.Y = temp.Vertices[0].TextureCoordinate.Y + (float)SpriteSize.Y / Texture.Height;

            return new FlatRedBall.Math.Geometry.FloatRectangle(temp.TopTextureCoordinate, temp.BottomTextureCoordinate, temp.LeftTextureCoordinate, temp.RightTextureCoordinate);
        }

        public Microsoft.Xna.Framework.Rectangle GetSpriteRectangle(int x, int y)
        {
            return new Microsoft.Xna.Framework.Rectangle(x * Convert.ToInt32(SpriteSize.X), y * Convert.ToInt32(SpriteSize.Y), Convert.ToInt32(SpriteSize.X), Convert.ToInt32(SpriteSize.Y));
        }

        #endregion
    }
}
