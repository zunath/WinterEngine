using System;
using WinterEngine.Editor.Managers;
using WinterEngine.Library.Managers;

namespace WinterEngine.Editor.ExtendedEventArgs
{
    public class ModuleCreationEventArgs : EventArgs
    {
        private ModuleManager _moduleFactory;

        /// <summary>
        /// Gets or sets the module factory returned in the event args
        /// </summary>
        public ModuleManager ModuleFactory
        {
            get { return _moduleFactory; }
            set { _moduleFactory = value; }
        }
    }
}
