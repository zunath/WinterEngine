using System.Windows.Forms;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class ItemView : UserControl, IViewControls
    {
        #region Constructors

        public ItemView()
        {
            InitializeComponent();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Unloads the tree view and then reloads it with fresh data.
        /// </summary>
        public void RefreshControls()
        {
            treeCategoryControlItem.RefreshTreeView();
        }

        /// <summary>
        /// Handles removing data from the tree view.
        /// </summary>
        public void UnloadControls()
        {
            treeCategoryControlItem.UnloadTreeView();
        }

        #endregion
    }
}
