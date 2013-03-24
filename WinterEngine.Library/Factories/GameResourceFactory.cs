using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.Library.Factories
{
    public class GameResourceFactory
    {
        #region Object creation methods

        /// <summary>
        /// Creates and returns a new resource object of the specified type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public GameResourceBase CreateObject(ResourceTypeEnum resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypeEnum.Ability:
                    return new Ability { ResourceType = resourceType };
                case ResourceTypeEnum.Category:
                    return new Category { ResourceType = resourceType };
                case ResourceTypeEnum.CharacterClass:
                    return new CharacterClass { ResourceType = resourceType };
                case ResourceTypeEnum.ContentPackage:
                    return new ContentPackage { ResourceType = resourceType };
                case ResourceTypeEnum.ItemProperty:
                    return new ItemProperty { ResourceType = resourceType };
                case ResourceTypeEnum.Race:
                    return new Race { ResourceType = resourceType };
                default:
                    throw new Exception("Resource type not supported.");
            }
        }

        #endregion
    }
}
