using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Editor.ExtendedEventArgs
{
    public class ModuleControlsEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets whether the controls are enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Creates a new ModuleControlsEventArgs object.
        /// </summary>
        /// <param name="enabled">Determines whether or not the controls are enabled (true) or disabled (false)</param>
        public ModuleControlsEventArgs(bool enabled)
        {
            IsEnabled = enabled;
        }

    }
}
