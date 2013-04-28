using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.FileAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;


namespace WinterEngine.Editor.Managers
{
    public class GameResourceManager : IDisposable
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public GameResourceManager()
        {
        }

        #endregion

        #region Events / Delegates

        public event EventHandler OnRebuildModuleComplete;

        #endregion

        #region Methods

        /// <summary>
        /// Handles refreshing content package resource links in the database and updating existing references.
        /// Content packages must have a valid path set for their path property.
        /// </summary>
        /// <param name="contentPackages"></param>
        public void RebuildModule(List<ContentPackage> contentPackages)
        {
            // Refresh all database links
            using (ContentPackageRepository packageRepo = new ContentPackageRepository())
            {
                // Remove missing content packages, upsert the current set of content packages
                packageRepo.ReplaceAll(contentPackages);
            }

            if (!Object.ReferenceEquals(OnRebuildModuleComplete, null))
            {
                OnRebuildModuleComplete(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles refreshing content package resource links in the database and updating existing references. Uses the currently active set of content packages in the database.
        /// </summary>
        public void RebuildModule()
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

        public void Dispose()
        {
        }

        #endregion
    }
}
