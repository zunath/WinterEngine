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
using WinterEngine.Game.Factories;

namespace WinterEngine.Game.Entities
{
    public partial class TilesetEditorEntity
    {
        #region Fields

        private Texture2D _tilesetSpriteSheet;
        #endregion

        #region Properties

        private Texture2D EntitySpriteSheet
        {
            get { return _tilesetSpriteSheet; }
            set { _tilesetSpriteSheet = value; }
        }
        
        #endregion

        #region Events / Delegates

        public event EventHandler<PositionEventArgs> OnTileSelected;

        #endregion

        #region FRB Events


        private void CustomInitialize()
        {
            TileEntityFactory.Initialize(TileList, ContentManagerName);
        }

        private void CustomActivity()
        {

        }

        private void CustomDestroy()
        {
            ClearTileEntityList();
            TileEntityFactory.Destroy();

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
                ClearTileEntityList();
                ContentPackageResource resource;

                using (ContentPackageResourceRepository repo = new ContentPackageResourceRepository())
                {
                    resource = repo.GetByID(e.ResourceID);
                }

                if (resource != null && !resource.IsDefault)
                {
                    EntitySpriteSheet = resource.ToTexture2D();
                    GenerateTileSpriteList();
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

        private void ClearTileEntityList()
        {
            for (int index = TileList.Count - 1; index >= 0; index--)
            {
                TileList[index].Destroy();
            }
        }

        private void GenerateTileSpriteList()
        {
            int numberOfColumns = EntitySpriteSheet.Width / (int)MappingEnum.TileWidth;
            int numberOfRows = EntitySpriteSheet.Height / (int)MappingEnum.TileHeight;
            int numberOfTiles = numberOfColumns * numberOfRows;

            for (int currentColumn = 0; currentColumn < numberOfColumns; currentColumn++)
            {
                for (int currentRow = 0; currentRow < numberOfRows; currentRow++)
                {
                    TileEntity entity = TileEntityFactory.CreateNew();
                    entity.InitializeSprite(EntitySpriteSheet, currentRow, currentColumn);
                }
            }

        }
        

        #endregion
    }
}
