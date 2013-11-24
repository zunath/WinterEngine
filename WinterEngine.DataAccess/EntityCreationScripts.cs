using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
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
            Category defaultCategoryGameModule;
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

            using (ModuleDataContext context = new ModuleDataContext())
            {
                #region Category Defaults
                try
                {
                    defaultCategoryArea = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Area,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryConversation = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Conversation,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryCreature = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Creature,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryItem = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Item,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryPlaceable = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Placeable,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryScript = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Script,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryTileset = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.Tileset,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });
                    defaultCategoryGameModule = context.ResourceCategories.Add(new Category
                    {
                        Name = "*Uncategorized",
                        GameObjectType = GameObjectTypeEnum.GameModule,
                        ResourceType = ResourceTypeEnum.GameObject,
                        IsSystemResource = true,
                        IsDefault = true,
                    });

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding categories.", ex);
                }
            
                #endregion

                try
                {
                    defaultGender = context.Genders.Add(new Gender
                    {
                        GenderType = GenderTypeEnum.Unknown,
                        IsSystemResource = true,
                        IsDefault = true,
                        Name = "(None)"

                    });
                    
                    context.Genders.Add(new Gender
                    {
                        GenderType = GenderTypeEnum.Male,
                        IsSystemResource = true,
                        Name = "Male"
                    });

                    context.Genders.Add(new Gender
                    {
                        GenderType = GenderTypeEnum.Female,
                        IsSystemResource = true,
                        Name = "Female"
                    });

                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw new Exception("Error adding genders.", ex);
                }

                try
                {
                    defaultRace = context.Races.Add(new Race
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)"
                    });

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding races.", ex);
                }
                try
                {
                    defaultContentPackageResourceBGM = context.ContentPackageResources.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.BGM
                    });
                    defaultContentPackageResourceCharacter = context.ContentPackageResources.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Character
                    });
                    defaultContentPackageResourceItem = context.ContentPackageResources.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Item
                    });
                    defaultContentPackageResourceNone = context.ContentPackageResources.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.None
                    });
                    defaultContentPackageResourcePlaceable = context.ContentPackageResources.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Placeable
                    });
                    defaultContentPackageResourceSoundEffect = context.ContentPackageResources.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.SoundEffect
                    });
                    defaultContentPackageResourceTileset = context.ContentPackageResources.Add(new ContentPackageResource
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)",
                        ContentPackageResourceType = ContentPackageResourceTypeEnum.Tileset
                    });

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding content package resources.", ex);
                }

                try
                {
                    defaultScript = context.Scripts.Add(new Script
                    {
                        IsDefault = true,
                        IsInTreeView = false,
                        IsSystemResource = true,
                        Name = "(None)",
                        Tag = "",
                        Resref = "",
                        ResourceCategoryID = defaultCategoryScript.ResourceID,
                        GraphicResourceID = defaultContentPackageResourceNone.ResourceID
                    });


                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding scripts.", ex);
                }
                
                try
                {
                    defaultFaction = context.Factions.Add(new Faction
                    {
                        IsDefault = true,
                        IsSystemResource = true,
                        Name = "(None)"                        
                    });

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding factions.", ex);
                }
                try
                {
                    defaultConversation = context.Conversations.Add(new Conversation
                    {
                        GraphicResourceID = defaultContentPackageResourceNone.ResourceID,
                        IsDefault = true,
                        IsInTreeView = false,
                        IsSystemResource = true,
                        Name = "(None)",
                        Tag = "",
                        Resref = "",
                        ResourceCategoryID = defaultCategoryConversation.ResourceID
                    });

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding conversations.", ex);
                }
                try
                {
                    defaultItem = context.Items.Add(new Item
                    {
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

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding items.", ex);
                }

                try
                {
                    defaultTileset = context.Tilesets.Add(new Tileset
                    {
                        GraphicResourceID = defaultContentPackageResourceTileset.ResourceID,
                        IsDefault = true,
                        IsInTreeView = false,
                        IsSystemResource = true,
                        Name = "(None)",
                        Tag = "",
                        Resref = "",
                        ResourceCategoryID = defaultCategoryTileset.ResourceID
                    });

                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw new Exception("Error adding tilesets.", ex);
                }

                try
                {
                    List<LevelRequirement> levels = new List<LevelRequirement>();

                    for (int current = 1; current <= 99; current++)
                    {
                        levels.Add(new LevelRequirement
                        {
                            NewAbilities = 0,
                            ExperienceRequired = current * 500,
                            IsDefault = false,
                            IsSystemResource = false,
                            Level = current,
                            SkillPoints = 0,
                        });
                    }

                    context.LevelRequirements.AddRange(levels);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding level requirements.", ex);
                }
            }
        }
    }
}
