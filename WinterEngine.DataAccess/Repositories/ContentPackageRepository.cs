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

        private ContentPackage SaveInternal(ContentPackage package, bool saveChanges)
        {
            ContentPackage retContentPackage;
            if (package.ResourceID <= 0)
            {
                retContentPackage = _context.ContentPackages.Add(package);
            }
            else
            {
                retContentPackage = _context.ContentPackages.SingleOrDefault(x => x.ResourceID == package.ResourceID);
                if (retContentPackage == null) return null;
                _context.Entry(retContentPackage).CurrentValues.SetValues(package);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retContentPackage;
        }

        /// <summary>
        /// Adds a content package to the database if it doesn't exist.
        /// If it already exists, the existing one will be updated with new values.
        /// </summary>
        /// <param name="package"></param>
        public ContentPackage Save(ContentPackage package)
        {
            return SaveInternal(package, true);
        }

        public void Save(IEnumerable<ContentPackage> entityList)
        {
            if(entityList != null)
            {
                foreach(var contPackage in entityList)
                {
                    SaveInternal(contPackage, false);
                }
            }
        }

        /// <summary>
        /// Deletes a content package from the database.
        /// </summary>
        /// <param name="package"></param>
        //public void Delete(ContentPackage package)
        //{
        //    for(int index = package.ResourceList.Count - 1; index >= 0; index--)
        //    {
        //        ContentPackageResource resource = package.ResourceList[index];
        //        _context.ContentPackageResources.Remove(resource);
        //    }

        //    _context.ContentPackages.Remove(package);
        //}

        private void DeleteInternal(ContentPackage contentPackage, bool saveChanges = true)
        {
            var contPackage = _context.ContentPackages.SingleOrDefault(x => x.ResourceID == contentPackage.ResourceID);
            if (contPackage == null) return;

            foreach(var rList in contentPackage.ResourceList)
            {
                _context.ContentPackageResources.Remove(rList);
            }

            _context.ContentPackages.Remove(contentPackage);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Delete(ContentPackage contPackage)
        {
            DeleteInternal(contPackage);
        }

        public void Delete(int resourceID)
        {
            var contPackage = _context.ContentPackages.Find(resourceID);
            DeleteInternal(contPackage);
        }

        public void Delete(IEnumerable<ContentPackage> contPackageList)
        {
            foreach (var contPackage in contPackageList)
            {
                DeleteInternal(contPackage, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<ContentPackage> GetAll()
        {
            return _context.ContentPackages.AsEnumerable();
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

        #endregion

    }
}
