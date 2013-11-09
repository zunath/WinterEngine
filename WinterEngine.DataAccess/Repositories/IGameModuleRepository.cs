using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public interface IGameModuleRepository: IDisposable
    {
        GameModule Add(GameModule gameModule);
        void Update(GameModule module);
        void Delete(int resourceID);
        bool Exists();
        GameModule GetModule();
    }
}
