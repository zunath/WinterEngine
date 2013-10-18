using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.GameObjects;


namespace WinterEngine.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private Guid InstanceIdentifier;
        private ModuleDataContext context;
        public ModuleDataContext Context { get { return context; } }
        private bool disposed;

        public UnitOfWork()
        {
            context = new ModuleDataContext(WinterConnectionInformation.ActiveConnectionString);
            InstanceIdentifier = Guid.NewGuid();
            disposed = false;
        }

        public UnitOfWork(string connectionString)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }

            context = new ModuleDataContext(connectionString);
            InstanceIdentifier = Guid.NewGuid();
            disposed = false;
        }

        public UnitOfWork(ModuleDataContext context)
        {
            this.context = context;
            InstanceIdentifier = Guid.NewGuid();
            disposed = false;
        }

        #region Game object repositories

        private GenericRepository<ModuleDataContext, Area> areaRepository;
        public GenericRepository<ModuleDataContext, Area> AreaRepository
        {
            get
            {
                if (this.areaRepository == null)
                {
                    this.areaRepository
                        = new GenericRepository<ModuleDataContext, Area>(context);
                }
                return areaRepository;
            }
        }

        private GenericRepository<ModuleDataContext, Creature> creatureRepository;
        public GenericRepository<ModuleDataContext, Creature> CreatureRepository
        {
            get
            {
                if (this.creatureRepository == null)
                {
                    this.creatureRepository
                        = new GenericRepository<ModuleDataContext, Creature>(context);
                }
                return creatureRepository;
            }
        }

        private GenericRepository<ModuleDataContext, Item> itemRepository;
        public GenericRepository<ModuleDataContext, Item> ItemRepository
        {
            get
            {
                if (this.itemRepository == null)
                {
                    this.itemRepository
                        = new GenericRepository<ModuleDataContext, Item>(context);
                }
                return itemRepository;
            }
        }

        private GenericRepository<ModuleDataContext, Placeable> placeableRepository;
        public GenericRepository<ModuleDataContext, Placeable> PlaceableRepository
        {
            get
            {
                if (this.placeableRepository == null)
                {
                    this.placeableRepository
                        = new GenericRepository<ModuleDataContext, Placeable>(context);
                }
                return placeableRepository;
            }
        }



        #endregion

        #region Resource repositories

        private GenericRepository<ModuleDataContext, Category> categoryRepository;
        public GenericRepository<ModuleDataContext, Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository
                        = new GenericRepository<ModuleDataContext, Category>(context);
                }
                return categoryRepository;
            }
        }

        private GenericRepository<ModuleDataContext, ContentPackage> contentPackageRepository;
        public GenericRepository<ModuleDataContext, ContentPackage> ContentPackageRepository
        {
            get
            {
                if (this.contentPackageRepository == null)
                {
                    this.contentPackageRepository
                        = new GenericRepository<ModuleDataContext, ContentPackage>(context);
                }
                return contentPackageRepository;
            }
        }
        private GenericRepository<ModuleDataContext, ContentPackageResource> contentPackageResourceRepository;
        public GenericRepository<ModuleDataContext, ContentPackageResource> ContentPackageResourceRepository
        {
            get
            {
                if (this.contentPackageResourceRepository== null)
                {
                    this.contentPackageResourceRepository
                        = new GenericRepository<ModuleDataContext, ContentPackageResource>(context);
                }
                return contentPackageResourceRepository;
            }
        }
        
        private GenericRepository<ModuleDataContext, ItemProperty> itemPropertyRepository;
        public GenericRepository<ModuleDataContext, ItemProperty> ItemPropertyRepository
        {
            get
            {
                if (this.itemPropertyRepository == null)
                {
                    this.itemPropertyRepository
                        = new GenericRepository<ModuleDataContext, ItemProperty>(context);
                }
                return itemPropertyRepository;
            }
        }

        private GenericRepository<ModuleDataContext, ItemType> itemTypeRepository;
        public GenericRepository<ModuleDataContext, ItemType> ItemTypeRepository
        {
            get
            {
                if (this.itemTypeRepository == null)
                {
                    this.itemTypeRepository
                        = new GenericRepository<ModuleDataContext, ItemType>(context);
                }
                return itemTypeRepository;
            }
        }
        private GenericRepository<ModuleDataContext, Race> raceRepository;
        public GenericRepository<ModuleDataContext, Race> RaceRepository
        {
            get
            {
                if (this.raceRepository == null)
                {
                    this.raceRepository
                        = new GenericRepository<ModuleDataContext, Race>(context);
                }
                return raceRepository;
            }
        }
        private GenericRepository<ModuleDataContext, Conversation> conversationRepository;
        public GenericRepository<ModuleDataContext, Conversation> ConversationRepository
        {
            get
            {
                if (this.conversationRepository == null)
                {
                    this.conversationRepository
                        = new GenericRepository<ModuleDataContext, Conversation>(context);
                }
                return conversationRepository;
            }
        }
        private GenericRepository<ModuleDataContext, Script> scriptRepository;
        public GenericRepository<ModuleDataContext, Script> ScriptRepository
        {
            get
            {
                if (this.scriptRepository == null)
                {
                    this.scriptRepository
                        = new GenericRepository<ModuleDataContext, Script>(context);
                }
                return scriptRepository;
            }
        }

        private GenericRepository<ModuleDataContext, LocalVariable> localVariableRepository;
        public GenericRepository<ModuleDataContext, LocalVariable> LocalVariableRepository
        {
            get
            {
                if (this.localVariableRepository == null)
                {
                    this.localVariableRepository
                        = new GenericRepository<ModuleDataContext, LocalVariable>(context);
                }
                return localVariableRepository;
            }
        }

        #endregion

        #region Mapping repositories
        
        private GenericRepository<ModuleDataContext, Tile> tileRepository;
        public GenericRepository<ModuleDataContext, Tile> TileRepository
        {
            get
            {
                if (this.tileRepository == null)
                {
                    this.tileRepository
                        = new GenericRepository<ModuleDataContext, Tile>(context);
                }
                return tileRepository;
            }
        }

        private GenericRepository<ModuleDataContext, Tileset> tilesetRepository;
        public GenericRepository<ModuleDataContext, Tileset> TilesetRepository
        {
            get
            {
                if (this.tilesetRepository == null)
                {
                    this.tilesetRepository
                        = new GenericRepository<ModuleDataContext, Tileset>(context);
                }
                return tilesetRepository;
            }
        }

        #endregion

        private Object getTargetRepository(Object item)
        {
            // Type of the object that is sent to the function
            Type itemType = item.GetType();

            // Gets the class name of the object sent to the function
            string className = itemType.Namespace == "WinterEngine.DataTransferObjects" ? itemType.Name : itemType.BaseType.Name;

            PropertyInfo targetRepositoryProperty =
                // WinterContext
                                            this
                // Will return the type "UnitOfWork"
                                                .GetType()
                /* Looks for a property of UnitOfWork that matches 
                 * the class name of the object that was sent to 
                 * this function */
                                                    .GetProperty(className + "Repository");
            return targetRepositoryProperty.GetValue(this, null);
        }

        public void Attach(Object item)
        {
            Type itemType = item.GetType();
            string className = itemType.Name;
            var targetDatasetField = context.GetType().GetProperty(className + "s");
            dynamic targetDataset = targetDatasetField.GetValue(context, null);

            targetDataset.Attach((dynamic)item);
        }

        public void Add(Object item)
        {
            dynamic targetRepository = getTargetRepository(item);
            targetRepository.Add((dynamic)item);
        }

        public void AddList(List<Object> itemList)
        {
            foreach(Object currentItem in itemList)
            {
                dynamic targetRepository = getTargetRepository(currentItem);
                targetRepository.Add((dynamic)currentItem);
            }
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> filter = null,
                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                        string includeProperties = "", Boolean readOnly = false) where T : class, new()
        {
            Object item = new T();
            dynamic targetRepository = getTargetRepository(item);
            return targetRepository.Get(filter, orderBy, includeProperties, readOnly);
        }



        public void Delete(Object item)
        {
            dynamic targetRepository = getTargetRepository(item);
            targetRepository.Delete((dynamic)item);
        }

        public void DeleteAll<TEntity>(List<TEntity> items) where TEntity : class
        {
            if (items.Count > 0)
            {
                Object item = items[0];
                dynamic targetRepository = getTargetRepository(item);

                items.ForEach(delegate(TEntity entity)
                {
                    targetRepository.Delete((dynamic)entity);
                });
            }
        }

        public void Update(Object item)
        {
            dynamic targetRepository = getTargetRepository(item);

            targetRepository.Update((dynamic)item);
        }

        public void UpdateAll<TEntity>(List<TEntity> items) where TEntity : class
        {
            if (items.Count > 0)
            {
                Object item = items[0];
                dynamic targetRepository = getTargetRepository(item);

                items.ForEach(delegate(TEntity entity)
                {
                    targetRepository.Update((dynamic)entity);
                });
            }
        }


        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                throw e;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
