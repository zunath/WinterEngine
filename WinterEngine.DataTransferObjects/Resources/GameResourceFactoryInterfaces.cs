using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects
{
    
    public interface IAbilityFactory { Ability Create(); }    
    public interface ICategoryFactory { Category Create(); }    
    public interface ICharacterClassFactory { CharacterClass Create(); }    
    public interface IContentPackageFactory { ContentPackage Create(); }    
    public interface IItemPropertyFactory { ItemProperty Create(); }    
    public interface IRaceFactory { Race Create(); }

    public interface IGameResourceFactory { GameResourceBase Create(ResourceTypeEnum resourceType); }
}
