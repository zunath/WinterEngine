using System;
using System.Windows.Forms;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class GraphicView : UserControl, IViewControls
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public GraphicView()
        {
            InitializeComponent();

            // Subscribe to the OnOpenObject event in the tree category control area.
            treeCategoryControlGraphic.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadObject);
            // Subscribe to the OnSaveObject event in the area view control.
            //graphicViewControl.OnSaveArea += new EventHandler<GameObjectEventArgs>(SaveObject);
        }
        #endregion

        #region Methods

        public void RefreshControls()
        {
            treeCategoryControlGraphic.RefreshTreeView();
        }

        public void UnloadControls()
        {
            treeCategoryControlGraphic.UnloadTreeView();
        }

        /// <summary>
        /// Handles loading child controls using the object passed by the tree category control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadObject(object sender, GameObjectEventArgs e)
        {
            //graphicViewControl.LoadArea(e.GameObject as Area);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveObject(object sender, GameObjectEventArgs e)
        {
            treeCategoryControlGraphic.ActiveGameObject = e.GameObject;
            treeCategoryControlGraphic.RefreshNodeNames();
        }

        #endregion
    }
}
