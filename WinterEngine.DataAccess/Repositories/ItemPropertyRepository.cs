using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public class ItemPropertyRepository : IResourceRepository<ItemProperty>
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

        public List<ItemProperty> GetAll()
        {
            return _context.ItemProperties.ToList();
        }

        //todo: Move logic somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from item
        //                                        in Context.ItemPropertyRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = item.Name,
        //                                            ResourceID = item.ResourceID
        //                                        }).ToList();
        //    return items;
        //}


        public ItemProperty Add(ItemProperty itemProperty)
        {
            return _context.ItemProperties.Add(itemProperty);
        }

        public void Add(List<ItemProperty> itemPropertyList)
        {
            _context.ItemProperties.AddRange(itemPropertyList);
        }

        public void Update(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = _context.ItemProperties.Where(x => x.ResourceID == itemProperty.ResourceID).SingleOrDefault();
            if (dbItemProperty == null) return;

            _context.Entry(dbItemProperty).CurrentValues.SetValues(itemProperty);
        }

        public void Upsert(ItemProperty itemProperty)
        {
            if (itemProperty.ResourceID <= 0)
            {
                _context.ItemProperties.Add(itemProperty);
            }
            else
            {
                Update(itemProperty);
            }
        }

        public ItemProperty GetByID(int itemPropertyID)
        {
            return _context.ItemProperties.Where(x => x.ResourceID == itemPropertyID).SingleOrDefault();
        }

        public bool Exists(ItemProperty itemProperty)
        {
            ItemProperty dbItemProperty = _context.ItemProperties.Where(x => x.ResourceID == itemProperty.ResourceID).SingleOrDefault();
            return !Object.ReferenceEquals(dbItemProperty, null);
        }

        public void Delete(ItemProperty itemProperty)
        {
            _context.ItemProperties.Remove(itemProperty);
        }

        public int GetDefaultResourceID()
        {
            ItemProperty defaultObject = _context.ItemProperties.Where(x => x.IsDefault).FirstOrDefault();
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        #endregion


        public object Load(int resourceID)
        {
            throw new NotImplementedException();
        }

        public int Save(object gameObject)
        {
            throw new NotImplementedException();
        }

        public void DeleteByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int resourceID)
        {
            throw new NotImplementedException();
        }
    }
}
