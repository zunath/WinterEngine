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
    public class ContentPackageRepository : IGenericRepository<ContentPackage>
    {
        #region Constants

        //private const string ManifestFileName = "Manifest.xml";

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #endregion

        #region Constructors

        public ContentPackageRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion
        
        #region Database Methods

        /// <summary>
        /// Adds a content package to the database.
        /// </summary>
        /// <param name="package"></param>
        public ContentPackage Add(ContentPackage package)
        {
            return _context.ContentPackages.Add(package);
        }
            
        /// <summary>
        /// Adds a list of content packages to the database.
        /// </summary>
        /// <param name="packageList"></param>
        public void Add(List<ContentPackage> packageList)
        {
            _context.ContentPackages.AddRange(packageList);
        }

        /// <summary>
        /// Adds a content package to the database if it doesn't exist.
        /// If it already exists, the existing one will be updated with new values.
        /// </summary>
        /// <param name="package"></param>
        public ContentPackage Save(ContentPackage package)
        {
            if (package.ResourceID <= 0)
            {
                _context.ContentPackages.Add(package);
            }
            else
            {
                Update(package);
            }

            return package;
        }

        public void Save(IEnumerable<ContentPackage> entityList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing content package with new values
        /// </summary>
        /// <param name="package"></param>
        public void Update(ContentPackage package)
        {
            ContentPackage dbPackage = _context.ContentPackages.Where(x => x.ResourceID == package.ResourceID).SingleOrDefault();
            if (dbPackage == null)
            {
                dbPackage = _context.ContentPackages.Where(x => x.FileName == package.FileName).SingleOrDefault();
            }

            if (dbPackage == null) return;

            _context.Entry(dbPackage).CurrentValues.SetValues(package);
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
                _context.ContentPackageResources.Remove(resource);
            }

            _context.ContentPackages.Remove(package);
        }

        public void Delete(int resourceID)
        {
            var contPackage = _context.ContentPackages.Find(resourceID);
            Delete(contPackage);
        }

        public void Delete(IEnumerable<ContentPackage> entityList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContentPackage> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the content package matching the packageID passed in.
        /// Returns null if content package does not exist.
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public ContentPackage GetByID(int packageID)
        {
            return _context.ContentPackages.Where(x => x.ResourceID == packageID).SingleOrDefault();
        }


        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        #endregion

    }
}
