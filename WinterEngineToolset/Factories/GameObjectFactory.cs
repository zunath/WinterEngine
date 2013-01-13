﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Library.DataAccess.Contexts;
using WinterEngine.Library.DataAccess.DataTransferObjects.GameObjects;
using WinterEngine.Library.DataAccess.Repositories;
using WinterEngine.Toolset.Enumerations;
using WinterEngine.Library.DataAccess.DataTransferObjects.ResourceObjects;

namespace WinterEngine.Toolset.Factories
{
    /// <summary>
    /// Factory method for creating game objects.
    /// </summary>
    public class GameObjectFactory
    {
        #region Object creation methods

        /// <summary>
        /// Creates and returns a new object of the specified type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public GameObject CreateObject(ResourceTypeEnum resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypeEnum.Area:
                    return new Area();
                case ResourceTypeEnum.Conversation:
                    return null;
                case ResourceTypeEnum.Creature:
                    return new Creature();
                case ResourceTypeEnum.Item:
                    return new Item();
                case ResourceTypeEnum.Placeable:
                    return new Placeable();
                case ResourceTypeEnum.Script:
                    return null;
                default:
                    return null;
            }
        }

        #endregion

        #region Database methods

        /// <summary>
        /// Adds an object of a specified type to the database.
        /// </summary>
        /// <param name="winterObject">The winter object to add to the database. This will be type-converted and added to the correct table when run.</param>
        /// <param name="resourceType">The type of resource to add.</param>
        public void AddToDatabase(GameObject winterObject, ResourceTypeEnum resourceType)
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    repo.Add(winterObject as Area);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    repo.Add(winterObject as Creature);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    repo.Add(winterObject as Item);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    repo.Add(winterObject as Placeable);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Deletes an object with the specified resref from the database.
        /// </summary>
        /// <param name="winterObject"></param>
        /// <param name="resourceType"></param>
        public void DeleteFromDatabase(string resref, ResourceTypeEnum resourceType)
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    repo.Delete(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }

        }

        /// <summary>
        /// Returns a list of all objects in the database of a specified resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public List<GameObject> GetAllFromDatabase(ResourceTypeEnum resourceType)
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    return repo.GetAll().ConvertAll<GameObject>(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    return repo.GetAll().ConvertAll<GameObject>(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    return repo.GetAll().ConvertAll<GameObject>(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    return repo.GetAll().ConvertAll<GameObject>(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns all objects from the database that have a matching resource category.
        /// </summary>
        /// <param name="resourceCategory">The resource category all return values must match</param>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <returns></returns>
        public List<GameObject> GetAllFromDatabaseByResourceCategory(ResourceCategory resourceCategory, ResourceTypeEnum resourceType)
        {
            List<GameObject> retList = new List<GameObject>();

            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    return repo.GetAllByResourceCategory(resourceCategory).ConvertAll(x => (GameObject)x);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns an object from the database with a matching resref.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <param name="resourceType">The type of resource to look for.</param>
        /// <returns></returns>
        public GameObject GetFromDatabaseByResref(string resref, ResourceTypeEnum resourceType)
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    return repo.GetByResref(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Delete all objects from database that match a specified resource category.
        /// </summary>
        /// <param name="resourceCategory">The resource category to remove all objects from.</param>
        /// <param name="resourceType">The type of resource to look for.</param>
        public void DeleteFromDatabaseByCategory(ResourceCategory resourceCategory, ResourceTypeEnum resourceType)
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    repo.DeleteAllByCategory(resourceCategory);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns True if an object exists in the database.
        /// Returns False if an object does not exist in the database.
        /// </summary>
        /// <param name="resref">The resource reference to search for.</param>
        /// <param name="resourceType">The resource type to look for.</param>
        /// <returns></returns>
        public bool DoesObjectExistInDatabase(string resref, ResourceTypeEnum resourceType)
        {
            if (resourceType == ResourceTypeEnum.Area)
            {
                using (AreaRepository repo = new AreaRepository())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Conversation)
            {
                throw new NotImplementedException();
            }
            else if (resourceType == ResourceTypeEnum.Creature)
            {
                using (CreatureRepository repo = new CreatureRepository())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Item)
            {
                using (ItemRepository repo = new ItemRepository())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Placeable)
            {
                using (PlaceableRepository repo = new PlaceableRepository())
                {
                    return repo.Exists(resref);
                }
            }
            else if (resourceType == ResourceTypeEnum.Script)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        #endregion
    }
}
