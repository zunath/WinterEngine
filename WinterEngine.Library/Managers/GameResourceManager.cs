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

        public static void UpdateModuleContentPackages(List<ContentPackage> contentPackages)
        {

            foreach (ContentPackage package in contentPackages)
            {
                string path = DirectoryPaths.ContentPackageDirectoryPath + package.FileName;

            }
        }

        /// <summary>
        /// Handles refreshing content package resource links in the database and updating existing references.
        /// Content packages must have a valid path set for their path property.
        /// </summary>
        /// <param name="contentPackages"></param>
        public static void RebuildModule(List<ContentPackage> contentPackages)
        {
            // Refresh all database links
            using (ContentPackageRepository packageRepo = new ContentPackageRepository())
            {
                // Remove missing content packages, upsert the current set of content packages
                packageRepo.ReplaceAll(contentPackages);
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
