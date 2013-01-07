using System.Windows.Forms;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class AreaView : UserControl, IViewControls
    {
        public AreaView()
        {
            InitializeComponent();
        }

        public void RefreshControls()
        {
            treeCategoryControlArea.RefreshTreeView();
        }

        public void UnloadControls()
        {
            treeCategoryControlArea.UnloadTreeView();
        }

    }
}
