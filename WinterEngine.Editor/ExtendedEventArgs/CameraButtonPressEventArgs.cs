using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Editor.Enums;

namespace WinterEngine.Editor.ExtendedEventArgs
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
