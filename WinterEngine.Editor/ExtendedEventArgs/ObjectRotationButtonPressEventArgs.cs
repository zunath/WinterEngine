using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Editor.Enums;

namespace WinterEngine.Editor.ExtendedEventArgs
{
    public class ObjectRotationButtonPressEventArgs : EventArgs
    {
        public ObjectRotationTypeEnum RotationType { get; set; }

        public ObjectRotationButtonPressEventArgs(ObjectRotationTypeEnum rotationType)
        {
            this.RotationType = rotationType;
        }
    }
}
