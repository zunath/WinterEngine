using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.DataAccess.Repositories
{
    public interface IRepository
    {
        void SaveChanges();
    }

    public interface IGenericRepository<T> : IRepository
    {
        T Add(T entity);
        void Add(List<T> entityList);
        void Upsert(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAll();
        T GetByID(int resourceID);
    }

    public interface IGameObjectRepository<T> : IGenericRepository<T> where T : GameObjectBase
    {
        List<T> GetAllByResourceCategory(Category resourceCategory);
        T GetByResref(string resref);
        void Delete(int resourceID);
        void DeleteAllByCategory(Category resourceCategory);
        JSTreeNode GenerateJSTreeHierarchy();
        bool Exists(string resref);
    }

    public interface IResourceRepository<T> : IGenericRepository<T> where T : GameResourceBase
    {
    }
}
