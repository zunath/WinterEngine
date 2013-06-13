using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
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
