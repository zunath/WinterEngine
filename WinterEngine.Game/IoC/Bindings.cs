using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ninject.Modules;

using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.Library;
using WinterEngine.Network;

namespace WinterEngine.Game.IoC
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameObjectRepository<Area>>().To<AreaRepository>();
            
            Bind<IGameObjectRepository<Conversation>>().To<ConversationRepository>();
            Bind<IGameObjectRepository<Creature>>().To<CreatureRepository>();
            Bind<IGameObjectRepository<Item>>().To<ItemRepository>();
            Bind<IGameObjectRepository<Placeable>>().To<PlaceableRepository>();
            Bind<IGameObjectRepository<Script>>().To<ScriptRepository>();

            Bind<IResourceRepository<Category>>().To<CategoryRepository>();
            Bind<IResourceRepository<ContentPackage>>().To<ContentPackageRepository>();
            Bind<IResourceRepository<ContentPackageResource>>().To<ContentPackageResourceRepository>();
            Bind<IResourceRepository<ItemType>>().To<ItemTypeRepository>();

        }
    }
}
