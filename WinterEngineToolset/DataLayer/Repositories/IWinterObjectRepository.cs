using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    interface IWinterObjectRepository : IDisposable
    {
        List<WinterObjectDTO> GetAllObjects();
        List<WinterObjectDTO> GetAllObjectsByResourceCategory(ResourceCategoryDTO resourceDTO);
        WinterObjectDTO GetObjectByResref(string resref);
    }
}
