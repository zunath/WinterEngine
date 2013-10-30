using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Factories
{
    public interface IAreaFactory { Area Create(); }
    public interface IConversationFactory { Conversation Create(); }
    public interface ICreatureFactory { Creature Create(); }
    public interface IItemFactory { Item Create(); }
    public interface IPlaceableFactory { Placeable Create(); }
    public interface IScriptFactory { Script Create(); }
    public interface ITilesetFactory { Tileset Create(); }
}
