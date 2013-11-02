using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository GetRepository(string name);
        IGenericRepository<T> GetGenericRepository<T>();
        IGameObjectRepository<T> GetGameObjectRepository<T>() where T : GameObjectBase;
        IResourceRepository<T> GetResourceRepository<T>() where T : GameResourceBase;
    }
}
