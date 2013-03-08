using System;
using System.Collections.Generic;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Graphics;

namespace WinterEngine.Library.Factories
{
    /// <summary>
    /// Factory method for creating game objects.
    /// </summary>
    public class GameObjectFactory
    {
        #region Object creation methods

        /// <summary>
        /// Creates and returns a new object of the specified type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public GameObjectBase CreateObject(ResourceTypeEnum resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypeEnum.Area:
                    return new Area();
                case ResourceTypeEnum.Conversation:
                    return null;
                case ResourceTypeEnum.Creature:
                    return new Creature();
                case ResourceTypeEnum.Item:
                    return new Item();
                case ResourceTypeEnum.Placeable:
                    return new Placeable();
                case ResourceTypeEnum.Script:
                    return null;
                default:
                    return null;
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
            if (winterObject.ResourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Add(winterObject as Area);
                }
            }
            else if (winterObject.ResourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (winterObject.ResourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Add(winterObject as Creature);
                }
            }
            else if (winterObject.ResourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Add(winterObject as Item);
                }
            }
            else if (winterObject.ResourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Add(winterObject as Placeable);
                }
            }
            else if (winterObject.ResourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
        public void AddToDatabase(List<GameObjectBase> gameObjectList, ResourceTypeEnum resourceType, string connectionString = "")
        {
            if (Object.ReferenceEquals(gameObjectList[0], null))
            {
                throw new NullReferenceException("gameObjectList is null.");
            }

            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    foreach (Area area in gameObjectList)
                    {
                        repo.Add(area);
                    }
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    foreach (Creature creature in gameObjectList)
                    {
                        repo.Add(creature);
                    }
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    foreach (Item item in gameObjectList)
                    {
                        repo.Add(item);
                    }
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    foreach (Placeable placeable in gameObjectList)
                    {
                        repo.Add(placeable);
                    }
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
        /// <param name="gameObject">The game object to update. Its resref will be searched for in the database.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public void UpdateInDatabase(GameObjectBase gameObject, string connectionString = "")
        {
            if (gameObject.ResourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Update(gameObject.Resref, gameObject as Area);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Update(gameObject.Resref, gameObject as Creature);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Update(gameObject.Resref, gameObject as Item);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Update(gameObject.Resref, gameObject as Placeable);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
            if (gameObject.ResourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Upsert(gameObject as Area);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Upsert(gameObject as Creature);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Upsert(gameObject as Item);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Upsert(gameObject as Placeable);
                }
            }
            else if (gameObject.ResourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
        public void DeleteFromDatabase(string resref, ResourceTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
        public List<GameObjectBase> GetAllFromDatabase(ResourceTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.GetAll().ConvertAll<GameObjectBase>(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
        public List<GameObjectBase> GetAllFromDatabaseByResourceCategory(Category resourceCategory, ResourceTypeEnum resourceType, string connectionString = "")
        {
            List<GameObjectBase> retList = new List<GameObjectBase>();

            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObjectBase)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
        public GameObjectBase GetFromDatabaseByResref(string resref, ResourceTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
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
        public void DeleteFromDatabaseByCategory(Category resourceCategory, ResourceTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
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
        public bool DoesObjectExistInDatabase(string resref, ResourceTypeEnum resourceType, string connectionString = "")
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository(connectionString))
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region Object conversion methods

        /// <summary>
        /// Builds independent resource lists based on the gameObjectList passed in.
        /// Item1 = Area List
        /// Item2 = Creature List
        /// Item3 = Item List
        /// Item4 = Placeable List
        /// Item5 = Conversation List
        /// Item6 = Script List
        /// </summary>
        /// <param name="gameObjectList">The list of game objects that will be expanded.</param>
        /// <returns></returns>
        public Tuple<List<Area>, List<Creature>, List<Item>, List<Placeable>> ExpandGameObjectList(List<GameObjectBase> gameObjectList)
        {
            List<Area> areaList = new List<Area>();
            List<Item> itemList = new List<Item>();
            List<Creature> creatureList = new List<Creature>();
            List<Placeable> placeableList = new List<Placeable>();

            foreach (var currentObject in gameObjectList)
            {
                GameObjectBase gameObject = currentObject as GameObjectBase;

                if (gameObject.ResourceType == ResourceTypeEnum.Area)
                {
                    areaList.Add(gameObject as Area);
                }
                else if (gameObject.ResourceType == ResourceTypeEnum.Item)
                {
                    itemList.Add(gameObject as Item);
                }
                else if (gameObject.ResourceType == ResourceTypeEnum.Creature)
                {
                    creatureList.Add(gameObject as Creature);
                }
                else if (gameObject.ResourceType == ResourceTypeEnum.Placeable)
                {
                    placeableList.Add(gameObject as Placeable);
                }
                else if (gameObject.ResourceType == ResourceTypeEnum.Conversation)
                {
                    throw new NotImplementedException();
                }
                else if (gameObject.ResourceType == ResourceTypeEnum.Script)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new NotSupportedException("Resource type is not supported.");
                }
            }

            return new Tuple<List<Area>, List<Creature>, List<Item>, List<Placeable>>(areaList, creatureList, itemList, placeableList);

        }

        #endregion
    }
}
