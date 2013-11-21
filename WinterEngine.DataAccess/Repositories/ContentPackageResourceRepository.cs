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
            return Context.ContentPackageResources.Add(resource);
        }

        public void Add(List<ContentPackageResource> resourceList)
        {
            Context.ContentPackageResources.AddRange(resourceList);
        }

        public void Update(ContentPackageResource resource)
        {
            ContentPackageResource dbResource = Context.ContentPackageResources.SingleOrDefault(x => x.ResourceID == resource.ResourceID);
            if (dbResource == null) return;

            Context.Entry(dbResource).CurrentValues.SetValues(resource);
        }

        public void Upsert(ContentPackageResource resource)
        {
            if (resource.ResourceID <= 0)
            {
                Context.ContentPackageResources.Add(resource);
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
            Context.ContentPackageResources.Remove(resource);
        }

        public List<ContentPackageResource> GetAll()
        {
            return Context.ContentPackageResources.Include("ContentPackage").ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from contentPackageResource
                                                in Context.ContentPackageResources
                                                select new DropDownListUIObject
                                                {
                                                    Name = contentPackageResource.Name,
                                                    ResourceID = contentPackageResource.ResourceID
                                                }).ToList();

            return items;
        }

        public List<DropDownListUIObject> GetAllUIObjects(ContentPackageResourceTypeEnum resourceType, bool includeDefault = false)
        {
            List<DropDownListUIObject> items = (from category
                                               in Context.ContentPackageResources
                                                where category.ContentPackageResourceType == resourceType
                                                select new DropDownListUIObject
                                                {
                                                    Name = category.Name,
                                                    ResourceID = category.ResourceID
                                                }).ToList();

            if (includeDefault)
            {
                items.Insert(0, new DropDownListUIObject(0, "(None)"));
            }

            return items;
        }

        public bool Exists(ContentPackageResource resource)
        {
            List<ContentPackageResource> resources = Context.ContentPackageResources.Where(x => x.ResourceID == resource.ResourceID).ToList();
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
            return Context.ContentPackageResources.SingleOrDefault(x => x.ResourceID == resourceID);
        }

        public int GetDefaultResourceID()
        {
            ContentPackageResource defaultObject = Context.ContentPackageResources.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        #endregion
    }
}
