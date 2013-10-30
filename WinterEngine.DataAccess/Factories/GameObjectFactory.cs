using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataAccess.Repositories.Interfaces;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;


namespace WinterEngine.DataAccess.Factories
{
    /// <summary>
    /// Factory pattern class for creating game objects.
    /// </summary>
    public class GameObjectFactory
    {

        [Inject]
        private readonly IRepositoryFactory repositoryFactory;

        #region Object creation methods
        [Inject]
        public IAreaFactory areaFactory { get; set; }
        [Inject] 
        public IConversationFactory conversationFactory { get; set; }
        [Inject] 
        public ICreatureFactory creatureFactory { get; set; }
        [Inject] 
        public IItemFactory itemFactory { get; set; }
        [Inject] 
        public IPlaceableFactory placeableFactory { get; set; }
        [Inject] 
        public IScriptFactory scriptFactory { get; set; }
        [Inject] 
        public ITilesetFactory tilesetFactory { get; set; }

        public GameObjectFactory(IRepositoryFactory repositoryFactory)
        {
            if (repositoryFactory == null) throw new ArgumentNullException("repositoryFactory");
            this.repositoryFactory = repositoryFactory;
        }

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
                    return areaFactory.Create();
                case GameObjectTypeEnum.Conversation:
                    return conversationFactory.Create();
                case GameObjectTypeEnum.Creature:
                    return creatureFactory.Create();
                case GameObjectTypeEnum.Item:
                    return itemFactory.Create();
                case GameObjectTypeEnum.Placeable:
                    return placeableFactory.Create();
                case GameObjectTypeEnum.Script:
                    return scriptFactory.Create();
                case GameObjectTypeEnum.Tileset:
                    return tilesetFactory.Create();
                default:
                    throw new NotSupportedException("Game object type not supported.");
            }
        }

        #endregion

        #region Database methods

        /// <summary>
        /// Adds a game object to the database.
        /// </summary>
        /// <param name="gameObject">The game object to add to the database. This will be type-converted and added to the correct table when run.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public GameObjectBase AddToDatabase(GameObjectBase gameObject)
        {
            GameObjectBase resultGameObject;
            try
            {
                if (gameObject.GameObjectType == GameObjectTypeEnum.Area)
                {
                    using (var repo = repositoryFactory.GetGenericRepository<Area>())
                    {
                        resultGameObject = repo.Add(gameObject as Area);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Conversation)
                {
                    using (var repo = repositoryFactory.GetGenericRepository<Conversation>())
                    {
                        resultGameObject = repo.Add(gameObject as Conversation);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Creature)
                {
                    using (var repo = repositoryFactory.GetGenericRepository<Creature>())
                    {
                        resultGameObject = repo.Add(gameObject as Creature);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Item)
                {
                    using (var repo = repositoryFactory.GetGenericRepository<Item>())
                    {
                        resultGameObject = repo.Add(gameObject as Item);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Placeable)
                {
                    using (var repo = repositoryFactory.GetGenericRepository<Placeable>())
                    {
                        resultGameObject = repo.Add(gameObject as Placeable);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Script)
                {
                    using (var repo = repositoryFactory.GetGenericRepository<Script>())
                    {
                        resultGameObject = repo.Add(gameObject as Script);
                    }
                }
                else if (gameObject.GameObjectType == GameObjectTypeEnum.Tileset)
                {
                    using (var repo = repositoryFactory.GetGenericRepository<Tileset>())
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
        public void AddToDatabase(List<GameObjectBase> gameObjectList)
        {
            foreach (GameObjectBase gameObject in gameObjectList)
            {
                AddToDatabase(gameObject);
            }
        }

        /// <summary>
        /// Updates a gameObject's entry in the database.
        /// An exception will be thrown if an object with a matching resref is not found.
        /// </summary>
        /// <param name="gameObjectBase">The game object to update. Its resref will be searched for in the database.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        public void UpdateInDatabase(GameObjectBase gameObject)
        {
            if (gameObject.GameObjectType == GameObjectTypeEnum.Area)
            {
                using (var repo = repositoryFactory.GetGenericRepository<Area>())
                {
                    repo.Update(gameObject as Area);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Conversation)
            {
                using (var repo = repositoryFactory.GetGenericRepository<Conversation>())
                {
                    repo.Update(gameObject as Conversation);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Creature)
            {
                using (var repo = repositoryFactory.GetGenericRepository<Creature>())
                {
                    repo.Update(gameObject as Creature);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Item)
            {
                using (var repo = repositoryFactory.GetGenericRepository<Item>())
                {
                    repo.Update(gameObject as Item);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Placeable)
            {
                using (var repo = repositoryFactory.GetGenericRepository<Placeable>())
                {
                    repo.Update(gameObject as Placeable);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Script)
            {
                using (var repo = repositoryFactory.GetGenericRepository<Script>())
                {
                    repo.Update(gameObject as Script);
                }
            }
            else if (gameObject.GameObjectType == GameObjectTypeEnum.Tileset)
            {
                using (var repo = repositoryFactory.GetGenericRepository<Tileset>())
                {
                    repo.Update(gameObject as Tileset);
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
        public void DeleteFromDatabase(int resourceID, GameObjectTypeEnum resourceType)
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Area>())
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Conversation>())
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Creature>())
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Item>())
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Placeable>())
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Script>())
                {
                    repo.Delete(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Tileset>())
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
        public List<GameObjectBase> GetAllFromDatabase(GameObjectTypeEnum resourceType)
        {
            List<GameObjectBase> result = new List<GameObjectBase>();
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Area>())
                {
                    result.AddRange(repo.GetAll());
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Conversation>())
                {
                    result.AddRange(repo.GetAll());
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Creature>())
                {
                    result.AddRange(repo.GetAll());
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Item>())
                {
                    result.AddRange(repo.GetAll());
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Placeable>())
                {
                    result.AddRange(repo.GetAll());
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Script>())
                {
                    result.AddRange(repo.GetAll());
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Tileset>())
                {
                    result.AddRange(repo.GetAll());
                }
            }
            else
            {
                throw new NotSupportedException();
            }
            return result;
        }

        /// <summary>
        /// Returns all objects from the database that have a matching resource category.
        /// </summary>
        /// <param name="resourceCategory">The resource category all return values must match</param>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <param name="connectionString">If you need to connect to a specific database, use this to pass the connection string. Otherwise, the default connection string will be used (WinterConnectionInformation.ActiveConnectionString)</param>
        /// <returns></returns>
        public List<GameObjectBase> GetAllFromDatabaseByResourceCategory(Category resourceCategory, GameObjectTypeEnum resourceType)
        {
            List<GameObjectBase> result = new List<GameObjectBase>();
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Area>())
                {
                    var temp = repo.GetAllByResourceCategory(resourceCategory);
                    result.AddRange(temp);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Conversation>())
                {
                    var temp = repo.GetAllByResourceCategory(resourceCategory);
                    result.AddRange(temp);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Creature>())
                {
                    var temp = repo.GetAllByResourceCategory(resourceCategory);
                    result.AddRange(temp);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Item>())
                {
                    var temp = repo.GetAllByResourceCategory(resourceCategory);
                    result.AddRange(temp);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Placeable>())
                {
                    var temp = repo.GetAllByResourceCategory(resourceCategory);
                    result.AddRange(temp);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Script>())
                {
                    var temp = repo.GetAllByResourceCategory(resourceCategory);
                    result.AddRange(temp);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Tileset>())
                {
                    var temp = repo.GetAllByResourceCategory(resourceCategory);
                    result.AddRange(temp);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
            return result;
        }

        public GameObjectBase GetFromDatabaseByID(int resourceID, GameObjectTypeEnum resourceType)
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Area>())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Conversation>())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Creature>())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Item>())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Placeable>())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Script>())
                {
                    return repo.GetByID(resourceID);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Tileset>())
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
        public void DeleteFromDatabaseByCategory(Category resourceCategory, GameObjectTypeEnum resourceType)
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Area>())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Conversation>())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Creature>())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Item>())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Placeable>())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Script>())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Tileset>())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else
            {
                throw new NotSupportedException();
            }

            // Now remove the category itself.
            using (var repo = repositoryFactory.GetResourceRepository<Category>())
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
        public bool DoesObjectExistInDatabase(string resref, GameObjectTypeEnum resourceType)
        {
            if (resourceType == GameObjectTypeEnum.Area)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Area>())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Conversation)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Conversation>())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Creature)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Creature>())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Item)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Item>())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Placeable)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Placeable>())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Script)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Script>())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == GameObjectTypeEnum.Tileset)
            {
                using (var repo = repositoryFactory.GetGameObjectRepository<Tileset>())
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
