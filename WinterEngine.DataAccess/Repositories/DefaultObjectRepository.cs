using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess.Repositories
{
    public class DefaultObjectRepository
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        public DefaultObjectRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }


        /// <summary>
        /// Returns all of the ID numbers of the "default" objects.
        /// </summary>
        /// <returns></returns>
        public DefaultObjectIDs GetDefaultObjectIDs()
        {
            DefaultObjectIDs result = new DefaultObjectIDs();
            Category defaultCategoryArea = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Area).FirstOrDefault();
            Category defaultCategoryConversation = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Conversation).FirstOrDefault();
            Category defaultCategoryCreature = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Creature).FirstOrDefault();
            Category defaultCategoryItem = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Item).FirstOrDefault();
            Category defaultCategoryPlaceable = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Placeable).FirstOrDefault();
            Category defaultCategoryScript = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Script).FirstOrDefault();
            Category defaultCategoryTileset = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Tileset).FirstOrDefault();
            Category defaultCategoryGameModule = _context.ResourceCategories.Where(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.GameModule).FirstOrDefault();

            result.CategoryAreaID = defaultCategoryArea == null ? 0 : defaultCategoryArea.ResourceID;
            result.CategoryConversationID = defaultCategoryConversation == null ? 0 : defaultCategoryConversation.ResourceID;
            result.CategoryCreatureID = defaultCategoryCreature == null ? 0 : defaultCategoryCreature.ResourceID;
            result.CategoryItemID = defaultCategoryItem == null ? 0 : defaultCategoryItem.ResourceID;
            result.CategoryPlaceableID = defaultCategoryPlaceable == null ? 0 : defaultCategoryPlaceable.ResourceID;
            result.CategoryScriptID = defaultCategoryScript == null ? 0 : defaultCategoryScript.ResourceID;
            result.CategoryTilesetID = defaultCategoryTileset == null ? 0 : defaultCategoryTileset.ResourceID;
            result.CategoryGameModuleID = defaultCategoryGameModule == null ? 9 : defaultCategoryGameModule.ResourceID;

            ContentPackageResource defaultContentPackageResourceBGM = _context.ContentPackageResources.Where(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.BGM).FirstOrDefault();
            ContentPackageResource defaultContentPackageResourceCharacter = _context.ContentPackageResources.Where(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Character).FirstOrDefault();
            ContentPackageResource defaultContentPackageResourceItem = _context.ContentPackageResources.Where(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Item).FirstOrDefault();
            ContentPackageResource defaultContentPackageResourcePlaceable = _context.ContentPackageResources.Where(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Placeable).FirstOrDefault();
            ContentPackageResource defaultContentPackageResourceSoundEffect = _context.ContentPackageResources.Where(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.SoundEffect).FirstOrDefault();
            ContentPackageResource defaultContentPackageResourceTileset = _context.ContentPackageResources.Where(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Tileset).FirstOrDefault();
            ContentPackageResource defaultContentPackageResourceNone = _context.ContentPackageResources.Where(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.None).FirstOrDefault();

            result.ContentPackageResourceBGMID = defaultContentPackageResourceBGM == null ? 0 : defaultContentPackageResourceBGM.ResourceID;
            result.ContentPackageResourceCharacterID = defaultContentPackageResourceCharacter == null ? 0 : defaultContentPackageResourceCharacter.ResourceID;
            result.ContentPackageResourceItemID = defaultContentPackageResourceItem == null ? 0 : defaultContentPackageResourceItem.ResourceID;
            result.ContentPackageResourcePlaceableID = defaultContentPackageResourcePlaceable == null ? 0 : defaultContentPackageResourcePlaceable.ResourceID;
            result.ContentPackageResourceSoundEffectID = defaultContentPackageResourceSoundEffect == null ? 0 : defaultContentPackageResourceSoundEffect.ResourceID;
            result.ContentPackageResourceTilesetID = defaultContentPackageResourceTileset == null ? 0 : defaultContentPackageResourceTileset.ResourceID;
            result.ContentPackageResourceNoneID = defaultContentPackageResourceNone == null ? 0 : defaultContentPackageResourceNone.ResourceID;

            Conversation defaultConversation = _context.Conversations.Where(x => x.IsDefault).FirstOrDefault();
            result.ConversationID = defaultConversation == null ? 0 : defaultConversation.ResourceID;

            Faction defaultFaction = _context.Factions.Where(x => x.IsDefault).FirstOrDefault();
            result.FactionID = defaultFaction == null ? 0 : defaultFaction.ResourceID;

            Gender defaultGender = _context.Genders.Where(x => x.IsDefault).FirstOrDefault();
            result.GenderID = defaultGender == null ? 0 : defaultGender.ResourceID;

            Item defaultItem = _context.Items.Where(x => x.IsDefault).FirstOrDefault();
            result.ItemID = defaultItem == null ? 0 : defaultItem.ResourceID;

            Race defaultRace = _context.Races.Where(x => x.IsDefault).FirstOrDefault();
            result.RaceID = defaultRace == null ? 0 : defaultRace.ResourceID;

            Script defaultScript = _context.Scripts.Where(x => x.IsDefault).FirstOrDefault();
            result.ScriptID = defaultScript == null ? 0 : defaultScript.ResourceID;

            Tileset defaultTileset = _context.Tilesets.Where(x => x.IsDefault).FirstOrDefault();
            result.TilesetID = defaultTileset == null ? 0 : defaultTileset.ResourceID; 
            
            return result;
        }
    }
}
