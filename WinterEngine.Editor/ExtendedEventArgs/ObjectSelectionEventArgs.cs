using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Editor.Enums;

namespace WinterEngine.Editor.ExtendedEventArgs
{
    public class ObjectSelectionEventArgs : EventArgs
    {
        public ObjectSelectionTypeEnum ObjectType { get; set; }

        public ObjectSelectionEventArgs(ObjectSelectionTypeEnum objectType)
        {
            this.ObjectType = objectType;
        }
    }
}
