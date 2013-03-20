using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.Library.Factories
{
    public class DirectoryPathFactory
    {
        /// <summary>
        /// Returns the path to the specified directory. This path is relative to the root directory of the installation.
        /// </summary>
        /// <param name="directoryType"></param>
        /// <returns></returns>
        public string GetDirectoryPath(DirectoryTypeEnum directoryType)
        {
            string path = "";

            switch (directoryType)
            {
                case DirectoryTypeEnum.ContentPackages:
                    path = "./ContentPackages";
                    break;
                default:
                    path = "";
                    break;
            }

            return path;
        }
    }
}
