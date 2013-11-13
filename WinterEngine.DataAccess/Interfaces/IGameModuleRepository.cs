using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess
{
    public interface IGameModuleRepository
    {
        GameModule Add(GameModule gameModule);
        void Save(GameModule module);
        void Delete(int resourceID);
        bool Exists();
        GameModule GetModule();
        void ApplyChanges();
    }
}
