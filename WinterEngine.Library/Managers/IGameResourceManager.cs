using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.XMLObjects;

namespace WinterEngine.Library.Managers
{
    public interface IGameResourceManager
    {
        ContentPackageXML DeserializeContentPackageFile(string filePath);
        List<ContentPackageResource> GetAllResourcesInContentPackage(string filePath);
        void RebuildModule(List<ContentPackage> contentPackages, ModuleRebuildModeEnum rebuildMode);
        void RebuildModule(ModuleRebuildModeEnum rebuildMode);
    }
}
