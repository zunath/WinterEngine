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
    public class ContentPackageResourceRepository : IResourceRepository<ContentPackageResource>
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

        public ContentPackageResource Add(ContentPackageResource resource)
        {
            return _context.ContentPackageResources.Add(resource);
        }

        public void Add(List<ContentPackageResource> resourceList)
        {
            _context.ContentPackageResources.AddRange(resourceList);
        }

        public void Save(ContentPackageResource resource)
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

        public void Update(ContentPackageResource resource)
        {
            ContentPackageResource dbResource = _context.ContentPackageResources.Where(x => x.ResourceID == resource.ResourceID).SingleOrDefault();
            if (dbResource == null) return;

            _context.Entry(dbResource).CurrentValues.SetValues(resource);
        }        

        //public void Upsert(List<ContentPackageResource> resourceList)
        //{
        //    foreach (ContentPackageResource resource in resourceList)
        //    {
        //        Upsert(resource);
        //    }
        //}

        public void Delete(ContentPackageResource resource)
        {
            _context.ContentPackageResources.Remove(resource);
        }

        public void Delete(int resourceID)
        {
            var contPackResource = _context.ContentPackageResources.Find(resourceID);
            Delete(contPackResource);
        }

        public List<ContentPackageResource> GetAll()
        {
            return _context.ContentPackageResources.ToList();
        }

        public ContentPackageResource GetByID(int resourceID)
        {
            return _context.ContentPackageResources.Where(x => x.ResourceID == resourceID).SingleOrDefault();
        }

        public void ApplyChanges()
        {
            _context.SaveChanges();
        }

        //todo: move this somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects()
        //{
        //    List<DropDownListUIObject> items = (from contentPackageResource
        //                                        in Context.ContentPackageResourceRepository.Get()
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = contentPackageResource.Name,
        //                                            ResourceID = contentPackageResource.ResourceID
        //                                        }).ToList();

        //    return items;
        //}

        //todo: move this somewhere else
        //public List<DropDownListUIObject> GetAllUIObjects(ContentPackageResourceTypeEnum resourceType, bool includeDefault = false)
        //{
        //    List<DropDownListUIObject> items = (from category
        //                                       in Context.ContentPackageResourceRepository.Get()
        //                                        where category.ContentPackageResourceType == resourceType
        //                                        select new DropDownListUIObject
        //                                        {
        //                                            Name = category.Name,
        //                                            ResourceID = category.ResourceID
        //                                        }).ToList();

        //    if (includeDefault)
        //    {
        //        items.Insert(0, new DropDownListUIObject(0, "(None)"));
        //    }

        //    return items;
        //}

        //public List<ContentPackageResource> GetAllByResourceType(ContentPackageResourceTypeEnum resourceType)
        //{
        //    return _context.ContentPackageResources.Where(x => x.ContentPackageResourceType == resourceType).ToList();
        //}

        //public bool Exists(ContentPackageResource resource)
        //{
        //    List<ContentPackageResource> resources = _context.ContentPackageResources.Where(x => x.ResourceID == resource.ResourceID).ToList();
        //    if (resources.Count > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public int GetDefaultResourceID()
        //{
        //    var defaultObject = _context.ContentPackageResources.Where(x => x.IsDefault).FirstOrDefault();
        //    return defaultObject == null ? 0 : defaultObject.ResourceID;
        //}

        #endregion
    }
}
