using System.Windows.Forms;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class PlaceableView : UserControl, IViewControls
    {
        public PlaceableView()
        {
            InitializeComponent();
        }

        public void RefreshControls()
        {
            treeCategoryControlPlaceable.RefreshTreeView();
        }

        public void UnloadControls()
        {
            treeCategoryControlPlaceable.UnloadTreeView();
        }
    }
}
