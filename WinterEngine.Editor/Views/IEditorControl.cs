using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.Editor.Views
{
    public interface IEditorControl
    {
        void SetVisible(bool isVisible);
        void SetEnabled(bool isEnabled);
    }
}
