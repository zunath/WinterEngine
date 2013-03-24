using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Library.Factories;

namespace WinterEngine.FileAccess
{
    public class ContentPackageManager : IDisposable
    {
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


        public void Dispose()
        {
        }
    }
}
