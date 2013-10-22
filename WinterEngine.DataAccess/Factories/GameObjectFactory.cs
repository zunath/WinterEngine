using System;
using System.Collections.Generic;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;



namespace WinterEngine.DataAccess.Factories
{
    /// <summary>
    /// Factory pattern class for creating game objects.
    /// </summary>
    public class GameObjectFactory
    {
        #region Object creation methods

        /// <summary>
        /// Creates and returns a new object of the specified type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public GameObjectBase CreateObject(GameObjectTypeEnum resourceType, string name, string tag, string resref)
        {
            DefaultObjectIDs defaultIDs;

            using (DefaultObjectRepository repo = new DefaultObjectRepository())
            {
                defaultIDs = repo.GetDefaultObjectIDs();
            }

            switch (resourceType)
            {
                case GameObjectTypeEnum.Area:
                    {
                        return new Area
                        {
                            Name = name,
                            Tag = tag,
                            Resref = resref,
                            GameObjectType = resourceType,
                            ResourceCategoryID = defaultIDs.CategoryAreaID,
                            OnEnterEventScriptID = defaultIDs.ScriptID,
                            OnExitEventScriptID = defaultIDs.ScriptID,
                            OnHeartbeatEventScriptID = defaultIDs.ScriptID,
                            OnUserDefinedEventScriptID = defaultIDs.ScriptID,
                            IsInTreeView = true,
                            GraphicResourceID = defaultIDs.ContentPackageResourceNoneID,
                            TilesetID = defaultIDs.TilesetID
                        };
                    }
                case GameObjectTypeEnum.Conversation:
                    {
                        return new Conversation
                        {
                            Name = name,
                            Tag = tag,
                            Resref = resref,
                            GameObjectType = resourceType,
                            ResourceCategoryID = defaultIDs.CategoryConversationID,
                            IsInTreeView = true,
                            GraphicResourceID = defaultIDs.ContentPackageResourceNoneID

                        };
                    }
                case GameObjectTypeEnum.Creature:
                    {
                        return new Creature
                        {
                            Name = name,
                            Tag = tag,
                            Resref = resref,
                            GameObjectType = resourceType,
                            Constitution = 1,
                            Dexterity = 1,
                            HitPoints = 1,
                            Intelligence = 1,
                            Level = 1,
                            MaxHitPoints = 1,
                            MaxMana = 1,
                            Strength = 1,
                            Wisdom = 1,
                            IsInvulnerable = false,
                            BackItemID = defaultIDs.ItemID,
                            BodyItemID = defaultIDs.ItemID,
                            ConversationID = defaultIDs.ConversationID,
                            EarLeftItemID = defaultIDs.ItemID,
                            EarRightItemID = defaultIDs.ItemID,
                            FactionID = defaultIDs.FactionID,
                            FeetItemID = defaultIDs.ItemID,
                            GenderID = defaultIDs.GenderID,
                            HandsItemID = defaultIDs.ItemID,
                            HeadItemID = defaultIDs.ItemID,
                            LegsItemID = defaultIDs.ItemID,
                            MainHandItemID = defaultIDs.ItemID,
                            NeckItemID = defaultIDs.ItemID,
                            OffHandItemID = defaultIDs.ItemID,
                            OnConversationEventScriptID = defaultIDs.ScriptID,
                            OnDamagedEventScriptID = defaultIDs.ScriptID,
                            OnDeathEventScriptID = defaultIDs.ScriptID,
                            OnHeartbeatEventScriptID = defaultIDs.ScriptID,
                            OnSpawnEventScriptID = defaultIDs.ScriptID,
                            RaceID = defaultIDs.RaceID,
                            ResourceCategoryID = defaultIDs.CategoryCreatureID,
                            RingLeftItemID = defaultIDs.ItemID,
                            RingRightItemID = defaultIDs.ItemID,
                            WaistItemID = defaultIDs.ItemID,
                            IsInTreeView = true,
                            GraphicResourceID = defaultIDs.ContentPackageResourceCharacterID
                        };
                    }
                case GameObjectTypeEnum.Item:
                    {
                        return new Item
                        {
                            Name = name,
                            Tag = tag,
                            Resref = resref,
                            GameObjectType = resourceType,
                            GraphicResourceID = defaultIDs.ContentPackageResourceItemID,
                            OnSpawnEventScriptID = defaultIDs.ScriptID,
                            ResourceCategoryID = defaultIDs.CategoryItemID,
                            IsInTreeView = true
                        };
                    }
                case GameObjectTypeEnum.Placeable:
                    {
                        return new Placeable
                        {
                            Name = name,
                            Tag = tag,
                            Resref = resref,
                            GameObjectType = resourceType,
                            OnCloseEventScriptID = defaultIDs.ScriptID,
                            OnDamagedEventScriptID = defaultIDs.ScriptID,
                            OnDeathEventScriptID = defaultIDs.ScriptID,
                            OnHeartbeatEventScriptID = defaultIDs.ScriptID,
                            OnOpenEventScriptID = defaultIDs.ScriptID,
                            OnSpawnEventScriptID = defaultIDs.ScriptID,
                            OnUsedEventScriptID = defaultIDs.ScriptID,
                            ResourceCategoryID = defaultIDs.CategoryPlaceableID,
                            IsInTreeView = true,
                            GraphicResourceID = defaultIDs.ContentPackageResourcePlaceableID
                        };
                    }
                case GameObjectTypeEnum.Script:
                    {
                        return new Script
                        {
                            Name = name,
                            Tag = tag,
                            Resref = resref,
                            GameObjectType = resourceType,
                            ResourceCategoryID = defaultIDs.CategoryScriptID,
                            IsInTreeView = true,
                            GraphicResourceID = defaultIDs.ContentPackageResourceNoneID
                        };
                    }
                case GameObjectTypeEnum.Tileset:
                    {
                        return new Tileset
                        {
                            Name = name,
                            Tag = tag,
                            Resref = resref,
                            GameObjectType = resourceType,
                            ResourceCategoryID = defaultIDs.CategoryTilesetID,
                            IsInTreeView = true,
                            GraphicResourceID = defaultIDs.ContentPackageResourceTilesetID
                        };
                    }
                default:
                    throw new Exception("Game object type not supported.");
            }
        }

        #endregion

        #region Database methods

        /// <summary>
        /// Adds a game object to the database.
        /// </summary>
        /// <param name="gameObject">The game object to add to the database. This will be type-converted and added to the correct table when run.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public GameObjectBase AddToDatabase(GameObjectBase gameObject, string connectionString = "")
        {
            GameObjectBase resultGameObject;
            try
            {
                if (gameObject.GameObjectType == GameObjectTypeEnum.Area)
                {
                    using (AreaRepository repo = new AreaRepository(connectionString))
                    {
                        resultGameObject = repo.Add(gameObject as Area);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Conversation)
                {
                    using (ConversationRepository repo = new ConversationRepository())
                    {
                        resultGameObject = repo.Add(gameObject as Conversation);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Creature)
                {
                    using (CreatureRepository repo = new CreatureRepository(connectionString))
                    {
                        resultGameObject = repo.Add(gameObject as Creature);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Item)
                {
                    using (ItemRepository repo = new ItemRepository(connectionString))
                    {
                        resultGameObject = repo.Add(gameObject as Item);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Placeable)
                {
                    using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                    {
                        resultGameObject = repo.Add(gameObject as Placeable);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Script)
                {
                    using (ScriptRepository repo = new ScriptRepository(connectionString))
                    {
                        resultGameObject = repo.Add(gameObject as Script);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Tileset)
                {
                    using (TilesetRepository repo = new TilesetRepository(connectionString))
                    {
                        resultGameObject = repo.Add(gameObject as Tileset);
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            catch
            {
                throw;
            }

            return resultGameObject;
        }

        /// <summary>
        /// Adds a list of game objects to the database.
        /// Objects may be of different types.
        /// Note that if all objects are of the same type, using the overloaded method which uses ResourceTypeEnum will be quicker on the processor.
        /// </summary>
        /// <param name="gameObjectList"></param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public void AddToDatabase(List<GameObjectBase> gameObjectList, string connectionString = "")
        {
            foreach (GameObjectBase gameObject in gameObjectList)
            {
                AddToDatabase(gameObject, connectionString);
            }
        }

        /// <summary>
        /// Updates a gameObject's entry in the database.
        /// An exception will be thrown if an object with a matching resref is not found.
        /// </summary>
        /// <param name="gameObjectBase">The game object to update. Its resref will be searched for in the database.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public void UpdateInDatabase(GameObjectBase gameObject, string connectionString = "")
        {
            if (gameObject.GameObjectType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Update(gameObject as Area);
                    
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    repo.Update(gameObject as Conversation);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Update(gameObject as Creature);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Update(gameObject as Item);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Update(gameObject as Placeable);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    repo.Update(gameObject as Script);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository(connectionString))
                {
                    repo.Update(gameObject as Tileset);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void UpsertInDatabase(GameObjectBase gameObject, string connectionString = "")
        {
            if (gameObject.GameObjectType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Upsert(gameObject as Area);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    repo.Upsert(gameObject as Conversation);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Upsert(gameObject as Creature);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Upsert(gameObject as Item);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Upsert(gameObject as Placeable);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    repo.Upsert(gameObject as Script);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository(connectionString))
                {
                    repo.Upsert(gameObject as Tileset);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Deletes an object with the specified resref from the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <param name="resourceType">The type of resource to remove.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public void DeleteFromDatabase(int resourceID, GameObjectTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository(connectionString))
                {
                    repo.Delete(resourceID);
                }
            }
            else
            {
                throw new NotSupportedException();
            }

        }

        /// <summary>
        /// Returns a list of all objects in the database of a specified resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        /// <returns></returns>
        public List<GameObjectBase> GetAllFromDatabase(GameObjectTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns all objects from the database that have a matching resource category.
        /// </summary>
        /// <param name="resourceCategory">The resource category all return values must match</param>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        /// <returns></returns>
        public List<GameObjectBase> GetAllFromDatabaseByResourceCategory(Category resourceCategory, GameObjectTypeEnum resourceType, string connectionString = "")
        {
            List<GameObjectBase> retList = new List<GameObjectBase>();

            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public GameObjectBase GetFromDatabaseByID(int resourceID, GameObjectTypeEnum gameResourceType)
        {
            if (gameResourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (gameResourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (gameResourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (gameResourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (gameResourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (gameResourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (gameResourceType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Delete all objects from database that match a specified resource category.
        /// </summary>
        /// <param name="resourceCategory">The resource category to remove all objects from.</param>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public void DeleteFromDatabaseByCategory(Category resourceCategory, GameObjectTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else
            {
                throw new NotSupportedException();
            }

            // Now remove the category itself.
            using (CategoryRepository repo = new CategoryRepository())
            {
                Category dbCategory = repo.GetByID(resourceCategory.ResourceID);
                repo.Delete(dbCategory);
            }
        }

        /// <summary>
        /// Returns True if an object exists in the database.
        /// Returns False if an object does not exist in the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <param name="resourceType">The resource type to look for.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        /// <returns></returns>
        public bool DoesObjectExistInDatabase(string resref, GameObjectTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (TilesetRepository repo = new TilesetRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        #endregion

    }
}
