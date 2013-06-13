using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class CameraButtonPressEventArgs : EventArgs
    {
        public CameraMovementTypeEnum MovementType { get; set; }

        public CameraButtonPressEventArgs(CameraMovementTypeEnum movementType)
        {
            this.MovementType = movementType;
        }
    }
}
