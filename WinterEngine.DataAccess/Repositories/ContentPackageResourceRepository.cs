﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataAccess.Repositories.Interfaces;


namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageResourceRepository : RepositoryBase, IResourceRepository<ContentPackageResource>
    {
        #region Constructors

        public ContentPackageResourceRepository(ModuleDataContext context, bool autoSaveChanges = true)
            : base(context, autoSaveChanges)
        {
        }

        #endregion

        #region Methods

        public ContentPackageResource Add(ContentPackageResource resource)
        {
            return _context.ContentPackageResources.Add(resource);
        }

        public void Add(List<ContentPackageResource> resourceList)
        {
            _context.ContentPackageResources.AddRange(resourceList);
        }

        public void Update(ContentPackageResource resource)
        {
            ContentPackageResource dbResource = _context.ContentPackageResources.Where(x => x.ResourceID == resource.ResourceID).SingleOrDefault();
            if (dbResource == null) return;

            _context.Entry(dbResource).CurrentValues.SetValues(resource);
        }

        public void Upsert(ContentPackageResource resource)
        {
            if (resource.ResourceID <= 0)
            {
                _context.ContentPackageResources.Add(resource);
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
            _context.ContentPackageResources.Remove(resource);
        }

        public List<ContentPackageResource> GetAll()
        {
            return _context.ContentPackageResources.ToList();
        }

        public List<ContentPackageResource> GetAllByResourceType(ContentPackageResourceTypeEnum resourceType)
        {
            return _context.ContentPackageResources.Where(x => x.ContentPackageResourceType == resourceType).ToList();
        }

        public bool Exists(ContentPackageResource resource)
        {
            List<ContentPackageResource> resources = _context.ContentPackageResources.Where(x => x.ResourceID == resource.ResourceID).ToList();
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
            return _context.ContentPackageResources.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        #endregion
    }
}
