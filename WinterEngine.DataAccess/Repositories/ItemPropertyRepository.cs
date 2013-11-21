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
            return Context.ItemProperties.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from item
                                                in Context.ItemProperties
                                                select new DropDownListUIObject
                                                {
                                                    Name = item.Name,
                                                    ResourceID = item.ResourceID
                                                }).ToList();
            return items;
        }


        public ItemProperty Add(ItemProperty itemProperty)
        {
            return Context.ItemProperties.Add(itemProperty);
        }

        public void Add(List<ItemProperty> itemPropertyList)
        {
            Context.ItemProperties.AddRange(itemPropertyList);
        }

        public void Update(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = Context.ItemProperties.SingleOrDefault(x => x.ResourceID == itemProperty.ResourceID);
            if (dbItemProperty == null) return;

            Context.Entry(dbItemProperty).CurrentValues.SetValues(itemProperty);
        }

        public void Upsert(ItemProperty itemProperty)
        {
            if (itemProperty.ResourceID <= 0)
            {
                Context.ItemProperties.Add(itemProperty);
            }
            else
            {
                Update(itemProperty);
            }
        }

        public ItemProperty GetByID(int itemPropertyID)
        {
            return Context.ItemProperties.SingleOrDefault(x => x.ResourceID == itemPropertyID);
        }

        public bool Exists(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = Context.ItemProperties.SingleOrDefault(x => x.ResourceID == itemProperty.ResourceID);
            return !Object.ReferenceEquals(dbItemProperty, null);
        }

        public void Delete(ItemProperty itemProperty)
        {
            Context.ItemProperties.Remove(itemProperty);
        }

        public int GetDefaultResourceID()
        {
            ItemProperty defaultObject = Context.ItemProperties.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

    }
}
