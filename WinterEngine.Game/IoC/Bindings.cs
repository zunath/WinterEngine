using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ninject.Modules;
using Ninject.Extensions.Factory;

using WinterEngine.DataAccess;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.Editor;
using WinterEngine.Network;

using WinterEngine.Game.Performance;
using System.Collections;

namespace WinterEngine.Game.IoC
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {

            #region WinterEngine.DataAccess

            //Bind<IGameObjectRepository<Area>>().To<AreaRepository>()
            Bind<IGameObjectRepository<Conversation>>().To<ConversationRepository>();
            Bind<IGameObjectRepository<Creature>>().To<CreatureRepository>();
            Bind<IGameObjectRepository<Item>>().To<ItemRepository>();
            Bind<IGameObjectRepository<Placeable>>().To<PlaceableRepository>();

            Bind<IResourceRepository<Category>>().To<CategoryRepository>();
            Bind<IResourceRepository<ContentPackage>>().To<ContentPackageRepository>();
            Bind<IResourceRepository<ContentPackageResource>>().To<ContentPackageResourceRepository>();
            Bind<IResourceRepository<ItemType>>().To<ItemTypeRepository>();

            #endregion

            #region WinterEngine.Game

            //Bind(typeof(IPoolable)).To(typeof(PoolList<>));

            #endregion
        }
    }
}
