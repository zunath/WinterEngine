using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Editor.Controls
{
    interface IPropertyControl
    {
        void RefreshAllControls();
        void UnloadAllControls();
    }
}
