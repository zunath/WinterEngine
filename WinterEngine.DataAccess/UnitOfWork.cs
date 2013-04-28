using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;


namespace WinterEngine.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private Guid InstanceIdentifier;
        private WinterContext context;
        public WinterContext Context { get { return context; } }
        private bool disposed;

        public UnitOfWork()
        {
            context = new WinterContext(WinterConnectionInformation.ActiveConnectionString);
            InstanceIdentifier = Guid.NewGuid();
            disposed = false;
        }

        public UnitOfWork(string connectionString)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }

            context = new WinterContext(connectionString);
            InstanceIdentifier = Guid.NewGuid();
            disposed = false;
        }

        public UnitOfWork(WinterContext context)
        {
            this.context = context;
            InstanceIdentifier = Guid.NewGuid();
            disposed = false;
        }

        #region Game object repositories

        private GenericRepository<WinterContext, Area> areaRepository;
        public GenericRepository<WinterContext, Area> AreaRepository
        {
            get
            {
                if (this.areaRepository == null)
                {
                    this.areaRepository
                        = new GenericRepository<WinterContext, Area>(context);
                }
                return areaRepository;
            }
        }

        private GenericRepository<WinterContext, Creature> creatureRepository;
        public GenericRepository<WinterContext, Creature> CreatureRepository
        {
            get
            {
                if (this.creatureRepository == null)
                {
                    this.creatureRepository
                        = new GenericRepository<WinterContext, Creature>(context);
                }
                return creatureRepository;
            }
        }

        private GenericRepository<WinterContext, Item> itemRepository;
        public GenericRepository<WinterContext, Item> ItemRepository
        {
            get
            {
                if (this.itemRepository == null)
                {
                    this.itemRepository
                        = new GenericRepository<WinterContext, Item>(context);
                }
                return itemRepository;
            }
        }

        private GenericRepository<WinterContext, Placeable> placeableRepository;
        public GenericRepository<WinterContext, Placeable> PlaceableRepository
        {
            get
            {
                if (this.placeableRepository == null)
                {
                    this.placeableRepository
                        = new GenericRepository<WinterContext, Placeable>(context);
                }
                return placeableRepository;
            }
        }



        #endregion

        #region Resource repositories

        private GenericRepository<WinterContext, Category> categoryRepository;
        public GenericRepository<WinterContext, Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository
                        = new GenericRepository<WinterContext, Category>(context);
                }
                return categoryRepository;
            }
        }

        private GenericRepository<WinterContext, ContentPackage> contentPackageRepository;
        public GenericRepository<WinterContext, ContentPackage> ContentPackageRepository
        {
            get
            {
                if (this.contentPackageRepository == null)
                {
                    this.contentPackageRepository
                        = new GenericRepository<WinterContext, ContentPackage>(context);
                }
                return contentPackageRepository;
            }
        }
        private GenericRepository<WinterContext, ContentPackageResource> contentPackageResourceRepository;
        public GenericRepository<WinterContext, ContentPackageResource> ContentPackageResourceRepository
        {
            get
            {
                if (this.contentPackageResourceRepository== null)
                {
                    this.contentPackageResourceRepository
                        = new GenericRepository<WinterContext, ContentPackageResource>(context);
                }
                return contentPackageResourceRepository;
            }
        }
        
        private GenericRepository<WinterContext, ItemProperty> itemPropertyRepository;
        public GenericRepository<WinterContext, ItemProperty> ItemPropertyRepository
        {
            get
            {
                if (this.itemPropertyRepository == null)
                {
                    this.itemPropertyRepository
                        = new GenericRepository<WinterContext, ItemProperty>(context);
                }
                return itemPropertyRepository;
            }
        }

        private GenericRepository<WinterContext, ItemType> itemTypeRepository;
        public GenericRepository<WinterContext, ItemType> ItemTypeRepository
        {
            get
            {
                if (this.itemTypeRepository == null)
                {
                    this.itemTypeRepository
                        = new GenericRepository<WinterContext, ItemType>(context);
                }
                return itemTypeRepository;
            }
        }
        private GenericRepository<WinterContext, Race> raceRepository;
        public GenericRepository<WinterContext, Race> RaceRepository
        {
            get
            {
                if (this.raceRepository == null)
                {
                    this.raceRepository
                        = new GenericRepository<WinterContext, Race>(context);
                }
                return raceRepository;
            }
        }
        #endregion

        #region Mapping repositories
        
        private GenericRepository<WinterContext, Tile> tileRepository;
        public GenericRepository<WinterContext, Tile> TileRepository
        {
            get
            {
                if (this.tileRepository == null)
                {
                    this.tileRepository
                        = new GenericRepository<WinterContext, Tile>(context);
                }
                return tileRepository;
            }
        }
        
        
        private GenericRepository<WinterContext, Map> mapRepository;
        public GenericRepository<WinterContext, Map> MapRepository
        {
            get
            {
                if (this.mapRepository == null)
                {
                    this.mapRepository
                        = new GenericRepository<WinterContext, Map>(context);
                }
                return mapRepository;
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
