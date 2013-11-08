using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using WinterEngine.DataTransferObjects.Enumerations;


namespace WinterEngine.DataTransferObjects
{
    /// <summary>
    /// Factory pattern class for creating game objects.
    /// </summary>
    public class GameObjectFactory : IGameObjectFactory
    {


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

        public GameObjectFactory()
        {
            
        }

        /// <summary>
        /// Creates and returns a new object of the specified type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public GameObjectBase Create(GameObjectTypeEnum resourceType)
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
    }
}
