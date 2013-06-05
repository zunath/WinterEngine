using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall.IO;

namespace WinterEngine.DataTransferObjects.Paths
{
    public static class DirectoryPaths
    {
        #region Fields

        private const string _contentPackagePath = "ContentPacks";
        private const string _characterVaultPath = "CharacterVault";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path to the content package directory.
        /// </summary>
        public static string ContentPackageDirectoryPath
        {
            get 
            {
                return FileManager.RelativeDirectory + @"Content/" + _contentPackagePath + "/";
            }
        }

        /// <summary>
        /// Gets the path to the character vault directory which contains all of the characters
        /// created on a server by players.
        /// </summary>
        public static string CharacterVaultDirectoryPath
        {
            get
            {
                return FileManager.RelativeDirectory + @"Content/" + _characterVaultPath + "/";
            }
        }

        #endregion
    }
}
