﻿using System.Windows.Forms;
using WinterEngine.Toolset.ExtendedEventArgs;
using System;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.GameObjects;

namespace WinterEngine.Toolset.GUI.Views
{
    public partial class PlaceableView : UserControl, IViewControls
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public PlaceableView()
        {
            InitializeComponent();

            // Subscribe to the OnOpenObject event in the tree category control area.
            treeCategoryControlPlaceable.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadPlaceable);
            // Subscribe to the OnSaveObject event in the area view control.
            placeableViewControl.OnSavePlaceable += new EventHandler<GameObjectEventArgs>(SavePlaceable);
        }
        #endregion

        #region Methods

        public void RefreshControls()
        {
            treeCategoryControlPlaceable.RefreshTreeView();
        }

        public void UnloadControls()
        {
            treeCategoryControlPlaceable.UnloadTreeView();
        }

        /// <summary>
        /// Handles loading child controls using the object passed by the tree category control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadPlaceable(object sender, GameObjectEventArgs e)
        {
            placeableViewControl.LoadPlaceable(e.GameObject as Placeable);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePlaceable(object sender, GameObjectEventArgs e)
        {
            treeCategoryControlPlaceable.ActiveGameObject = e.GameObject;
            treeCategoryControlPlaceable.RefreshNodeNames();
        }

        #endregion
    }
}
