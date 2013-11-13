using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.Library.Managers
{
    public interface IModuleManager
    {
        bool CreateModule(GameModule module);
        void SaveModule(string path);
        void SaveModule();
        void OpenModule(string path);
        void CloseModule();
        bool CheckForMissingContentPackages();

    }
}
