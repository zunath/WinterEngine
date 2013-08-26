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


namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageRepository : RepositoryBase, IResourceRepository<ContentPackage>
    {
        #region Constants

        private const string ManifestFileName = "Manifest.xml";

        #endregion

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
            return Context.ContentPackageRepository.Add(package);
        }
            
        /// <summary>
        /// Adds a list of content packages to the database.
        /// </summary>
        /// <param name="packageList"></param>
        public void Add(List<ContentPackage> packageList)
        {
            Context.ContentPackageRepository.AddList(packageList);
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
                Context.ContentPackageRepository.Add(package);
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
            ContentPackage dbPackage = Context.ContentPackageRepository.Get(x => x.ResourceID == package.ResourceID).SingleOrDefault();
            if (dbPackage == null)
            {
                dbPackage = Context.ContentPackageRepository.Get(x => x.FileName == package.FileName).SingleOrDefault();
            }

            if (dbPackage == null) return;

            dbPackage.Comment = package.Comment;
            dbPackage.ContentPackagePath = package.ContentPackagePath;
            dbPackage.Description = package.Description;
            dbPackage.FileName = package.FileName;
            dbPackage.IsSystemResource = package.IsSystemResource;
            dbPackage.Name = package.Name;
            dbPackage.ResourceTypeID = package.ResourceTypeID;
            dbPackage.ResourceList = package.ResourceList;   
        }

        /// <summary>
        /// Deletes a content package from the database.
        /// </summary>
        /// <param name="package"></param>
        public void Delete(ContentPackage package)
        {
            Context.ContentPackageRepository.Delete(package);
        }

        /// <summary>
        /// Returns all content packages from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAll()
        {
            return Context.ContentPackageRepository.Get().ToList();
        }


        /// <summary>
        /// Returns the file names of every content package used by the module.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFileNames()
        {
            List<ContentPackage> packages = Context.ContentPackageRepository.Get().ToList();
            return packages.Select(x => x.FileName).ToList();
        }

        /// <summary>
        /// Returns all content packages which are not system resources from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAllNonSystemResource()
        {
            return Context.ContentPackageRepository.Get(x => x.IsSystemResource == false).ToList();
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
            ContentPackage dbPackage = Context.ContentPackageRepository.Get(x => x.FileName == package.FileName).SingleOrDefault();
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
            return Context.ContentPackageRepository.Get(x => x.ResourceID == packageID).SingleOrDefault();
        }

        #endregion
    }
}
