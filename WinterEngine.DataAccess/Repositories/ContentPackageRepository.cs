using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using AutoMapper;
using Ionic.Zip;
using WinterEngine.DataAccess.Contexts;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess.Repositories
{
    public class ContentPackageRepository : RepositoryBase, IResourceRepository<ContentPackage>
    {
        #region Constants

        private const string ManifestFileName = "Manifest.xml";

        #endregion

        #region Constructors

        public ContentPackageRepository(string connectionString = "")
            : base(connectionString)
        {
        }

        #endregion
        
        #region Database Methods

        /// <summary>
        /// Adds a content package to the database.
        /// </summary>
        /// <param name="package"></param>
        public void Add(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                context.ContentPackages.Add(package);
                context.SaveChanges();
            }
        }
            
        /// <summary>
        /// Adds a list of content packages to the database.
        /// </summary>
        /// <param name="packageList"></param>
        public void Add(List<ContentPackage> packageList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (ContentPackage contentPackage in packageList)
                {
                    context.ContentPackages.Add(contentPackage);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a content package to the database if it doesn't exist.
        /// If it already exists, the existing one will be updated with new values.
        /// </summary>
        /// <param name="package"></param>
        public void Upsert(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ContentPackage dbResource = context.ContentPackages.SingleOrDefault(r => r.FileName.Equals(package.FileName));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    dbResource = Mapper.Map(package, dbResource);
                }
                else
                {
                    context.ContentPackages.Add(package);
                }

                context.SaveChanges();
            }
        }

        public void Upsert(List<ContentPackage> packageList)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                foreach (ContentPackage package in packageList)
                {
                    ContentPackage dbResource = context.ContentPackages.SingleOrDefault(r => r.FileName.Equals(package.FileName));

                    if (!Object.ReferenceEquals(dbResource, null))
                    {
                        dbResource = Mapper.Map(package, dbResource);
                    }
                    else
                    {
                        context.ContentPackages.Add(package);
                    }

                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Updates an existing content package with new values
        /// </summary>
        /// <param name="package"></param>
        public void Update(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                // Find the resource in the database that matches the passed-in item type's ID (primary key)
                ContentPackage dbResource = context.ContentPackages.SingleOrDefault(r => r.ResourceID.Equals(package.ResourceID));

                if (!Object.ReferenceEquals(dbResource, null))
                {
                    dbResource = Mapper.Map(package, dbResource);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes a content package from the database.
        /// </summary>
        /// <param name="package"></param>
        public void Delete(ContentPackage package)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                ContentPackage dbPackage = context.ContentPackages.SingleOrDefault(val => val.ResourceID == package.ResourceID);

                if (!Object.ReferenceEquals(dbPackage, null))
                {
                    context.ContentPackages.Remove(dbPackage);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Returns all content packages from the database in ascending order, based on their LoadOrder property.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAll()
        {
            List<ContentPackage> contentPackageList = new List<ContentPackage>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from contentPackage
                            in context.ContentPackages
                            orderby contentPackage.LoadOrder ascending
                            select contentPackage;
                contentPackageList = query.ToList();
            }

            return contentPackageList;
        }


        /// <summary>
        /// Returns the file names of every content package used by the module.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFileNames()
        {
            List<string> contentPackageFileNameList = new List<string>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from contentPackage
                            in context.ContentPackages
                            select contentPackage.FileName;

                contentPackageFileNameList = query.ToList();
            }

            return contentPackageFileNameList;
        }

        /// <summary>
        /// Returns all content packages which are not system resources from the database.
        /// </summary>
        /// <returns></returns>
        public List<ContentPackage> GetAllNonSystemResource()
        {
            List<ContentPackage> contentPackageList = new List<ContentPackage>();

            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from contentPackage
                            in context.ContentPackages
                            where contentPackage.IsSystemResource == false
                            select contentPackage;
                contentPackageList = query.ToList();
            }

            return contentPackageList;
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
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                var query = from contentPackage
                            in context.ContentPackages
                            where contentPackage.FileName == package.FileName
                            select contentPackage;

                List<ContentPackage> dbPackageList = query.ToList();

                if (dbPackageList.Count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Returns the content package matching the packageID passed in.
        /// Returns null if content package does not exist.
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public ContentPackage GetByID(int packageID)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                return context.ContentPackages.FirstOrDefault(x => x.ResourceID == packageID);
            }
        }

        /// <summary>
        /// Handles removing references to content packages and resources which are not tied to the list
        /// of contentPackages passed in to this method.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contentPackages"></param>
        private void RemoveMissingContent(WinterContext context, List<ContentPackage> contentPackages)
        {
            List<ContentPackage> existingContentPackages = (from package
                                                            in context.ContentPackages
                                                            select package).ToList();
            List<ContentPackage> removedContentPackages = existingContentPackages.Except(contentPackages).ToList();

            // Remove all references for content packages which no longer exist.
            foreach (ContentPackage package in removedContentPackages)
            {
                // Resources must be removed first.
                List<ContentPackageResource> resources = (from resource
                                                          in context.ContentPackageResources
                                                          where resource.ContentPackageID == package.ResourceID
                                                          select resource).ToList();
                foreach (ContentPackageResource resource in resources)
                {
                    context.ContentPackageResources.Remove(resource);
                }

                context.SaveChanges();
                context.ContentPackages.Remove(package);
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Removes content packages and resources which are no longer in use.
        /// Adds or updates content packages and resources.
        /// </summary>
        /// <param name="contentPackages"></param>
        public void ReplaceAll(List<ContentPackage> contentPackages)
        {
            using (WinterContext context = new WinterContext(ConnectionString))
            {
                RemoveMissingContent(context, contentPackages);

                // Now add or update content packages and associated resources.
                foreach (ContentPackage package in contentPackages)
                {
                    List<ContentPackageResource> resources = GetContentPackageResourcesFromManifestFile(package);
                    ContentPackage dbPackage = context.ContentPackages.FirstOrDefault(x => x.FileName == package.FileName);

                    // Is there an entry for this content package already in the database? 
                    // If so, remove any non-existent resources and add/update resources contained in the packages.
                    if (!Object.ReferenceEquals(dbPackage, null))
                    {
                        foreach (ContentPackageResource resource in resources)
                        {
                            resource.ContentPackageID = dbPackage.ResourceID;
                        }

                        List<ContentPackageResource> existingResources = (from resource
                                                                          in context.ContentPackageResources
                                                                          where resource.ContentPackageID == dbPackage.ResourceID
                                                                          select resource).ToList();
                        List<ContentPackageResource> removedResources = existingResources.Except(resources).ToList();

                        foreach (ContentPackageResource resource in removedResources)
                        {
                            context.ContentPackageResources.Remove(resource);
                        }

                        foreach (ContentPackageResource resource in resources)
                        {
                            ContentPackageResource dbResource = context.ContentPackageResources.FirstOrDefault(x => x.FileName == resource.FileName &&
                                                                                                                    x.ContentPackageID == resource.ContentPackageID);

                            if (!Object.ReferenceEquals(dbResource, null))
                            {
                                dbResource = Mapper.Map(resource, dbResource);
                            }
                            else
                            {
                                context.ContentPackageResources.Add(resource);
                            }
                        }

                    }
                    else
                    {
                        dbPackage = context.ContentPackages.Add(package);
                        context.SaveChanges();
                        foreach (ContentPackageResource resource in resources)
                        {
                            resource.ContentPackageID = dbPackage.ResourceID;
                            context.ContentPackageResources.Add(resource);
                        }
                    }
                }

                context.SaveChanges();
            }
        }


        #endregion

        #region File Access Methods

        /// <summary>
        /// Returns a list of ContentPackageResource objects based on the manifest file contained in the specified content package.
        /// </summary>
        /// <param name="contentPackagePath"></param>
        /// <returns></returns>
        public List<ContentPackageResource> GetContentPackageResourcesFromManifestFile(ContentPackage package)
        {
            List<ContentPackageResource> resources = new List<ContentPackageResource>();
            try
            {
                using (ZipFile zipFile = new ZipFile(package.ContentPackagePath))
                {
                    using (Stream stream = zipFile[ManifestFileName].OpenReader())
                    {
                        using (XmlReader reader = XmlReader.Create(stream))
                        {
                            while (reader.Read())
                            {
                                if (reader.ReadToFollowing("Resource"))
                                {
                                    int resourceID = Convert.ToInt32(reader.GetAttribute("ID"));
                                    string resourceName = reader.GetAttribute("ResourceName");
                                    ContentPackageResourceTypeEnum resourceType = (ContentPackageResourceTypeEnum)Enum.Parse(typeof(ContentPackageResourceTypeEnum), reader.GetAttribute("ResourceType"), true);
                                    string fileName = reader.GetAttribute("FileName");
                                    reader.ReadToFollowing("Details");
                                    ContentPackageResource resource = new ContentPackageResource(resourceType, ContentBuilderFileTypeEnum.PackageFile, resourceName, fileName);

                                    if (package.ResourceID > 0)
                                    {
                                        resource.ContentPackageID = package.ResourceID;
                                    }

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
        /// <param name="builderPackageDescription"></param>
        /// <param name="builderPackageName"></param>
        /// <param name="resourceList"></param>
        public void SaveContentPackageFile(ContentPackage package, List<ContentPackageResource> resourceList, string builderPackageName, string builderPackageDescription)
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
                    using (ZipFile backupZipFile = new ZipFile(backupFilePath))
                    {
                        // Build the manifest file and add it to the zip package.
                        zipFile.AddEntry(ManifestFileName, CreateManifestFile(package, resourceList, builderPackageName, builderPackageDescription).ToArray());
                        foreach (ContentPackageResource resource in resourceList)
                        {
                            // Resource is not in the content package. It needs to be added to the content package directly.
                            if (resource.FileType == ContentBuilderFileTypeEnum.ExternalFile)
                            {
                                zipFile.AddFile(resource.ResourcePath, "");
                            }
                            // Resource exists in the content package already. It needs to be read into memory and copied into the new content package file.
                            else if (resource.FileType == ContentBuilderFileTypeEnum.PackageFile)
                            {
                                using (Stream stream = backupZipFile[resource.FileName].OpenReader())
                                {
                                    zipFile.AddEntry(resource.FileName, stream);
                                }
                            }
                        }

                        zipFile.Save();
                    }
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



        /// <summary>
        /// Builds a manifest file containing details about each individual resource and adds it to the specified content package.
        /// Returns a MemoryStream object containing the Manifest file
        /// </summary>
        private MemoryStream CreateManifestFile(ContentPackage contentPackage, List<ContentPackageResource> resourceList, string builderPackageName, string builderPackageDescription)
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
                writer.WriteAttributeString("Name", builderPackageName);
                writer.WriteAttributeString("Description", builderPackageDescription);

                foreach (ContentPackageResource resource in resourceList)
                {
                    writer.WriteStartElement("Resource");
                    writer.WriteAttributeString("ID", Convert.ToString(index));
                    writer.WriteAttributeString("ResourceName", resource.ResourceName);
                    writer.WriteAttributeString("ResourceType", resource.ContentPackageResourceType.ToString());
                    writer.WriteAttributeString("FileName", resource.FileName);
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


        /// <summary>
        /// Takes a content package's file path and creates a ContentPackage object based on its data.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ContentPackage ConvertFileToContentPackage(string filePath)
        {
            ContentPackage package = new ContentPackage(filePath, false);

            using (ZipFile zipFile = new ZipFile(package.ContentPackagePath))
            {
                using (Stream stream = zipFile[ManifestFileName].OpenReader())
                {
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        while (reader.Read())
                        {
                            if (reader.ReadToFollowing("ContentPackageResources"))
                            {
                                package.VisibleName = reader.GetAttribute("Name");
                                package.Description = reader.GetAttribute("Description");
                            }
                        }
                    }
                }
            }

            return package;
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
                contentPackageFileNames.Add(Path.GetFileName(path));
            }

            return contentPackageFileNames;
        }
        
        public void Dispose()
        {
        }

        #endregion

    }
}
