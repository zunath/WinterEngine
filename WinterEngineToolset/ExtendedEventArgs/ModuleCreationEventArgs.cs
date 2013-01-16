using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Library.Factories;

namespace WinterEngine.Toolset.ExtendedEventArgs
{
    public class ModuleCreationEventArgs : EventArgs
    {
        private WinterModuleFactory _moduleFactory;

        /// <summary>
        /// Gets or sets the module factory returned in the event args
        /// </summary>
        public WinterModuleFactory ModuleFactory
        {
            get { return _moduleFactory; }
            set { _moduleFactory = value; }
        }
    }
}
