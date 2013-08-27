using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Paths;
using WinterEngine.DataTransferObjects.XMLObjects;


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
                    ContentPackageResource resource = new ContentPackageResource
                    {
                        FileName = current.FileName,
                        Name = Path.GetFileNameWithoutExtension(current.FileName),
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

        public static void RebuildModule(List<ContentPackage> contentPackages)
        {
            contentPackages.ForEach(a => a.ResourceList = GameResourceManager.GetAllResourcesInContentPackage(DirectoryPaths.ContentPackageDirectoryPath + a.FileName));

            try
            {
                using (ContentPackageRepository repo = new ContentPackageRepository())
                {
                    List<ContentPackage> existingContentPackages = repo.GetAllNonSystemResource();

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
        public static void RebuildModule()
        {
            List<ContentPackage> contentPackages;
            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                contentPackages = repo.GetAllNonSystemResource();
            }

            RebuildModule(contentPackages);
        }

        #endregion
    }
}
