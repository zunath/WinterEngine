using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class ItemPropertyRepository : IGenericRepository<ItemProperty>
    {
        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public ItemPropertyRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private ItemProperty InternalSave(ItemProperty itemProperty, bool saveChanges)
        {
            ItemProperty retItemProperty;
            if (itemProperty.ResourceID <= 0)
            {
                retItemProperty = _context.ItemProperties.Add(itemProperty);
            }
            else
            {
                retItemProperty = _context.ItemProperties.SingleOrDefault(x => x.ResourceID == itemProperty.ResourceID);
                if (retItemProperty == null) return null;
                _context.Entry(retItemProperty).CurrentValues.SetValues(itemProperty);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retItemProperty;
        }
        public ItemProperty Save(ItemProperty itemProperty)
        {
            return InternalSave(itemProperty, true);
        }

        public void Save(IEnumerable<ItemProperty> entityList)
        {
            if (entityList != null)
            {
                foreach (var itm in entityList)
                {
                    InternalSave(itm, false);
                }
                _context.SaveChanges();
            }
        }

        public void Update(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = _context.ItemProperties.Where(x => x.ResourceID == itemProperty.ResourceID).SingleOrDefault();
            if (dbItemProperty == null) return;

            _context.Entry(dbItemProperty).CurrentValues.SetValues(itemProperty);
        }

        private void DeleteInternal(ItemProperty itemProperty, bool saveChanges = true)
        {
            var dbItemProperty = _context.ItemProperties.SingleOrDefault(x => x.ResourceID == itemProperty.ResourceID);
            if (dbItemProperty == null) return;

            _context.ItemProperties.Remove(dbItemProperty);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(ItemProperty itemProperty)
        {
            DeleteInternal(itemProperty);
        }

        public void Delete(int resourceID)
        {
            var itemProperty = _context.ItemProperties.Find(resourceID);
            DeleteInternal(itemProperty);
        }

        public void Delete(IEnumerable<ItemProperty> itemPropertyList)
        {
            foreach (var itemProperty in itemPropertyList)
            {
                DeleteInternal(itemProperty, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<ItemProperty> GetAll()
        {
            return _context.ItemProperties.ToList();
        }

        public ItemProperty GetByID(int itemPropertyID)
        {
            return _context.ItemProperties.Where(x => x.ResourceID == itemPropertyID).SingleOrDefault();
        }


        #endregion
                
    }
}
