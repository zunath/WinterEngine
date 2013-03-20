using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Library.Factories;

namespace WinterEngine.Editor.ExtendedEventArgs
{
    public class ModuleCreationEventArgs : EventArgs
    {
        private ModuleFactory _moduleFactory;

        /// <summary>
        /// Gets or sets the module factory returned in the event args
        /// </summary>
        public ModuleFactory ModuleFactory
        {
            get { return _moduleFactory; }
            set { _moduleFactory = value; }
        }
    }
}
