using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class ItemPropertyRepository : RepositoryBase, IResourceRepository<ItemProperty>
    {
        
        #region Constructors

        public ItemPropertyRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public List<ItemProperty> GetAll()
        {
            return Context.ItemPropertyRepository.Get().ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects(bool includeDefault = false)
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.ItemPropertyRepository.Get()
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();
            if (includeDefault)
            {
                items.Insert(0, new DropDownListUIObject(0, "(None)"));
            }

            return items;
        }


        public ItemProperty Add(ItemProperty itemProperty)
        {
            return Context.ItemPropertyRepository.Add(itemProperty);
        }

        public void Add(List<ItemProperty> itemPropertyList)
        {
            Context.ItemPropertyRepository.AddList(itemPropertyList);
        }

        public void Update(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = Context.ItemPropertyRepository.Get(x => x.ResourceID == itemProperty.ResourceID).SingleOrDefault();
            if (dbItemProperty == null) return;

            Context.Context.Entry(dbItemProperty).CurrentValues.SetValues(itemProperty);
        }

        public void Upsert(ItemProperty itemProperty)
        {
            if (itemProperty.ResourceID <= 0)
            {
                Context.ItemPropertyRepository.Add(itemProperty);
            }
            else
            {
                Update(itemProperty);
            }
        }

        public ItemProperty GetByID(int itemPropertyID)
        {
            return Context.ItemPropertyRepository.Get(x => x.ResourceID == itemPropertyID).SingleOrDefault();
        }

        public bool Exists(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = Context.ItemPropertyRepository.Get(x => x.ResourceID == itemProperty.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbItemProperty, null);
        }

        public void Delete(ItemProperty itemProperty)
        {
            Context.ItemPropertyRepository.Delete(itemProperty);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

    }
}
