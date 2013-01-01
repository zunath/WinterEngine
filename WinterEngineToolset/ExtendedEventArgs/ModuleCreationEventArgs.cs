using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Toolset.ExtendedEventArgs
{
    public class ModuleCreationEventArgs : EventArgs
    {
        private string _temporaryPathDirectory;

        public string TemporaryPathDirectory
        {
            get { return _temporaryPathDirectory; }
            set { _temporaryPathDirectory = value; }
        }
    }
}
