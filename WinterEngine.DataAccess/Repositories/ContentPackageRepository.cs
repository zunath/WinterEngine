using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Ionic.Zip;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.UIObjects;


namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageRepository : RepositoryBase, IResourceRepository<ContentPackage>
    {
        #region Constructors

        public ContentPackageRepository(string connectionString = "", bool autoSaveChanges = true)
            : base(connectionString, autoSaveChanges)
        {
        }

        #endregion
        
        #region Database Methods

        /// <summary>
        /// Adds a content package to the database.
        /// </summary>
        /// <param name="package"></param>
        public ContentPackage Add(ContentPackage package)
        {
            return Context.ContentPackages.Add(package);
        }
            
        /// <summary>
        /// Adds a list of content packages to the database.
        /// </summary>
        /// <param name="packageList"></param>
        public void Add(List<ContentPackage> packageList)
        {
            Context.ContentPackages.AddRange(packageList);
        }

        /// <summary>
        /// Adds a content package to the database if it doesn't exist.
        /// If it already exists, the existing one will be updated with new values.
        /// </summary>
        /// <param name="package"></param>
        public void Upsert(ContentPackage package)
        {
            if (package.ResourceID <= 0)
            {
                Context.ContentPackages.Add(package);
            }
            else
            {
                Update(package);
            }
        }

        /// <summary>
        /// Updates an existing content package with new values
        /// </summary>
        /// <param name="package"></param>
        public void Update(ContentPackage package)
        {
            ContentPackage dbPackage = Context.ContentPackages.SingleOrDefault(x => x.ResourceID == package.ResourceID);
            if (dbPackage == null)
            {
                dbPackage = Context.ContentPackages.SingleOrDefault(x => x.FileName == package.FileName);
            }

            if (dbPackage == null) return;

            Context.Entry(dbPackage).CurrentValues.SetValues(package);
        }

        /// <summary>
        /// Deletes a content package from the database.
        /// </summary>
        /// <param name="package"></param>
        public void Delete(ContentPackage package)
        {
            for(int index = package.ResourceList.Count - 1; index >= 0; index--)
            {
                ContentPackageResource resource = package.ResourceList[index];
                Context.ContentPackageResources.Remove(resource);
            }

            Context.ContentPackages.Remove(package);
        }

        /// <summary>
        /// Returns all content packages from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAll()
        {
            return Context.ContentPackages.ToList();
        }

        public List<DropDownListUIObject> GetAllUIObjects()
        {
            List<DropDownListUIObject> items = (from contentPackage
                                                in Context.ContentPackages
                                                select new DropDownListUIObject
                                                {
                                                    Name = contentPackage.Name,
                                                    ResourceID = contentPackage.ResourceID
                                                }).ToList();
            return items;
        }

        /// <summary>
        /// Returns true if a content package exists in the database.
        /// Returns false if a content package does not exist in the database.
        /// The content package's FileName property is used to check.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public bool Exists(ContentPackage package)
        {
            ContentPackage dbPackage = Context.ContentPackages.SingleOrDefault(x => x.FileName == package.FileName);
            return !Object.ReferenceEquals(dbPackage, null);
        }

        /// <summary>
        /// Returns the content package matching the packageID passed in.
        /// Returns null if content package does not exist.
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public ContentPackage GetByID(int packageID)
        {
            return Context.ContentPackages.SingleOrDefault(x => x.ResourceID == packageID);
        }

        public int GetDefaultResourceID()
        {
            ContentPackage defaultObject = Context.ContentPackages.FirstOrDefault(x => x.IsDefault);
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        #endregion
    }
}
