using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Repositories
{
    interface IResourceRepository<T> : IDisposable where T : GameResourceBase
    {
        void Add(T entity);
        void Add(List<T> entityList);
        void Update(T entity);
        void Upsert(T entity);
        void Delete(T entity);
        List<T> GetAll();
        bool Exists(T entity);
        T GetByID(int entityID);
    }
}
