using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess
{
    public class EntityCreationScripts
    {
        public void Initialize()
        {
            Category defaultCategoryArea;
            Category defaultCategoryItem;
            Category defaultCategoryPlaceable;
            Category defaultCategoryCreature;
            Category defaultCategoryConversation;
            Category defaultCategoryScript;
            Category defaultCategoryTileset;
            Gender defaultGender;
            Race defaultRace;
            Script defaultScript;
            ContentPackageResource defaultContentPackageResourceBGM;
            ContentPackageResource defaultContentPackageResourceCharacter;
            ContentPackageResource defaultContentPackageResourceItem;
            ContentPackageResource defaultContentPackageResourceNone;
            ContentPackageResource defaultContentPackageResourcePlaceable;
            ContentPackageResource defaultContentPackageResourceSoundEffect;
            ContentPackageResource defaultContentPackageResourceTileset;
            Faction defaultFaction;
            Conversation defaultConversation;
            Item defaultItem;
            Tileset defaultTileset;

            using (UnitOfWork context = new UnitOfWork())
            {
                #region Category Defaults
                try
                {
                    defaultCategoryArea = context.CategoryRepository.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Area,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryConversation = context.CategoryRepository.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Conversation,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryCreature = context.CategoryRepository.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Creature,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryItem = context.CategoryRepository.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Item,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryPlaceable = context.CategoryRepository.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Placeable,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryScript = context.CategoryRepository.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Script,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryTileset = context.CategoryRepository.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Tileset,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });

                    context.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding categories.", ex);
                }
            
                #endregion

                try
                {
                    defaultGender = context.GenderRepository.Add(new Gender
                    {
                        GenderType = GenderTypeEnum.Unknown,
                        IsSystemResource = true,
                        IsDefault = true,
                        Name = "(None)"

                    });
                    
                    context.GenderRepository.Add(new Gender
                    {
                        GenderType = GenderTypeEnum.Male,
                        IsSystemResource = true,
                        Name = "Male"
                    });

                    context.GenderRepository.Add(new Gender
                    {
                        GenderType = GenderTypeEnum.Female,
                        IsSystemResource = true,
                        Name = "Female"
                    });

                    context.Save();
                }
                catch(Exception ex)
                {
                    throw new Exception("Error adding genders.", ex);
                }

                try
                {
                    defaultRace = context.RaceRepository.Add(new Race
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)"
                    });

                    context.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding races.", ex);
                }

                try
                {
                    defaultScript = context.ScriptRepository.Add(new Script
                    {
                        IsDefault = true,
                        IsInTreeView = false,
                        IsSystemResource = true,
                        Name = "(None)",
                        Tag = "",
                        Resref = "",
                        ResourceCategoryID = defaultCategoryScript.ResourceID 
                    });


                    context.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding scripts.", ex);
                }
                try
                {
                    defaultContentPackageResourceBGM = context.ContentPackageResourceRepository.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.BGM
                    });
                    defaultContentPackageResourceCharacter = context.ContentPackageResourceRepository.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Character
                    });
                    defaultContentPackageResourceItem = context.ContentPackageResourceRepository.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Item
                    });
                    defaultContentPackageResourceNone = context.ContentPackageResourceRepository.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.None
                    });
                    defaultContentPackageResourcePlaceable = context.ContentPackageResourceRepository.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Placeable
                    });
                    defaultContentPackageResourceSoundEffect = context.ContentPackageResourceRepository.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.SoundEffect
                    });
                    defaultContentPackageResourceTileset = context.ContentPackageResourceRepository.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Tileset
                    });

                    context.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding content package resources.", ex);
                }
                try
                {
                    defaultFaction = context.FactionRepository.Add(new Faction
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)"                        
                    });

                    context.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding factions.", ex);
                }
                try
                {
                    defaultConversation = context.ConversationRepository.Add(new Conversation
                    {
                        ResourceID = context.Context.Conversations.Count() + 1, // TEMPORARY FIX: Identities are not being generated in EF6 - waiting for bug fix from Microsoft.
                        GraphicResourceID = defaultContentPackageResourceNone.ResourceID,
                        IsDefault = true,
                        IsInTreeView = false,
                        IsSystemResource = true,
                        Name = "(None)",
                        Tag = "",
                        Resref = "",
                        ResourceCategoryID = defaultCategoryConversation.ResourceID
                    });

                    context.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding conversations.", ex);
                }
                try
                {
                    defaultItem = context.ItemRepository.Add(new Item
                    {
                        ResourceID = context.Context.Items.Count() + 1, // TEMPORARY FIX: Identities are not being generated in EF6 - waiting for bug fix from Microsoft.
                        GraphicResourceID = defaultContentPackageResourceItem.ResourceID,
                        IsDefault = true,
                        IsInTreeView = false,
                        IsSystemResource = true,
                        Name = "(None)",
                        Tag = "",
                        Resref = "",
                        OnSpawnEventScriptID = defaultScript.ResourceID,
                        ResourceCategoryID = defaultCategoryItem.ResourceID
                    });

                    context.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding items.", ex);
                }

                try
                {
                    defaultTileset = context.TilesetRepository.Add(new Tileset
                    {
                        ResourceID = context.Context.Tilesets.Count() + 1, // TEMPORARY FIX: Identities are not being generated in EF6 - waiting for bug fix from Microsoft.
                        GraphicResourceID = defaultContentPackageResourceTileset.ResourceID,
                        IsDefault = true,
                        IsInTreeView = false,
                        IsSystemResource = true,
                        Name = "(None)",
                        Tag = "",
                        Resref = "",
                        ResourceCategoryID = defaultCategoryTileset.ResourceID
                    });

                    context.Save();
                }
                catch(Exception ex)
                {
                    throw new Exception("Error adding tilesets.", ex);
                }
            }
        }
    }
}
