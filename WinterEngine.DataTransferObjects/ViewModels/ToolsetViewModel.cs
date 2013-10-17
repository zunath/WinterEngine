using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.ViewModels
{
    public class ToolsetViewModel
    {
        public string CurrentObjectMode { get; set; }
        public string CurrentObjectTreeSelector { get; set; }
        public string CurrentObjectTabSelector { get; set; }

        [JsonIgnore]
        public GameObjectTypeEnum GameObjectType
        {
            get
            {
                return (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), CurrentObjectMode);
            }
        }


        public Area ActiveArea { get; set; }
        public Creature ActiveCreature { get; set; }
        public Item ActiveItem { get; set; }
        public Placeable ActivePlaceable { get; set; }
        public Conversation ActiveConversation { get; set; }
        public Script ActiveScript { get; set; }
        public Tileset ActiveTileset { get; set; }
        public Tile ActiveTile { get; set; }

        public List<GameModule> ModuleList { get; set; }
        public List<ContentPackage> AvailableContentPackages { get; set; }
        public List<ContentPackage> AttachedContentPackages { get; set; }
        public List<ContentPackageResource> TilesetSpriteSheetsList { get; set; }
        
        public ToolsetViewModel()
        {
            CurrentObjectMode = "";
            CurrentObjectTabSelector = "";
            CurrentObjectTreeSelector = "";
            ActiveArea = new Area(true);
            ActiveConversation = new Conversation(true);
            ActiveCreature = new Creature(true);
            ActiveItem = new Item(true);
            ActivePlaceable = new Placeable(true);
            ActiveScript = new Script();
            ActiveTileset = new Tileset();
            ActiveTile = new Tile();
            ModuleList = new List<GameModule>();
            AvailableContentPackages = new List<ContentPackage>();
            AttachedContentPackages = new List<ContentPackage>();
            TilesetSpriteSheetsList = new List<ContentPackageResource>();
        }
    }
}
