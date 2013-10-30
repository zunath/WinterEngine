using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Interface;

namespace WinterEngine.DataAccess.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository GetRepository(string name);
        IGenericRepository<T> GetGenericRepository<T>();
        IGameObjectRepository<T> GetGameObjectRepository<T>();
        IResourceRepository<T> GetResourceRepository<T>();
    }
}
