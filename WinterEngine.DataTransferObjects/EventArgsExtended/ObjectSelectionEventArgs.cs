﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataTransferObjects.EventArgsExtended
{
    public class ObjectSelectionEventArgs : EventArgs
    {
        public int ResourceID { get; set; }
    }
}
