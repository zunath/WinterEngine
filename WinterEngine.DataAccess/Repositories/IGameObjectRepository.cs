using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;


namespace WinterEngine.DataAccess.Repositories
{
    interface IGameObjectRepository<T> : IDisposable where T: GameObjectBase
    {
        void Add(T entity);
        void Add(List<T> entityList);
        void Upsert(T entity);
        void Update(T entity);
        void Delete(string resref);
        bool Exists(string resref);
        List<T> GetAll();
        List<T> GetAllByResourceCategory(Category resourceCategory);
        T GetByResref(string resref);
        void DeleteAllByCategory(Category resourceCategory);
        List<JSTreeNode> GenerateJSTreeCategories();
    }
}
