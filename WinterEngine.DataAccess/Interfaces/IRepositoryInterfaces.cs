using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;

namespace WinterEngine.DataAccess
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        void Add(List<T> entityList);
        void Save(T entity); //Why have an update and save?
        void Update(T entity);
        void Delete(T entity);
        void Delete(int resourceID);
        List<T> GetAll();
        T GetByID(int resourceID);
        void ApplyChanges();
    }

    public interface IGameObjectRepository<T> : IGenericRepository<T> where T : GameObjectBase
    {
        List<T> GetAllByResourceCategory(Category resourceCategory);
        T GetByResref(string resref);
        void DeleteAllByCategory(Category resourceCategory);
        JSTreeNode GenerateJSTreeHierarchy();
        bool Exists(string resref);
    }

    public interface IResourceRepository<T> : IGenericRepository<T>
    {
    }
}
