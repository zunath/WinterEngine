using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Ionic.Zip;

using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.XMLObjects;
using WinterEngine.Library.Extensions;
using WinterEngine.DataTransferObjects.Enumerations;
using System.Linq;
using WinterEngine.DataAccess;

namespace WinterEngine.Library.Managers
{
    public class GameResourceManager : IGameResourceManager
    {
        #region Methods

        private readonly IGenericRepository<ContentPackage> _contentPackageRepository;

        public GameResourceManager(IGenericRepository<ContentPackage> contentPackageRepository)
        {
            if (contentPackageRepository == null) throw new ArgumentNullException("contentPackageRepository");
            _contentPackageRepository = contentPackageRepository;
        }

        public ContentPackageXML DeserializeContentPackageFile(string filePath)
        {
            ContentPackageXML packageXML;

            try
            {
                using (ZipFile zipFile = new ZipFile(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ContentPackageXML));
                    MemoryStream stream = new MemoryStream();
                    zipFile["Manifest.xml"].Extract(stream);
                    stream.Position = 0;

                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        packageXML = serializer.Deserialize(reader) as ContentPackageXML;
                    }
                }
                packageXML.Name.Truncate(64);
                packageXML.Description.Truncate(4000);

                return packageXML;
            }
            catch
            {
                throw;
            }
        }

        public List<ContentPackageResource> GetAllResourcesInContentPackage(string filePath)
        {
            try
            {
                List<ContentPackageResource> resources = new List<ContentPackageResource>();
                ContentPackageXML xmlModel = DeserializeContentPackageFile(filePath);

                foreach (ContentPackageResourceXML current in xmlModel.ResourceList)
                {
                    string truncatedName = Path.GetFileNameWithoutExtension(current.FileName).Truncate(64);

                    ContentPackageResource resource = new ContentPackageResource
                    {
                        FileName = current.FileName,
                        Name = truncatedName,
                        ContentPackageResourceType = current.ResourceType
                    };
                    resources.Add(resource);
                }

                return resources;
            }
            catch
            {
                throw;
            }
        }

        public void RebuildModule(List<ContentPackage> contentPackages, ModuleRebuildModeEnum rebuildMode)
        {
            contentPackages.ForEach(a => a.ResourceList = GetAllResourcesInContentPackage(DirectoryPaths.ContentPackageDirectoryPath + a.FileName));

            try
            {

                List<ContentPackage> existingContentPackages;
                if (rebuildMode == ModuleRebuildModeEnum.SystemResourcesOnly)
                {
                    existingContentPackages = _contentPackageRepository.GetAll().ToList();
                    existingContentPackages = (from package
                                               in existingContentPackages
                                               where package.IsSystemResource == true
                                               select package).ToList();
                }
                else if (rebuildMode == ModuleRebuildModeEnum.UserResourcesOnly)
                {
                    existingContentPackages = _contentPackageRepository.GetAll().ToList();
                    existingContentPackages = (from package
                                               in existingContentPackages
                                               where package.IsSystemResource == false
                                               select package).ToList();
                }
                else
                {
                    existingContentPackages = _contentPackageRepository.GetAll().ToList();
                }

                // Update or remove existing
                foreach (ContentPackage current in existingContentPackages)
                {
                    if (contentPackages.Exists(x => x.FileName == current.FileName))
                    {
                        _contentPackageRepository.Save(current);
                        contentPackages.RemoveAll(x => x.FileName == current.FileName);
                    }
                    else
                    {
                        _contentPackageRepository.Delete(current);
                    }
                }

                // Add the new ones
                _contentPackageRepository.Save(contentPackages);


            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Handles refreshing content package resource links in the database and updating existing references. Uses the currently active set of content packages in the database.
        /// </summary>
        public void RebuildModule(ModuleRebuildModeEnum rebuildMode)
        {
            List<ContentPackage> contentPackages = (from package
                                                    in _contentPackageRepository.GetAll()
                                                    where package.IsSystemResource == false
                                                    select package).ToList();
            RebuildModule(contentPackages, rebuildMode);

        }

        #endregion
    }
}
