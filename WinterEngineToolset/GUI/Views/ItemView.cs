using System.Windows.Forms;
using System;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.GameObjects;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class ItemView : UserControl, IViewControls
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public ItemView()
        {
            InitializeComponent();

            // Subscribe to the OnOpenObject event in the tree category control area.
            treeCategoryControlItem.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadItem);
            // Subscribe to the OnSaveObject event in the area view control.
            itemViewControl.OnSaveItem += new EventHandler<GameObjectEventArgs>(SaveItem);
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

        /// <summary>
        /// Handles loading child controls using the object passed by the tree category control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadItem(object sender, GameObjectEventArgs e)
        {
            itemViewControl.LoadItem(e.GameObject as Item);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveItem(object sender, GameObjectEventArgs e)
        {
            treeCategoryControlItem.ActiveGameObject = e.GameObject;
            treeCategoryControlItem.RefreshNodeNames();
        }

        #endregion
    }
}
