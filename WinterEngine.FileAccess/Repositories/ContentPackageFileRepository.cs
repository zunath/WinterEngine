using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Ionic.Zip;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.Library.Factories;

namespace WinterEngine.FileAccess.Repositories
{
    public class ContentPackageFileRepository : IDisposable
    {

        #region Constants

        private const string ManifestFileName = "Manifest.xml";

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list containing the paths of all the content packages in the ContentPacks folder.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllContentPackagePaths()
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            List<string> contentPackagePaths = Directory.GetFiles(DirectoryPaths.ContentPackageDirectoryPath, "*" + factory.GetFileExtension(FileTypeEnum.ContentPackage)).ToList();
            return contentPackagePaths;
        }

        /// <summary>
        /// Returns a list containing the names of all the content packages in the ContentPacks folder.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllContentPackageFileNames()
        {
            FileExtensionFactory factory = new FileExtensionFactory();
            string[] filePaths = Directory.GetFiles(DirectoryPaths.ContentPackageDirectoryPath, "*" + factory.GetFileExtension(FileTypeEnum.ContentPackage));
            List<string> contentPackageFileNames = new List<string>();

            foreach (string path in filePaths)
            {
                contentPackageFileNames.Add(Path.GetFileNameWithoutExtension(path));
            }

            return contentPackageFileNames;
        }

        /// <summary>
        /// Extracts a ContentPackageResource to memory and returns the MemoryStream object.
        /// </summary>
        /// <param name="resource">The resource to extract to memory</param>
        /// <returns></returns>
        public MemoryStream ExtractResourceToMemory(ContentPackageResource resource)
        {
            MemoryStream stream = new MemoryStream();
            using (ZipFile zipFile = new ZipFile(resource.Package.ContentPackagePath))
            {
                zipFile[resource.FileName].Extract(stream);
            }

            return stream;
        }

        /// <summary>
        /// Returns a list of resource names from a content package.
        /// </summary>
        /// <param name="contentPackagePath"></param>
        /// <returns></returns>
        public List<string> GetAllResourceNames(ContentPackage package, bool includeManifestFile)
        {
            List<string> fileNames;

            using (ZipFile zipFile = new ZipFile(package.ContentPackagePath))
            {
                fileNames = zipFile.EntryFileNames.ToList();
            }

            // The manifest file shouldn't be returned in some situations. Find it and remove it from the list.
            if (!includeManifestFile)
            {
                fileNames.Remove(fileNames.Find(x => x == ManifestFileName));
            }

            return fileNames;
        }

        /// <summary>
        /// Takes a content package's file path and creates a ContentPackage object based on its data.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ContentPackage ConvertFileToContentPackage(string filePath)
        {
            ContentPackage package = new ContentPackage();
            package.ContentPackagePath = filePath;
            package.FileName = Path.GetFileNameWithoutExtension(filePath);
            package.IsSystemResource = false;
            package.ResourceType = ResourceTypeEnum.ContentPackage;

            return package;
        }

        /// <summary>
        /// Returns a list of ContentPackageResource objects based on the manifest file contained in the specified content package.
        /// </summary>
        /// <param name="contentPackagePath"></param>
        /// <returns></returns>
        public List<ContentPackageBuilderResource> GetContentPackageResourcesFromManifest(ContentPackage package)
        {
            List<ContentPackageBuilderResource> resources = ReadManifestFile(package);

            return resources;
        }


        /// <summary>
        /// Builds a manifest file containing details about each individual resource and adds it to the specified content package.
        /// Returns a MemoryStream object containing the Manifest file
        /// </summary>
        private MemoryStream CreateManifestFile(ContentPackage contentPackage, List<ContentPackageBuilderResource> resourceList)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            int index = 1;

            MemoryStream stream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("ContentPackageResources");
                
                foreach (ContentPackageBuilderResource resource in resourceList)
                {
                    writer.WriteStartElement("Resource");
                    writer.WriteAttributeString("ID", Convert.ToString(index));
                    writer.WriteAttributeString("Name", resource.ResourceName);
                    writer.WriteAttributeString("ResourceType", resource.ResourceType.ToString());
                    writer.WriteStartElement("Details");

                    writer.WriteEndElement(); // Close the Details element
                    writer.WriteEndElement(); // Close the Resource element

                    index++;
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return stream;
        }

        private List<ContentPackageBuilderResource> ReadManifestFile(ContentPackage package)
        {
            List<ContentPackageBuilderResource> resources = new List<ContentPackageBuilderResource>();
            try
            {
                using (ZipFile zipFile = new ZipFile(package.ContentPackagePath))
                {
                    using (Stream stream = zipFile[ManifestFileName].OpenReader())
                    {
                        Console.WriteLine(stream.ToString());

                        using (XmlReader reader = XmlReader.Create(stream))
                        {
                            while (reader.Read())
                            {
                                if(reader.ReadToFollowing("Resource"))
                                {
                                    int resourceID = Convert.ToInt32(reader.GetAttribute("ID"));
                                    string resourceName = reader.GetAttribute("Name");
                                    GameObjectTypeEnum gameObjectType = (GameObjectTypeEnum)Enum.Parse(typeof(GameObjectTypeEnum), reader.GetAttribute("ResourceType"), true);
                                    reader.ReadToFollowing("Details");
                                    ContentPackageBuilderResource resource = new ContentPackageBuilderResource(gameObjectType, ContentBuilderFileTypeEnum.PackageFile, resourceName);
                                    
                                    resources.Add(resource);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resources.Clear();
                throw new Exception("Error getting content package resources from manifest file.", ex);
            }

            return resources;
        }

        /// <summary>
        /// Saves a content package to disk.
        /// </summary>
        /// <param name="package"></param>
        public void SaveContentPackageToDisk(ContentPackage package, List<ContentPackageBuilderResource> resourceList)
        {
            string backupFilePath = package.ContentPackagePath + ".bak";
            try
            {
                // Back up existing file, just in case something goes wrong.
                if (File.Exists(package.ContentPackagePath))
                {
                    File.Move(package.ContentPackagePath, backupFilePath);
                }

                using (ZipFile zipFile = new ZipFile(package.ContentPackagePath))
                {
                    // Build the manifest file and add it to the zip package.
                    zipFile.AddEntry(ManifestFileName, CreateManifestFile(package, resourceList).ToArray());
                    foreach (ContentPackageBuilderResource resource in resourceList)
                    {
                        // Resource is not in the content package. It needs to be added to the content package directly.
                        if (resource.FileType == ContentBuilderFileTypeEnum.ExternalFile)
                        {
                            zipFile.AddFile(resource.ResourcePath, "");
                        }
                        // Resource exists in the content package already. It needs to be read into memory and copied into the new content package file.
                        else if (resource.FileType == ContentBuilderFileTypeEnum.PackageFile)
                        {
                            using (ZipFile backupZipFile = new ZipFile(backupFilePath))
                            {
                                MemoryStream stream = new MemoryStream();
                                backupZipFile[resource.ResourceName].Extract(stream);
                                zipFile.AddEntry(resource.ResourceName, stream.ToArray());
                            }
                        }
                    }

                    zipFile.Save();
                }

                // Remove the file backup.
                File.Delete(backupFilePath);
            }
            catch (Exception ex)
            {
                // Something went wrong. Revert to the backup file.
                if (File.Exists(package.ContentPackagePath))
                {
                    File.Delete(package.ContentPackagePath);
                }

                if (File.Exists(backupFilePath))
                {
                    File.Move(backupFilePath, package.ContentPackagePath);
                }

                throw new Exception("Error saving content package.", ex);
            }
        }

        #endregion

        public void Dispose()
        {
        }
    }
}