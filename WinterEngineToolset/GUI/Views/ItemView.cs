using System.Windows.Forms;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class ItemView : UserControl, IViewControls
    {
        public ItemView()
        {
            InitializeComponent();
        }

        public void RefreshControls()
        {
            treeCategoryControlItem.RefreshTreeView();
        }
    }
}
