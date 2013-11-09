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
using WinterEngine.Editor.Extensions;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.Enumerations;
using FlatRedBall.ManagedSpriteGroups;
using WinterEngine.Game.Factories;
using WinterEngine.DataTransferObjects.BusinessObjects;
using Microsoft.Xna.Framework;
using System.Linq;
using WinterEngine.Game.Interfaces;

namespace WinterEngine.Game.Entities
{
    public partial class TilesetEditorEntity: IEditorEntity
    {
        #region Fields

        private Texture2D _tilesetSpriteSheet;
        private IGenericRepository<ContentPackageResource> contentPackageResourceRepo;
        #endregion

        #region Properties

        private int TilesetResourceID { get; set; }
        private TileEntity SelectedTile { get; set; }

        private Texture2D EntitySpriteSheet
        {
            get { return _tilesetSpriteSheet; }
            set { _tilesetSpriteSheet = value; }
        }
        
        #endregion

        #region Events / Delegates

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

        public void HandleLoadTilesetSpritesheetEvent(object sender, TilesetSelectionEventArgs e)
        {
            try
            {
                ClearTileEntityList();
                this.TilesetResourceID = e.TilesetResourceID;
                ContentPackageResource resource;

                using (ContentPackageResourceRepository repo = new ContentPackageResourceRepository())
                {
                    resource = repo.GetByID(e.GraphicResourceID);
                }

                if (resource != null && !resource.IsDefault)
                {
                    ContentPackageResource resource = contentPackageResourceRepo.GetByID(e.ResourceID);
                    EntitySpriteSheet = resource.ToTexture2D();
                    GenerateTileSpriteList();
                }
                
                }
            catch
            {
                throw;
            }
        }

        public void HandleSaveTilesetSpritesheetEvent(object sender, TilesetSelectionEventArgs e)
        {
            try
            {
                List<Tile> tileDTOs = (from tile
                                       in TileList
                                       select new Tile
                                       {
                                           IsPassable = tile.IsPassable,
                                           TextureCellX = tile.SpriteSheetColumn,
                                           TextureCellY = tile.SpriteSheetRow,
                                           TilesetID = e.TilesetResourceID
                                       }).ToList();



            }
            catch
        {
                throw;
            }
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
            int tileIndex = 0;

            Tileset activeTileset;
            using (TilesetRepository repo = new TilesetRepository())
            {
                activeTileset = repo.GetByID(TilesetResourceID);
            }

            for (int currentColumn = 0; currentColumn < numberOfColumns; currentColumn++)
            {
                for (int currentRow = 0; currentRow < numberOfRows; currentRow++)
                {
                    Tile activeTile = activeTileset.TileList.FirstOrDefault(x => x.TextureCellX == currentColumn && x.TextureCellY == currentRow);
                    bool isPassable = activeTile == null ? true : activeTile.IsPassable;
                    TileEntity entity = TileEntityFactory.CreateNew();
                    entity.InitializeSprite(EntitySpriteSheet, currentRow, currentColumn, isPassable);
                    
                    tileIndex++;
                }
            }
        }

        #endregion

        #region Interface Methods

        public void HideEntity()
                    {
            foreach (TileEntity tile in TileList)
            {
                tile.HideEntity();
                }
            }

        public void ShowEntity()
        {
            foreach (TileEntity tile in TileList)
            {
                tile.ShowEntity();
            }
        }
        
        #endregion
    }
}
