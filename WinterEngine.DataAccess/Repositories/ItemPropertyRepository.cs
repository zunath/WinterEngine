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

        public ItemProperty Add(ItemProperty itemProperty)
        {
            return _context.ItemProperties.Add(itemProperty);
        }

        public void Add(List<ItemProperty> itemPropertyList)
        {
            _context.ItemProperties.AddRange(itemPropertyList);
        }

        public ItemProperty Save(ItemProperty itemProperty)
        {
            if (itemProperty.ResourceID <= 0)
            {
                _context.ItemProperties.Add(itemProperty);
            }
            else
            {
                Update(itemProperty);
            }
            return itemProperty;
        }

        public void Save(IEnumerable<ItemProperty> entityList)
        {
            throw new NotImplementedException();
        }

        public void Update(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = _context.ItemProperties.Where(x => x.ResourceID == itemProperty.ResourceID).SingleOrDefault();
            if (dbItemProperty == null) return;

            _context.Entry(dbItemProperty).CurrentValues.SetValues(itemProperty);
        }

        public void Delete(ItemProperty itemProperty)
        {
            _context.ItemProperties.Remove(itemProperty);
        }

        public void Delete(int resourceID)
        {
            var itemProperty = _context.ItemProperties.Find(resourceID);
            Delete(itemProperty);
        }

        public void Delete(IEnumerable<ItemProperty> entityList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemProperty> GetAll()
        {
            return _context.ItemProperties.ToList();
        }

        public ItemProperty GetByID(int itemPropertyID)
        {
            return _context.ItemProperties.Where(x => x.ResourceID == itemPropertyID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        #endregion
                
    }
}
