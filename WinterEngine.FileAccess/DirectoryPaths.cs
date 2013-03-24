using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.IO;

namespace WinterEngine.FileAccess
{
    public static class DirectoryPaths
    {
        #region Fields

        private const string _contentPackagePath = "ContentPacks";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path to the content package directory.
        /// </summary>
        public static string ContentPackageDirectoryPath
        {
            get 
            {
                return FileManager.RelativeDirectory + @"Content/" + _contentPackagePath;
            }
        }

        #endregion
    }
}
