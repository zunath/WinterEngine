using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;

namespace WinterEngine.Toolset.ExtendedEventArgs
{
    public class ModuleCreationEventArgs : EventArgs
    {
        private WinterModule _module;

        /// <summary>
        /// Gets or sets the module returned in the event args
        /// </summary>
        public WinterModule Module
        {
            get { return _module; }
            set { _module = value; }
        }
    }
}
