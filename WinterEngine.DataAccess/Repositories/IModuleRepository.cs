using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess
{
    public interface IModuleRepository
    {
        void Add(GameModule gameModule);

        GameModule GetModule();

        void UpdateModule(GameModule module);

    }
}
