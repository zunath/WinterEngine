using System.Windows.Forms;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class CreatureView : UserControl, IViewControls
    {
        public CreatureView()
        {
            InitializeComponent();
        }

        public void RefreshControls()
        {
            treeCategoryControlCreature.RefreshTreeView();
        }

    }
}
