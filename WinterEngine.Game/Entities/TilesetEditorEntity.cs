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

using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.Library.Extensions;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.Enumerations;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.TileGraphics;

namespace WinterEngine.Game.Entities
{
    public partial class TilesetEditorEntity
    {
        #region Fields

        private Texture2D _tilesetSpriteSheet;
        private List<Sprite> _tileSprites;

        #endregion

        #region Properties

        private Texture2D EntitySpriteSheet
        {
            get { return _tilesetSpriteSheet; }
            set { _tilesetSpriteSheet = value; }
        }
        private List<Sprite> TileSprites
        {
            get
            {
                if (_tileSprites == null)
                {
                    _tileSprites = new List<Sprite>();
                }
                return _tileSprites;
            }
            set
            {
                _tileSprites = value;
            }
        }
        
        #endregion

        #region Events / Delegates

        public event EventHandler<ObjectSelectionEventArgs> OnTileSelected;

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

        #region Event Handling

        public void LoadTilesetSpritesheet(object sender, ObjectSelectionEventArgs e)
        {
            try
            {
                if (e.ResourceID > 0)
                {
                    ContentPackageResource resource;

                    using (ContentPackageResourceRepository repo = new ContentPackageResourceRepository())
                    {
                        resource = repo.GetByID(e.ResourceID);
                    }

                    EntitySpriteSheet = resource.ToTexture2D();
                    GenerateTileSpriteList();
                }
                else
                {
                    EntitySpriteSheet = FlatRedBallServices.Load<Texture2D>(@"Content/Editor/Icons/NoGraphic_64x64.png");
                }
            }
            catch
            {
                throw;
            }
        }



        public void LoadTilesetEditor(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods

        private void GenerateTileSpriteList()
        {
            // DEBUGGING
            SpriteManager.Camera.Z = 300;
            // END DEBUGGING

            SpriteManager.RemoveSpriteList(TileSprites);
            TileSprites.Clear();

            int numberOfColumns = EntitySpriteSheet.Width / (int)MappingEnum.TileWidth;
            int numberOfRows = EntitySpriteSheet.Height / (int)MappingEnum.TileHeight;
            int numberOfTiles = numberOfColumns * numberOfRows;

            for (int currentColumn = 0; currentColumn < numberOfColumns; currentColumn++)
            {
                for (int currentRow = 0; currentRow < numberOfRows; currentRow++)
                {

                    Sprite sprite = new Sprite
                    {
                        Texture = EntitySpriteSheet,
                        PixelSize = 0.5f,                    
                        TopTexturePixel = currentRow * (int)MappingEnum.TileHeight,
                        LeftTexturePixel = currentColumn * (int)MappingEnum.TileWidth,
                        BottomTexturePixel = (currentRow + 1) * (int)MappingEnum.TileHeight,
                        RightTexturePixel = (currentColumn + 1) * (int)MappingEnum.TileWidth,
                        X = currentColumn * (int)MappingEnum.TileWidth,
                        Y = -(currentRow * (int)MappingEnum.TileHeight)
                        //X = currentColumn,
                        //Y = currentRow
                    };
                    

                    TileSprites.Add(sprite);
                    SpriteManager.AddSprite(sprite);
                    
                }
            }
        }
        

        #endregion
    }
}
