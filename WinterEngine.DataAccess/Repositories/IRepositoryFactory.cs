using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository GetRepository(string name);
        IRepository GetRepository(GameObjectTypeEnum gameType);
        IGenericRepository<T> GetGenericRepository<T>();
        IGameObjectRepository<T> GetGameObjectRepository<T>() where T : GameObjectBase;
        IResourceRepository<T> GetResourceRepository<T>() where T : GameResourceBase;
    }
}
