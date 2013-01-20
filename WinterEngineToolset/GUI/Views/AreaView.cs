using System;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.Toolset.ExtendedEventArgs;

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
            treeCategoryControlArea.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadObject);
            // Subscribe to the OnSaveObject event in the area view control.
            areaViewControl.OnSaveArea += new EventHandler<GameObjectEventArgs>(SaveObject);
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
        public void LoadObject(object sender, GameObjectEventArgs e)
        {
            areaViewControl.LoadArea(e.GameObject as Area);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveObject(object sender, GameObjectEventArgs e)
        {
            treeCategoryControlArea.ActiveGameObject = e.GameObject;
            treeCategoryControlArea.RefreshNodeNames();
        }

        #endregion
    }
}
