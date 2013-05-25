using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class TypeOfEventArgs : EventArgs
    {
        public Type ObjectType { get; set; }

        public TypeOfEventArgs(Type type)
        {
            this.ObjectType = type;
        }
    }
}
