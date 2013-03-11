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
        int GetLeftWindowWidth();
        int GetRightWindowWidth();
        int GetTopWindowWidth();
        int GetBottomWindowWidth();
        int GetLeftWindowHeight();
        int GetRightWindowHeight();
        int GetTopWindowHeight();
        int GetBottomWindowHeight();
    }
}
