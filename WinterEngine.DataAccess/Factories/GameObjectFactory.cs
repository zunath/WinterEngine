﻿using System;
using System.Collections.Generic;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
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
        public GameObjectBase CreateObject(GameObjectTypeEnum resourceType)
        {
            switch (resourceType)
            {
                case GameObjectTypeEnum.Area:
                    return new Area { GameObjectType = resourceType };
                case GameObjectTypeEnum.Conversation:
                    return new Conversation { GameObjectType = resourceType };
                case GameObjectTypeEnum.Creature:
                    return new Creature { GameObjectType = resourceType };
                case GameObjectTypeEnum.Item:
                    return new Item { GameObjectType = resourceType };
                case GameObjectTypeEnum.Placeable:
                    return new Placeable { GameObjectType = resourceType };
                case GameObjectTypeEnum.Script:
                    return new Script { GameObjectType = resourceType };
                default:
                    throw new Exception("Game object type not supported.");
            }
        }

        #endregion

        #region Database methods

        /// <summary>
        /// Adds a game object to the database.
        /// </summary>
        /// <param name="winterObject">The game object to add to the database. This will be type-converted and added to the correct table when run.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public void AddToDatabase(GameObjectBase winterObject, string connectionString = "")
        {
            if (winterObject.GameObjectType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Add(winterObject as Area);
                }
            }
            else if (winterObject.GameObjectType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository())
                {
                    repo.Add(winterObject as Conversation);
                }
            }
            else if (winterObject.GameObjectType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Add(winterObject as Creature);
                }
            }
            else if (winterObject.GameObjectType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Add(winterObject as Item);
                }
            }
            else if (winterObject.GameObjectType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Add(winterObject as Placeable);
                }
            }
            else if (winterObject.GameObjectType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository())
                {
                    repo.Add(winterObject as Script);
                }
            }
            else
            {
                throw new NotSupportedException();
            }

        }

        /// <summary>
        /// Adds a list of game objects to the database. 
        /// All objects in the list must be of the specified resourceType or errors may occur.
        /// </summary>
        /// <param name="gameObjectList"></param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        /// <param name="resourceType"></param>
        public void AddToDatabase(List<GameObjectBase> gameObjectList, GameObjectTypeEnum resourceType, string connectionString = "")
        {
            if (Object.ReferenceEquals(gameObjectList[0], null))
            {
                throw new NullReferenceException("gameObjectList is null.");
            }

            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    foreach (Area area in gameObjectList)
                    {
                        repo.Add(area);
                    }
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    foreach (Conversation conversation in gameObjectList)
                    {
                        repo.Add(conversation);
                    }
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    foreach (Creature creature in gameObjectList)
                    {
                        repo.Add(creature);
                    }
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    foreach (Item item in gameObjectList)
                    {
                        repo.Add(item);
                    }
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    foreach (Placeable placeable in gameObjectList)
                    {
                        repo.Add(placeable);
                    }
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    foreach (Script script in gameObjectList)
                    {
                        repo.Add(script);
                    }
                }
            }
            else
            {
                throw new NotSupportedException();
            }
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
            else
            {
                throw new NotSupportedException();
            }
        }


        /// <summary>
        /// Updates a gameObject's entry in the database.
        /// If an object is not found, the object will be added to the database.
        /// </summary>
        /// <param name="gameObject">The game object to update. Its resref will be searched for in the database.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
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
        public void DeleteFromDatabase(string resref, GameObjectTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    repo.Delete(resref);
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
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns an object from the database with a matching resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        /// <returns></returns>
        public GameObjectBase GetFromDatabaseByResref(string resref, GameObjectTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (ConversationRepository repo = new ConversationRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (ScriptRepository repo = new ScriptRepository(connectionString))
                {
                    return repo.GetByResref(resref);
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
            else
            {
                throw new NotSupportedException();
            }
        }

        #endregion
    }
}
