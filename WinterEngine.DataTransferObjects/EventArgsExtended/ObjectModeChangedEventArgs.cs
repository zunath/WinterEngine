using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class ObjectModeChangedEventArgs: EventArgs
    {
        public GameObjectTypeEnum GameObjectType { get; set; }

        public ObjectModeChangedEventArgs(GameObjectTypeEnum gameObjectType)
        {
            this.GameObjectType = gameObjectType;
        }
    }
}
