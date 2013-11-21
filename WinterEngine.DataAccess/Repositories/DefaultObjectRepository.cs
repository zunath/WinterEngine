using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess.Repositories
{
    public class DefaultObjectRepository : RepositoryBase, IDisposable
    {
        public DefaultObjectRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }


        /// <summary>
        /// Returns all of the ID numbers of the "default" objects.
        /// </summary>
        /// <returns></returns>
        public DefaultObjectIDs GetDefaultObjectIDs()
        {
            DefaultObjectIDs result = new DefaultObjectIDs();
            Category defaultCategoryArea = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Area);
            Category defaultCategoryConversation = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Conversation);
            Category defaultCategoryCreature = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Creature);
            Category defaultCategoryItem = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Item);
            Category defaultCategoryPlaceable = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Placeable);
            Category defaultCategoryScript = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Script);
            Category defaultCategoryTileset = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.Tileset);
            Category defaultCategoryGameModule = Context.ResourceCategories.FirstOrDefault(x => x.IsDefault && x.GameObjectType == GameObjectTypeEnum.GameModule);

            result.CategoryAreaID = defaultCategoryArea == null ? 0 : defaultCategoryArea.ResourceID;
            result.CategoryConversationID = defaultCategoryConversation == null ? 0 : defaultCategoryConversation.ResourceID;
            result.CategoryCreatureID = defaultCategoryCreature == null ? 0 : defaultCategoryCreature.ResourceID;
            result.CategoryItemID = defaultCategoryItem == null ? 0 : defaultCategoryItem.ResourceID;
            result.CategoryPlaceableID = defaultCategoryPlaceable == null ? 0 : defaultCategoryPlaceable.ResourceID;
            result.CategoryScriptID = defaultCategoryScript == null ? 0 : defaultCategoryScript.ResourceID;
            result.CategoryTilesetID = defaultCategoryTileset == null ? 0 : defaultCategoryTileset.ResourceID;
            result.CategoryGameModuleID = defaultCategoryGameModule == null ? 9 : defaultCategoryGameModule.ResourceID;

            ContentPackageResource defaultContentPackageResourceBGM = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.BGM);
            ContentPackageResource defaultContentPackageResourceCharacter = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Character);
            ContentPackageResource defaultContentPackageResourceItem = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Item);
            ContentPackageResource defaultContentPackageResourcePlaceable = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Placeable);
            ContentPackageResource defaultContentPackageResourceSoundEffect = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.SoundEffect);
            ContentPackageResource defaultContentPackageResourceTileset = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.Tileset);
            ContentPackageResource defaultContentPackageResourceNone = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault && x.ContentPackageResourceType == ContentPackageResourceTypeEnum.None);

            result.ContentPackageResourceBGMID = defaultContentPackageResourceBGM == null ? 0 : defaultContentPackageResourceBGM.ResourceID;
            result.ContentPackageResourceCharacterID = defaultContentPackageResourceCharacter == null ? 0 : defaultContentPackageResourceCharacter.ResourceID;
            result.ContentPackageResourceItemID = defaultContentPackageResourceItem == null ? 0 : defaultContentPackageResourceItem.ResourceID;
            result.ContentPackageResourcePlaceableID = defaultContentPackageResourcePlaceable == null ? 0 : defaultContentPackageResourcePlaceable.ResourceID;
            result.ContentPackageResourceSoundEffectID = defaultContentPackageResourceSoundEffect == null ? 0 : defaultContentPackageResourceSoundEffect.ResourceID;
            result.ContentPackageResourceTilesetID = defaultContentPackageResourceTileset == null ? 0 : defaultContentPackageResourceTileset.ResourceID;
            result.ContentPackageResourceNoneID = defaultContentPackageResourceNone == null ? 0 : defaultContentPackageResourceNone.ResourceID;

            Conversation defaultConversation = Context.Conversations.FirstOrDefault(x => x.IsDefault);
            result.ConversationID = defaultConversation == null ? 0 : defaultConversation.ResourceID;

            Faction defaultFaction = Context.Factions.FirstOrDefault(x => x.IsDefault);
            result.FactionID = defaultFaction == null ? 0 : defaultFaction.ResourceID;

            Gender defaultGender = Context.Genders.FirstOrDefault(x => x.IsDefault);
            result.GenderID = defaultGender == null ? 0 : defaultGender.ResourceID;

            Item defaultItem = Context.Items.FirstOrDefault(x => x.IsDefault);
            result.ItemID = defaultItem == null ? 0 : defaultItem.ResourceID;

            Race defaultRace = Context.Races.FirstOrDefault(x => x.IsDefault);
            result.RaceID = defaultRace == null ? 0 : defaultRace.ResourceID;

            Script defaultScript = Context.Scripts.FirstOrDefault(x => x.IsDefault);
            result.ScriptID = defaultScript == null ? 0 : defaultScript.ResourceID;

            Tileset defaultTileset = Context.Tilesets.FirstOrDefault(x => x.IsDefault);
            result.TilesetID = defaultTileset == null ? 0 : defaultTileset.ResourceID; 
            
            return result;
        }

        public void Dispose()
        {
            base.Dispose();
        }
    }
}
