using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageRepository : RepositoryBase, IResourceRepository<ContentPackage>
    {
        #region Constructors

        public ContentPackageRepository(string connectionString = "")
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WinterConnectionInformation.ActiveConnectionString;
            }
            ConnectionString = connectionString;
            
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Adds a content package to the database.
        /// </summary>
        /// <param name="package"></param>
        public void Add(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.ContentPackages.Add(package);
                context.SaveChanges();
            }
        }
            
        /// <summary>
        /// Adds a list of content packages to the database.
        /// </summary>
        /// <param name="packageList"></param>
        public void Add(List<ContentPackage> packageList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (ContentPackage contentPackage in packageList)
                {
                    context.ContentPackages.Add(contentPackage);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a content package to the database if it doesn't exist.
        /// If it already exists, the existing one will be updated with new values.
        /// </summary>
        /// <param name="package"></param>
        public void Upsert(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ContentPackage dbResource = context.ContentPackages.SingleOrDefault(r => r.ResourceID.Equals(package.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.ContentPackages.Remove(dbResource);
                    context.ContentPackages.Add(package);
                    context.SaveChanges();
                }
                else
                {
                    context.ContentPackages.Add(package);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Updates an existing content package with new values
        /// </summary>
        /// <param name="package"></param>
        public void Update(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the resource in the database that matches the passed-in item type's ID (primary key)
                ContentPackage dbResource = context.ContentPackages.SingleOrDefault(r => r.ResourceID.Equals(package.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    context.ContentPackages.Remove(dbResource);
                    context.ContentPackages.Add(package);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes a content package from the database.
        /// </summary>
        /// <param name="package"></param>
        public void Delete(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ContentPackage dbPackage = context.ContentPackages.SingleOrDefault(val => val.ResourceID == package.ResourceID);

                if (!Object.ReferenceEquals(dbPackage, null))
                {
                    context.ContentPackages.Remove(dbPackage);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Returns all content packages from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAll()
        {
            List<ContentPackage> contentPackageList = new List<ContentPackage>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from contentPackage
                            in context.ContentPackages
                            select contentPackage;
                contentPackageList = query.ToList();
            }

            return contentPackageList;
        }

        /// <summary>
        /// Returns all content packages which are not system resources from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAllNonSystemResource()
        {
            List<ContentPackage> contentPackageList = new List<ContentPackage>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from contentPackage
                            in context.ContentPackages
                            where contentPackage.IsSystemResource == false
                            select contentPackage;
                contentPackageList = query.ToList();
            }

            return contentPackageList;
        }

        /// <summary>
        /// Returns true if a content package exists in the database.
        /// Returns false if a content package does not exist in the database.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public bool Exists(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ContentPackage dbPackage = context.ContentPackages.FirstOrDefault(c => c.ResourceID.Equals(package.ResourceID));
                return !Object.ReferenceEquals(dbPackage, null);
            }
        }

        /// <summary>
        /// Returns the content package matching the packageID passed in.
        /// Returns null if content package does not exist.
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public ContentPackage GetByID(int packageID)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.ContentPackages.FirstOrDefault(x => x.ResourceID == packageID);
            }
        }

        /// <summary>
        /// Deletes all entries for existing content packages and inserts the specified list of content packages to the database.
        /// </summary>
        /// <param name="contentPackages"></param>
        public void ReplaceAll(List<ContentPackage> contentPackages)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Remove existing packages
                var query = from package
                            in context.ContentPackages
                            select package;
                List<ContentPackage> removedContentPackages = query.ToList();

                foreach (ContentPackage package in removedContentPackages)
                {
                    context.ContentPackages.Remove(package);
                }

                // Add the new set of packages
                foreach (ContentPackage package in contentPackages)
                {
                    context.ContentPackages.Add(package);
                }

                context.SaveChanges();
            }
        }

        public void Dispose()
        {
        }

        #endregion

    }
}
