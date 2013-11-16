using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> GetGenericRepository<T>();
        IGameObjectRepository<T> GetGameObjectRepository<T>() where T : GameObjectBase;
        IGenericRepository<T> GetResourceRepository<T>() where T : GameResourceBase;
    }
}
