using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataAccess.Repositories.Interfaces;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        /*
         Not a big fan of this method.
         May switch to this
         http://stackoverflow.com/questions/10285946/ioc-ninject-and-factories
        */

        [Inject] public IGameObjectRepository<Area> areaRepository { private get; set; }
        [Inject] public IGameObjectRepository<Conversation> conversationRepository { private get; set; }
        [Inject] public IGameObjectRepository<Creature> creatureRepository { private get; set; }
        [Inject] public IGameObjectRepository<Item> itemRepository { private get; set; }
        [Inject] public IGameObjectRepository<Placeable> placeableRepository { private get; set; }
        [Inject] public IGameObjectRepository<Script> scriptRepository { private get; set; }
        [Inject] public IGameObjectRepository<Tileset> tilesetRepository { private get; set; }
        
        [Inject] public IResourceRepository<Ability> abilityRepository { private get; set; }        
        [Inject] public IResourceRepository<Category> categoryRepository { private get; set; }
        [Inject] public IResourceRepository<CharacterClass> characterClassRepository { private get; set; }
        [Inject] public IResourceRepository<ContentPackage> contentPackageRepository { private get; set; }
        [Inject] public IResourceRepository<ContentPackageResource> contentPackageResourceRepository { private get; set; }
        [Inject] public IResourceRepository<ItemProperty> itemPropertyResource { private get; set; }
        [Inject] public IResourceRepository<ItemType> itemTypeRepository { private get; set; }
        [Inject] public IResourceRepository<LocalVariable> localVariableRepository { private get; set; }
        [Inject] public IResourceRepository<Race> raceRepository { private get; set; }
        [Inject] public IResourceRepository<Tile> tileRepository { private get; set; }
        
        public IRepository GetRepository()
        {
            throw new NotImplementedException();
        }

        public IGenericRepository<T> GetGenericRepository<T>()
        {
            if(typeof(T) == typeof(Area))
            {
                return (IGenericRepository<T>)areaRepository;
            }
            if (typeof(T) == typeof(Conversation))
            {
                return (IGenericRepository<T>)conversationRepository;
            }
            if (typeof(T) == typeof(Creature))
            {
                return (IGenericRepository<T>)creatureRepository;
            }
            if (typeof(T) == typeof(Item))
            {
                return (IGenericRepository<T>)itemRepository;
            }
            if (typeof(T) == typeof(Placeable))
            {
                return (IGenericRepository<T>)placeableRepository;
            }
            if (typeof(T) == typeof(Script))
            {
                return (IGenericRepository<T>)scriptRepository;
            }
            if (typeof(T) == typeof(Tileset))
            {
                return (IGenericRepository<T>)tilesetRepository;
            }
            if (typeof(T) == typeof(Ability))
            {
                return (IGenericRepository<T>)abilityRepository;
            }
            if (typeof(T) == typeof(Category))
            {
                return (IGenericRepository<T>)categoryRepository;
            }
            if (typeof(T) == typeof(CharacterClass))
            {
                return (IGenericRepository<T>)characterClassRepository;
            }
            if (typeof(T) == typeof(ContentPackage))
            {
                return (IGenericRepository<T>)contentPackageRepository;
            }
            if (typeof(T) == typeof(ContentPackageResource))
            {
                return (IGenericRepository<T>)contentPackageResourceRepository;
            }
            if (typeof(T) == typeof(ItemProperty))
            {
                return (IGenericRepository<T>)itemPropertyResource;
            }
            if (typeof(T) == typeof(ItemType))
            {
                return (IGenericRepository<T>)itemTypeRepository;
            }
            if (typeof(T) == typeof(LocalVariable))
            {
                return (IGenericRepository<T>)localVariableRepository;
            }
            if (typeof(T) == typeof(Race))
            {
                return (IGenericRepository<T>)raceRepository;
            }
            if (typeof(T) == typeof(Tile))
            {
                return (IGenericRepository<T>)tileRepository;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IGameObjectRepository<T> GetGameObjectRepository<T>() where T:GameObjectBase
        {
            if (typeof(T) == typeof(Area))
            {
                return (IGameObjectRepository<T>)areaRepository;
            }
            if (typeof(T) == typeof(Conversation))
            {
                return (IGameObjectRepository<T>)conversationRepository;
            }
            if (typeof(T) == typeof(Creature))
            {
                return (IGameObjectRepository<T>)creatureRepository;
            }
            if (typeof(T) == typeof(Item))
            {
                return (IGameObjectRepository<T>)itemRepository;
            }
            if (typeof(T) == typeof(Placeable))
            {
                return (IGameObjectRepository<T>)placeableRepository;
            }
            if (typeof(T) == typeof(Script))
            {
                return (IGameObjectRepository<T>)scriptRepository;
            }
            if (typeof(T) == typeof(Tileset))
            {
                return (IGameObjectRepository<T>)tilesetRepository;
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
                return (IResourceRepository<T>)abilityRepository;
            }
            if (typeof(T) == typeof(Category))
            {
                return (IResourceRepository<T>)categoryRepository;
            }
            if (typeof(T) == typeof(CharacterClass))
            {
                return (IResourceRepository<T>)characterClassRepository;
            }
            if (typeof(T) == typeof(ContentPackage))
            {
                return (IResourceRepository<T>)contentPackageRepository;
            }
            if (typeof(T) == typeof(ContentPackageResource))
            {
                return (IResourceRepository<T>)contentPackageResourceRepository;
            }
            if (typeof(T) == typeof(ItemProperty))
            {
                return (IResourceRepository<T>)itemPropertyResource;
            }
            if (typeof(T) == typeof(ItemType))
            {
                return (IResourceRepository<T>)itemTypeRepository;
            }
            if (typeof(T) == typeof(LocalVariable))
            {
                return (IResourceRepository<T>)localVariableRepository;
            }
            if (typeof(T) == typeof(Race))
            {
                return (IResourceRepository<T>)raceRepository;
            }
            if (typeof(T) == typeof(Tile))
            {
                return (IResourceRepository<T>)tileRepository;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
