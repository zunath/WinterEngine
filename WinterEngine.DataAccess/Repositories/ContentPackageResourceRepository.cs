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

        public void Add(ContentPackageResource resource)
        {
            Context.ContentPackageResourceRepository.Add(resource);
        }

        public void Add(List<ContentPackageResource> resourceList)
        {
            Context.ContentPackageResourceRepository.AddList(resourceList);
        }

        public void Update(ContentPackageResource resource)
        {
            Context.ContentPackageResourceRepository.Update(resource);
        }

        public void Upsert(ContentPackageResource resource)
        {
            if (resource.ResourceID <= 0)
            {
                Context.ContentPackageResourceRepository.Add(resource);
            }
            else
            {
                Context.ContentPackageResourceRepository.Update(resource);
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
            return Context.ContentPackageResourceRepository.Get(null, null, "Package").ToList();
        }

        public List<ContentPackageResource> GetAllByResourceType(ContentPackageResourceTypeEnum resourceType)
        {
            return Context.ContentPackageResourceRepository.Get(x => x.ContentPackageResourceTypeID == (int)resourceType, null, "Package").ToList();
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
            return Context.ContentPackageResourceRepository.Get(x => x.ResourceID == resourceID, null, "Package").SingleOrDefault();
        }

        /// <summary>
        /// Adds a list of content package resources to the database. Duplicate entries (based on FileName) are ignored.
        /// </summary>
        /// <param name="resourceList"></param>
        public void AddIgnoreExisting(List<ContentPackageResource> resourceList)
        {
            foreach (ContentPackageResource resource in resourceList)
            {
                ContentPackageResource existingResource = Context.ContentPackageResourceRepository.Get(x => x.FileName == resource.FileName).SingleOrDefault();

                if (Object.ReferenceEquals(existingResource, null))
                {
                    Context.ContentPackageResourceRepository.Add(resource);
                }
            }
        }

        /// <summary>
        /// Removes all resources in the specified ContentPackage which are not in the specified resource list
        /// </summary>
        /// <param name="package"></param>
        /// <param name="resourceList"></param>
        public void RemoveMissingResources(ContentPackage package, List<ContentPackageResource> resourceList)
        {
            List<ContentPackageResource> existingResources = Context.ContentPackageResourceRepository.Get(x => x.ContentPackageID == package.ResourceID).ToList();
            List<ContentPackageResource> removedResources = existingResources.Except(resourceList).ToList();

            foreach (ContentPackageResource resource in removedResources)
            {
                Context.ContentPackageResourceRepository.Delete(resource);
            }
        }


        /// <summary>
        /// Extracts a content builder resource from a content package to memory and returns the MemoryStream object.
        /// </summary>
        /// <param name="package"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public MemoryStream ExtractResourceToMemory(ContentPackageResource resource, ContentPackage package = null)
        {
            string path = "";

            if (!Object.ReferenceEquals(package, null))
            {
                path = package.ContentPackagePath;
            }
            else
            {
                path = resource.Package.ContentPackagePath;
            }

            MemoryStream stream = new MemoryStream();
            using (ZipFile zipFile = new ZipFile(path))
            {
                zipFile[resource.FileName].Extract(stream);
            }

            return stream;
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
