using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;

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
                try
                {
                    if (String.IsNullOrWhiteSpace(CurrentObjectMode))
                    {
                        return GameObjectTypeEnum.Unknown;
                    }
                    else
                    {
                        return (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), CurrentObjectMode);
                    }
                }
                catch
                {
                    return GameObjectTypeEnum.Unknown;
                }
            }
        }

        public bool IsObjectLoadedForCurrentObjectMode
        {
            get
            {
                bool isLoaded = false;

                if ((GameObjectType == GameObjectTypeEnum.Area && ActiveArea.ResourceID > 0) ||
                   (GameObjectType == GameObjectTypeEnum.Conversation && ActiveConversation.ResourceID > 0) ||
                   (GameObjectType == GameObjectTypeEnum.Creature && ActiveCreature.ResourceID > 0) ||
                   (GameObjectType == GameObjectTypeEnum.Item && ActiveItem.ResourceID > 0) ||
                   (GameObjectType == GameObjectTypeEnum.Placeable && ActivePlaceable.ResourceID > 0) ||
                   (GameObjectType == GameObjectTypeEnum.Script && ActiveScript.ResourceID > 0) ||
                   (GameObjectType == GameObjectTypeEnum.Tileset && ActiveTileset.ResourceID > 0))
                {
                    isLoaded = true;
                }

                return isLoaded;
            }
        }
        public bool IsModuleOpened { get; set; }

        #region Selectable Object Data

        public GameModule ActiveModule { get; set; }
        public Area ActiveArea { get; set; }
        public Creature ActiveCreature { get; set; }
        public Item ActiveItem { get; set; }
        public Placeable ActivePlaceable { get; set; }
        public Conversation ActiveConversation { get; set; }
        public Script ActiveScript { get; set; }
        public Tileset ActiveTileset { get; set; }
        #endregion

        #region Drop Down Menu List Data

        public List<GameModule> ModuleList { get; set; }
        public List<ContentPackage> AvailableContentPackages { get; set; }
        public List<ContentPackage> AttachedContentPackages { get; set; }
        public List<DropDownListUIObject> TilesetSpriteSheetsList { get; set; }
        public List<DropDownListUIObject> ItemList { get; set; }
        public List<DropDownListUIObject> ScriptList { get; set; }
        public List<DropDownListUIObject> GenderList { get; set; }
        public List<DropDownListUIObject> ConversationList { get; set; }
        public List<DropDownListUIObject> RaceList { get; set; }
        public List<DropDownListUIObject> FactionList { get; set; }
        public List<DropDownListUIObject> TilesetList { get; set; }

        #endregion

        public ToolsetViewModel()
        {
            IsModuleOpened = false;
            CurrentObjectMode = "";
            CurrentObjectTabSelector = "";
            CurrentObjectTreeSelector = "";
            ActiveModule = new GameModule(true);
            ActiveArea = new Area(true);
            ActiveConversation = new Conversation(true);
            ActiveCreature = new Creature(true);
            ActiveItem = new Item(true);
            ActivePlaceable = new Placeable(true);
            ActiveScript = new Script();
            ActiveTileset = new Tileset();
            ModuleList = new List<GameModule>();
            AvailableContentPackages = new List<ContentPackage>();
            AttachedContentPackages = new List<ContentPackage>();
            TilesetSpriteSheetsList = new List<DropDownListUIObject>();
            ItemList = new List<DropDownListUIObject>();
            ScriptList = new List<DropDownListUIObject>();
            GenderList = new List<DropDownListUIObject>();
            ConversationList = new List<DropDownListUIObject>();
            RaceList = new List<DropDownListUIObject>();
            FactionList = new List<DropDownListUIObject>();
            TilesetList = new List<DropDownListUIObject>();
        }
    }
}
