using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Ionic.Zip;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageResourceRepository : RepositoryBase, IResourceRepository<ContentPackageResource>
    {
        #region Constructors

        public ContentPackageResourceRepository(string connectionString = "")
            : base(connectionString)
        {
        }

        #endregion

        #region Methods

        public void Add(ContentPackageResource resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.ContentPackageResources.Add(resource);
                context.SaveChanges();
            }
        }

        public void Add(List<ContentPackageResource> resourceList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (ContentPackageResource resource in resourceList)
                {
                    context.ContentPackageResources.Add(resource);
                }

                context.SaveChanges();
            }
        }

        public void Update(ContentPackageResource resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the resource in the database that matches the passed-in item type's ID (primary key)
                ContentPackageResource dbResource = context.ContentPackageResources.SingleOrDefault(r => r.ResourceID.Equals(resource.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    dbResource = Mapper.Map(resource, dbResource);
                    context.SaveChanges();
                }
            }
        }

        public void Upsert(ContentPackageResource resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ContentPackageResource dbResource = context.ContentPackageResources.SingleOrDefault(r => r.ResourceID.Equals(resource.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    dbResource = Mapper.Map(resource, dbResource);
                }
                else
                {
                    context.ContentPackageResources.Add(resource);
                }

                context.SaveChanges();
            }
        }

        public void Upsert(List<ContentPackageResource> resourceList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (ContentPackageResource resource in resourceList)
                {
                    ContentPackageResource dbResource = context.ContentPackageResources.SingleOrDefault(r => r.ResourceID.Equals(resource.ResourceID));

                    if (!Object.ReferenceEquals(dbResource, null))
                    {
                        dbResource = Mapper.Map(resource, dbResource);
                    }
                    else
                    {
                        context.ContentPackageResources.Add(resource);
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(ContentPackageResource resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ContentPackageResource dbPackage = context.ContentPackageResources.SingleOrDefault(val => val.ResourceID == resource.ResourceID);

                if (!Object.ReferenceEquals(dbPackage, null))
                {
                    context.ContentPackageResources.Remove(dbPackage);
                    context.SaveChanges();
                }
            }
        }

        public List<ContentPackageResource> GetAll()
        {
            List<ContentPackageResource> resourceList = new List<ContentPackageResource>();
            
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from resource
                            in context.ContentPackageResources
                            select resource;
                resourceList = query.ToList();
            }

            return resourceList;
        }

        public bool Exists(ContentPackageResource resource)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from dbResource
                            in context.ContentPackageResources
                            where resource.ResourceID == dbResource.ResourceID
                            select dbResource;

                List<ContentPackageResource> dbPackageList = query.ToList();

                if (dbPackageList.Count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public ContentPackageResource GetByID(int resourceID)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.ContentPackageResources.FirstOrDefault(x => x.ResourceID == resourceID);
            }
        }

        /// <summary>
        /// Adds a list of content package resources to the database. Duplicate entries (based on FileName) are ignored.
        /// </summary>
        /// <param name="resourceList"></param>
        public void AddIgnoreExisting(List<ContentPackageResource> resourceList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (ContentPackageResource resource in resourceList)
                {
                    ContentPackageResource existingResource = context.ContentPackageResources.FirstOrDefault(x => x.FileName == resource.FileName);

                    if (Object.ReferenceEquals(existingResource, null))
                    {
                        context.ContentPackageResources.Add(resource);
                    }
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes all resources in the specified ContentPackage which are not in the specified resource list
        /// </summary>
        /// <param name="package"></param>
        /// <param name="resourceList"></param>
        public void RemoveMissingResources(ContentPackage package, List<ContentPackageResource> resourceList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                List<ContentPackageResource> existingResources = (from resource
                                                                  in context.ContentPackageResources
                                                                  where package.ResourceID == resource.ContentPackageID
                                                                  select resource).ToList();
                List<ContentPackageResource> removedResources = existingResources.Except(resourceList).ToList();

                foreach (ContentPackageResource resource in removedResources)
                {
                    context.ContentPackageResources.Remove(resource);
                }

                context.SaveChanges();
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
