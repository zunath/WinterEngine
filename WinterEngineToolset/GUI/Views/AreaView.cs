using System.Windows.Forms;
using WinterEngine.Toolset.ExtendedEventArgs;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.GameObjects;
using System;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class AreaView : UserControl, IViewControls
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public AreaView()
        {
            InitializeComponent();

            // Subscribe to the OnOpenObject event in the tree category control area.
            treeCategoryControlArea.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadArea);
            // Subscribe to the OnSaveObject event in the area view control.
            areaViewControl.OnSaveArea += new EventHandler<GameObjectEventArgs>(SaveArea);
        }
        #endregion

        #region Methods

        public void RefreshControls()
        {
            treeCategoryControlArea.RefreshTreeView();
        }

        public void UnloadControls()
        {
            treeCategoryControlArea.UnloadTreeView();
        }

        /// <summary>
        /// Handles loading child controls using the object passed by the tree category control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadArea(object sender, GameObjectEventArgs e)
        {
            areaViewControl.LoadArea(e.GameObject as Area);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveArea(object sender, GameObjectEventArgs e)
        {
            treeCategoryControlArea.ActiveGameObject = e.GameObject;
            treeCategoryControlArea.RefreshNodeNames();
        }

        #endregion
    }
}
