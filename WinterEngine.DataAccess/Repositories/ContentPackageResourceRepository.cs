using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;


namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageResourceRepository : RepositoryBase, IResourceRepository<ContentPackageResource>
    {
        #region Constructors

        public ContentPackageResourceRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public ContentPackageResource Add(ContentPackageResource resource)
        {
            return Context.ContentPackageResourceRepository.Add(resource);
        }

        public void Add(List<ContentPackageResource> resourceList)
        {
            Context.ContentPackageResourceRepository.AddList(resourceList);
        }

        public void Update(ContentPackageResource resource)
        {
            ContentPackageResource dbResource = Context.ContentPackageResourceRepository.Get(x => x.ResourceID == resource.ResourceID).SingleOrDefault();
            if (dbResource == null) return;

            Context.Context.Entry(dbResource).CurrentValues.SetValues(resource);
        }

        public void Upsert(ContentPackageResource resource)
        {
            if (resource.ResourceID <= 0)
            {
                Context.ContentPackageResourceRepository.Add(resource);
            }
            else
            {
                Update(resource);
            }
        }

        public void Upsert(List<ContentPackageResource> resourceList)
        {
            foreach (ContentPackageResource resource in resourceList)
            {
                Upsert(resource);
            }
        }

        public void Delete(ContentPackageResource resource)
        {
            Context.ContentPackageResourceRepository.Delete(resource);
        }

        public List<ContentPackageResource> GetAll()
        {
            return Context.ContentPackageResourceRepository.Get(null, null, "ContentPackage").ToList();
        }

        public List<ContentPackageResource> GetAllByResourceType(ContentPackageResourceTypeEnum resourceType)
        {
            return Context.ContentPackageResourceRepository.Get(x => x.ContentPackageResourceTypeID == (int)resourceType).ToList();
        }

        public bool Exists(ContentPackageResource resource)
        {
            List<ContentPackageResource> resources = Context.ContentPackageResourceRepository.Get(x => x.ResourceID == resource.ResourceID).ToList();
            if (resources.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ContentPackageResource GetByID(int resourceID)
        {
            return Context.ContentPackageResourceRepository.Get(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        #endregion
    }
}
