﻿using System;
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
    public class ContentPackageRepository : IResourceRepository<ContentPackage>, IRepository
    {
        #region Constants

        private const string ManifestFileName = "Manifest.xml";

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
        public void Upsert(ContentPackage package)
        {
            if (package.ResourceID <= 0)
            {
                _context.ContentPackages.Add(package);
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

        /// <summary>
        /// Returns all content packages from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAll()
        {
            return _context.ContentPackages.ToList();
        }

        //todo: Move this logic somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from contentPackage
        //                                        in Context.ContentPackageRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = contentPackage.Name,
        //                                            ResourceID = contentPackage.ResourceID
        //                                        }).ToList();
        //    return items;
        //}


        /// <summary>
        /// Returns the file names of every content package used by the module.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFileNames()
        {
            List<ContentPackage> packages = _context.ContentPackages.ToList();
            return packages.Select(x => x.FileName).ToList();
        }

        /// <summary>
        /// Returns all content packages which are not system resources (aka: user resources) from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAllUserResources()
        {
            return _context.ContentPackages.Where(x => x.IsSystemResource == false).ToList();
        }

        /// <summary>
        /// Returns all content packages which are system resources from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAllSystemResources()
        {
            return _context.ContentPackages.Where(x => x.IsSystemResource == true).ToList();
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
            ContentPackage dbPackage = _context.ContentPackages.Where(x => x.FileName == package.FileName).SingleOrDefault();
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
            return _context.ContentPackages.Where(x => x.ResourceID == packageID).SingleOrDefault();
        }

        public int GetDefaultResourceID()
        {
            ContentPackage defaultObject = _context.ContentPackages.Where(x => x.IsDefault).FirstOrDefault();
            return defaultObject == null ? 0 : defaultObject.ResourceID;
        }

        #endregion

        public object Load(int resourceID)
        {
            throw new NotImplementedException();
        }

        public int Save(object gameObject)
        {
            throw new NotImplementedException();
        }

        public void DeleteByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int resourceID)
        {
            throw new NotImplementedException();
        }
    }
}
