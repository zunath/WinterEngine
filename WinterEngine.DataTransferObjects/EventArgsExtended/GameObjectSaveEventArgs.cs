using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class GameObjectSaveEventArgs : EventArgs
    {
        public Area ActiveArea { get; set; }
        public Creature ActiveCreature { get; set; }
        public Item ActiveItem { get; set; }
        public Placeable ActivePlaceable { get; set; }
        public Conversation ActiveConversation { get; set; }
        public Script ActiveScript { get; set; }
        public Tileset ActiveTileset { get; set; }

        public GameObjectSaveEventArgs(Area area)
        {
            this.ActiveArea = area;
        }

        public GameObjectSaveEventArgs(Creature creature)
        {
            this.ActiveCreature = creature;
        }

        public GameObjectSaveEventArgs(Item item)
        {
            this.ActiveItem = item;
        }

        public GameObjectSaveEventArgs(Placeable placeable)
        {
            this.ActivePlaceable = placeable;
        }

        public GameObjectSaveEventArgs(Conversation conversation)
        {
            this.ActiveConversation = conversation;
        }

        public GameObjectSaveEventArgs(Script script)
        {
            this.ActiveScript = script;
        }

        public GameObjectSaveEventArgs(Tileset tileset)
        {
            this.ActiveTileset = tileset;
        }

    }
}
