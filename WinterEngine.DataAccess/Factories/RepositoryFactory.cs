using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        /*
         Not a big fan of this method.
         May switch to this
         http://stackoverflow.com/questions/10285946/ioc-ninject-and-factories
        */

        [Inject] public IGameObjectRepository<Area> AreaRepository { private get; set; }
        [Inject] public IGameObjectRepository<Conversation> ConversationRepository { private get; set; }
        [Inject] public IGameObjectRepository<Creature> CreatureRepository { private get; set; }
        [Inject] public IGameObjectRepository<Item> ItemRepository { private get; set; }
        [Inject] public IGameObjectRepository<Placeable> PlaceableRepository { private get; set; }
        [Inject] public IGameObjectRepository<Script> ScriptRepository { private get; set; }
        [Inject] public IGameObjectRepository<Tileset> TilesetRepository { private get; set; }
        [Inject] public IGameObjectRepository<GameModule> GameModuleRepository { private get; set; }
        
        [Inject] public IResourceRepository<Ability> AbilityRepository { private get; set; }        
        [Inject] public IResourceRepository<Category> CategoryRepository { private get; set; }
        [Inject] public IResourceRepository<CharacterClass> CharacterClassRepository { private get; set; }
        [Inject] public IResourceRepository<ContentPackage> ContentPackageRepository { private get; set; }
        [Inject] public IResourceRepository<ContentPackageResource> ContentPackageResourceRepository { private get; set; }
        [Inject] public IResourceRepository<ItemProperty> ItemPropertyResource { private get; set; }
        [Inject] public IResourceRepository<ItemType> ItemTypeRepository { private get; set; }
        [Inject] public IResourceRepository<LocalVariable> LocalVariableRepository { private get; set; }
        [Inject] public IResourceRepository<Race> RaceRepository { private get; set; }
        [Inject] public IResourceRepository<Tile> TileRepository { private get; set; }
        [Inject] public IResourceRepository<TileCollisionBox> TileCollisionBoxRepository { private get; set; }

        public IGenericRepository<T> GetGenericRepository<T>()
        {
            if(typeof(T) == typeof(Area))
            {
                return (IGenericRepository<T>)AreaRepository;
            }
            if (typeof(T) == typeof(Conversation))
            {
                return (IGenericRepository<T>)ConversationRepository;
            }
            if (typeof(T) == typeof(Creature))
            {
                return (IGenericRepository<T>)CreatureRepository;
            }
            if (typeof(T) == typeof(Item))
            {
                return (IGenericRepository<T>)ItemRepository;
            }
            if (typeof(T) == typeof(Placeable))
            {
                return (IGenericRepository<T>)PlaceableRepository;
            }
            if (typeof(T) == typeof(Script))
            {
                return (IGenericRepository<T>)ScriptRepository;
            }
            if (typeof(T) == typeof(Tileset))
            {
                return (IGenericRepository<T>)TilesetRepository;
            }
            if (typeof(T) == typeof(Ability))
            {
                return (IGenericRepository<T>)AbilityRepository;
            }
            if (typeof(T) == typeof(Category))
            {
                return (IGenericRepository<T>)CategoryRepository;
            }
            if (typeof(T) == typeof(CharacterClass))
            {
                return (IGenericRepository<T>)CharacterClassRepository;
            }
            if (typeof(T) == typeof(ContentPackage))
            {
                return (IGenericRepository<T>)ContentPackageRepository;
            }
            if (typeof(T) == typeof(ContentPackageResource))
            {
                return (IGenericRepository<T>)ContentPackageResourceRepository;
            }
            if (typeof(T) == typeof(ItemProperty))
            {
                return (IGenericRepository<T>)ItemPropertyResource;
            }
            else if (typeof(T) == typeof(ItemType))
            {
                return (IGenericRepository<T>)ItemTypeRepository;
            }
            else if (typeof(T) == typeof(LocalVariable))
            {
                return (IGenericRepository<T>)LocalVariableRepository;
            }
            else if (typeof(T) == typeof(Race))
            {
                return (IGenericRepository<T>)RaceRepository;
            }
            else if (typeof(T) == typeof(Tile))
            {
                return (IGenericRepository<T>)TileRepository;
            }
            else if (typeof(T) == typeof(GameModule))
            {
                return (IGenericRepository<T>)GameModuleRepository;
            }
            else if (typeof(T) == typeof(TileCollisionBox))
            {
                return (IGenericRepository<T>)TileCollisionBoxRepository;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IGameObjectRepository<T> GetGameObjectRepository<T>() where T : GameObjectBase
        {
            if (typeof(T) == typeof(Area))
            {
                return (IGameObjectRepository<T>)AreaRepository;
            }
            else if (typeof(T) == typeof(Conversation))
            {
                return (IGameObjectRepository<T>)ConversationRepository;
            }
            else if (typeof(T) == typeof(Creature))
            {
                return (IGameObjectRepository<T>)CreatureRepository;
            }
            else if (typeof(T) == typeof(Item))
            {
                return (IGameObjectRepository<T>)ItemRepository;
            }
            else if (typeof(T) == typeof(Placeable))
            {
                return (IGameObjectRepository<T>)PlaceableRepository;
            }
            else if (typeof(T) == typeof(Script))
            {
                return (IGameObjectRepository<T>)ScriptRepository;
            }
            else if (typeof(T) == typeof(Tileset))
            {
                return (IGameObjectRepository<T>)TilesetRepository;
            }
            else if (typeof(T) == typeof(GameModule))
            {
                return (IGameObjectRepository<T>)GameModuleRepository;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IResourceRepository<T> GetResourceRepository<T>() where T : GameResourceBase
        {
            if (typeof(T) == typeof(Ability))
            {
                return (IResourceRepository<T>)AbilityRepository;
            }
            if (typeof(T) == typeof(Category))
            {
                return (IResourceRepository<T>)CategoryRepository;
            }
            if (typeof(T) == typeof(CharacterClass))
            {
                return (IResourceRepository<T>)CharacterClassRepository;
            }
            if (typeof(T) == typeof(ContentPackage))
            {
                return (IResourceRepository<T>)ContentPackageRepository;
            }
            if (typeof(T) == typeof(ContentPackageResource))
            {
                return (IResourceRepository<T>)ContentPackageResourceRepository;
            }
            if (typeof(T) == typeof(ItemProperty))
            {
                return (IResourceRepository<T>)ItemPropertyResource;
            }
            if (typeof(T) == typeof(ItemType))
            {
                return (IResourceRepository<T>)ItemTypeRepository;
            }
            if (typeof(T) == typeof(LocalVariable))
            {
                return (IResourceRepository<T>)LocalVariableRepository;
            }
            if (typeof(T) == typeof(Race))
            {
                return (IResourceRepository<T>)RaceRepository;
            }
            if (typeof(T) == typeof(Tile))
            {
                return (IResourceRepository<T>)TileRepository;
            }
            if (typeof(T) == typeof(TileCollisionBox))
            {
                return (IResourceRepository<T>)TileCollisionBoxRepository;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
