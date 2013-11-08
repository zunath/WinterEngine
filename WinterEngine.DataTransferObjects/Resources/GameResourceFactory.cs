using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;


namespace WinterEngine.DataTransferobjects
{
    public class GameResourceFactory : IGameResourceFactory
    {
        [Inject]
        public IAbilityFactory abilityFactory { private get; set; }
        [Inject]
        public ICategoryFactory categoryFactory { private get; set; }
        [Inject]
        public ICharacterClassFactory characterClassFactory { private get; set; }
        [Inject]
        public IContentPackageFactory contentPackageFactory { private get; set; }
        [Inject]
        public IItemPropertyFactory itemPropertyFactory { private get; set; }
        [Inject]
        public IRaceFactory raceFactory { private get; set; }
        
        #region Object creation methods

        /// <summary>
        /// Creates and returns a new resource object of the specified type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public GameResourceBase Create(ResourceTypeEnum resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypeEnum.Ability:
                    return abilityFactory.Create();
                case ResourceTypeEnum.Category:
                    return categoryFactory.Create();
                case ResourceTypeEnum.CharacterClass:
                    return characterClassFactory.Create();
                case ResourceTypeEnum.ContentPackage:
                    return contentPackageFactory.Create();
                case ResourceTypeEnum.ItemProperty:
                    return itemPropertyFactory.Create();
                case ResourceTypeEnum.Race:
                    return raceFactory.Create();
                default:
                    throw new Exception("Resource type not supported.");
            }
        }

        #endregion
    }
}
