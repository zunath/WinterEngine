using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;


namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageResourceRepository : IGenericRepository<ContentPackageResource>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public ContentPackageResourceRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        public ContentPackageResource Add(ContentPackageResource resource)
        {
            return _context.ContentPackageResources.Add(resource);
        }

        public void Add(List<ContentPackageResource> resourceList)
        {
            _context.ContentPackageResources.AddRange(resourceList);
        }

        public ContentPackageResource Save(ContentPackageResource resource)
        {
            if (resource.ResourceID <= 0)
            {
                _context.ContentPackageResources.Add(resource);
            }
            else
            {
                Update(resource);
            }
            return resource;
        }

        public void Save(IEnumerable<ContentPackageResource> entityList)
        {
            throw new NotImplementedException();
        }

        public void Update(ContentPackageResource resource)
        {
            ContentPackageResource dbResource = _context.ContentPackageResources.Where(x => x.ResourceID == resource.ResourceID).SingleOrDefault();
            if (dbResource == null) return;

            _context.Entry(dbResource).CurrentValues.SetValues(resource);
        }

        public void Delete(ContentPackageResource resource)
        {
            _context.ContentPackageResources.Remove(resource);
        }

        public void Delete(int resourceID)
        {
            var contPackResource = _context.ContentPackageResources.Find(resourceID);
            Delete(contPackResource);
        }

        public void Delete(IEnumerable<ContentPackageResource> entityList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContentPackageResource> GetAll()
        {
            return _context.ContentPackageResources.ToList();
        }

        public ContentPackageResource GetByID(int resourceID)
        {
            return _context.ContentPackageResources.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
