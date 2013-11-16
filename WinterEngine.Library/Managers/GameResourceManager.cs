﻿using Ionic.Zip;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.XMLObjects;
using WinterEngine.Library.Extensions;
using WinterEngine.DataTransferObjects.Enumerations;
using System.Linq;


namespace WinterEngine.Editor.Managers
{
    public static class GameResourceManager
    {
        #region Methods

        public static ContentPackageXML DeserializeContentPackageFile(string filePath)
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

        public static List<ContentPackageResource> GetAllResourcesInContentPackage(string filePath)
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

        public static void RebuildModule(List<ContentPackage> contentPackages, ModuleRebuildModeEnum rebuildMode)
        {
            contentPackages.ForEach(a => a.ResourceList = GameResourceManager.GetAllResourcesInContentPackage(DirectoryPaths.ContentPackageDirectoryPath + a.FileName));

            try
            {
                using (ContentPackageRepository repo = new ContentPackageRepository())
                {
                    List<ContentPackage> existingContentPackages; 
                    if(rebuildMode == ModuleRebuildModeEnum.SystemResourcesOnly)
                    {
                        existingContentPackages = repo.GetAll();
                        existingContentPackages = (from package
                                                   in existingContentPackages
                                                   where package.IsSystemResource == true
                                                   select package).ToList();
                    }
                    else if(rebuildMode == ModuleRebuildModeEnum.UserResourcesOnly)
                    {
                        existingContentPackages = repo.GetAll();
                        existingContentPackages = (from package
                                                   in existingContentPackages
                                                   where package.IsSystemResource == false
                                                   select package).ToList();
                    }
                    else
                    {
                        existingContentPackages = repo.GetAll();
                    }

                    // Update or remove existing
                    foreach (ContentPackage current in existingContentPackages)
                    {
                        if (contentPackages.Exists(x => x.FileName == current.FileName))
                        {
                            repo.Update(current);
                            contentPackages.RemoveAll(x => x.FileName == current.FileName);
                        }
                        else
                        {
                            repo.Delete(current);
                        }
                    }

                    // Add the new ones
                    repo.Add(contentPackages);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Handles refreshing content package resource links in the database and updating existing references. Uses the currently active set of content packages in the database.
        /// </summary>
        public static void RebuildModule(ModuleRebuildModeEnum rebuildMode)
        {
            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                List<ContentPackage> contentPackages = (from package
                                                        in repo.GetAll()
                                                        where package.IsSystemResource == false
                                                        select package).ToList();
                RebuildModule(contentPackages, rebuildMode);
            }
        }

        #endregion
    }
}
