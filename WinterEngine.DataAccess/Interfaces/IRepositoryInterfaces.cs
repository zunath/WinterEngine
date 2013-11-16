using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.UIObjects;

namespace WinterEngine.DataAccess
{
    public interface IGenericRepository<T>
    {
        T Save(T entity);
        void Save(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(int resourceID);
        void Delete(IEnumerable<T> entities);
        IEnumerable<T> GetAll();
        T GetByID(int resourceID);
    }

    public interface IGameObjectRepository<T> : IGenericRepository<T> where T : GameObjectBase
    {
        IEnumerable<T> GetAllByResourceCategory(Category resourceCategory);
        T GetByResref(string resref);
        void DeleteAllByCategory(Category resourceCategory);
        bool Exists(string resref);
    }

    public interface IUITreeObjectRepository
    {
        JSTreeNode GenerateJSTreeHierarchy<T>() where T: GameObjectBase;
    }

    public interface IUIObjectRepository
    {
        IEnumerable<DropDownListUIObject> GetAllUIObjects();
    }

    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllResourceCategoriesByResourceType(GameObjectTypeEnum resourceType);
        Category GetUncategorizedCategory(GameObjectTypeEnum resourceType);
    }

    public interface IGameModuleRepository
    {
        GameModule GetModule();
    }

}
