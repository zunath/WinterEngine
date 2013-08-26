using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Paths;


namespace WinterEngine.Editor.Managers
{
    public static class GameResourceManager
    {
        #region Methods

        private static List<ContentPackageResource> GetAllResourcesInContentPackage(ContentPackage package)
        {
            List<ContentPackageResource> resources = new List<ContentPackageResource>();

            return resources;
        }

        public static void RebuildModule(List<ContentPackage> contentPackages)
        {
            contentPackages.ForEach(a => a.ResourceList = GameResourceManager.GetAllResourcesInContentPackage(a));

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

            // TO-DO: Update object resource links.

        }

        /// <summary>
        /// Handles refreshing content package resource links in the database and updating existing references. Uses the currently active set of content packages in the database.
        /// </summary>
        public static void RebuildModule()
        {
            List<ContentPackage> contentPackages;
            using (ContentPackageRepository repo = new ContentPackageRepository())
            {
                contentPackages = repo.GetAll();
            }

            foreach (ContentPackage package in contentPackages)
            {
                package.ContentPackagePath = DirectoryPaths.ContentPackageDirectoryPath + package.FileName;
            }

            RebuildModule(contentPackages);
        }

        #endregion
    }
}
