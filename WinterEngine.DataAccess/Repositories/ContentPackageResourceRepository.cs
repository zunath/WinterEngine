﻿using System;
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
    public class ContentPackageResourceRepository : IGenericRepository<ContentPackageResource>
    {

        private readonly ModuleDataContext _context;
        private readonly bool _autoSaveChanges;

        #region Constructors

        public ContentPackageResourceRepository(ModuleDataContext context, bool autoSave = true)
        {
            if (context == null) throw new ArgumentNullException("DbContext");
            _context = context;
            _autoSaveChanges = autoSave;
        }

        #endregion

        #region Methods

        private ContentPackageResource InternalSave(ContentPackageResource contPackResource, bool saveChanges)
        {
            ContentPackageResource retContentPackResource;
            if (contPackResource.ResourceID <= 0)
            {
                retContentPackResource = _context.ContentPackageResources.Add(contPackResource);
            }
            else
            {
                retContentPackResource = _context.ContentPackageResources.SingleOrDefault(x => x.ResourceID == contPackResource.ResourceID);
                if (retContentPackResource == null) return null;
                _context.Entry(retContentPackResource).CurrentValues.SetValues(contPackResource);

            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }

            return retContentPackResource;
        }

        public ContentPackageResource Save(ContentPackageResource resource)
        {
            return InternalSave(resource, true);
        }

        public void Save(IEnumerable<ContentPackageResource> entityList)
        {
            if(entityList != null)
            {
                foreach(var cpr in entityList)
                {
                    InternalSave(cpr, false);
                }
                _context.SaveChanges();
            }
        }

        private void DeleteInternal(ContentPackageResource contentPackageResource, bool saveChanges = true)
        {
            var contPackageResource = _context.ContentPackageResources.SingleOrDefault(x => x.ResourceID == contentPackageResource.ResourceID);
            if (contPackageResource == null) return;

            _context.ContentPackageResources.Remove(contentPackageResource);

            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }
        public void Delete(ContentPackageResource contPackageResource)
        {
            DeleteInternal(contPackageResource);
        }

        public void Delete(int resourceID)
        {
            var ability = _context.ContentPackageResources.Find(resourceID);
            DeleteInternal(ability);
        }

        public void Delete(IEnumerable<ContentPackageResource> contPackageList)
        {
            foreach (var contPackage in contPackageList)
            {
                DeleteInternal(contPackage, false);
            }
            _context.SaveChanges();
        }

        public IEnumerable<ContentPackageResource> GetAll()
        {
            return _context.ContentPackageResources.ToList();
        }

        public ContentPackageResource GetByID(int resourceID)
        {
            return _context.ContentPackageResources.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }


        #endregion
    }
}
