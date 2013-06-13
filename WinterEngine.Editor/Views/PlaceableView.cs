using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlatRedBall;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.Editor.Controls;

namespace WinterEngine.Editor.Views
{
    public class PlaceableView : IEditorControl
    {
        #region Fields

        TreeCategoryControl _treeCategoryControl;
        PlaceablePropertiesControl _placeablePropertiesControl;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tree category control
        /// </summary>
        private TreeCategoryControl TreeCategory
        {
            get { return _treeCategoryControl; }
            set { _treeCategoryControl = value; }
        }

        /// <summary>
        /// Gets or sets the placeable properties control
        /// </summary>
        public PlaceablePropertiesControl PlaceableProperties
        {
            get { return _placeablePropertiesControl; }
            set { _placeablePropertiesControl = value; }
        }

        #endregion

        #region Constructors

        public PlaceableView()
        {
            InitializeFormControls();
            InitializeEventSubscriptions();
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Handles loading child controls using the object passed by the tree category control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadObject(object sender, GameObjectEventArgs e)
        {
            PlaceableProperties.LoadPlaceable(e.GameObject as Placeable);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active placeable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveObject(object sender, GameObjectEventArgs e)
        {
            TreeCategory.ActiveGameObject = e.GameObject;
            TreeCategory.RefreshNodeNames();
        }


        #endregion

        #region Methods

        /// <summary>
        /// Shows or hides all user controls managed by this object.
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetVisible(bool isVisible)
        {
            TreeCategory.Visible = isVisible;
            PlaceableProperties.Visible = isVisible;
        }

        /// <summary>
        /// Enables or disables all user controls managed by this object.
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetEnabled(bool isEnabled)
        {
            TreeCategory.Enabled = isEnabled;
            PlaceableProperties.Enabled = isEnabled;
        }

        /// <summary>
        /// Handles subscribing to events.
        /// </summary>
        private void InitializeEventSubscriptions()
        {
            FlatRedBallServices.CornerGrabbingResize += OnScreenResize;
            TreeCategory.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadObject);
            PlaceableProperties.OnSavePlaceable += new EventHandler<GameObjectEventArgs>(SaveObject);
        }

        /// <summary>
        /// Updates positions of controls when the window is resized by user
        /// </summary>
        private void OnScreenResize(object sender, EventArgs e)
        {
            UpdateControlPositions();
        }

        /// <summary>
        /// Handles updating the positions of user controls
        /// </summary>
        private void UpdateControlPositions()
        {
            int menuBarHeight = Control.FromHandle(FlatRedBallServices.WindowHandle).Controls[(int)UserControlIDEnum.MenuBarControl].Height;
            int objectSelectionHeight = Control.FromHandle(FlatRedBallServices.WindowHandle).Controls[(int)UserControlIDEnum.ObjectSelectionControl].Height;
            int viewportWidth = FlatRedBallServices.GraphicsDevice.Viewport.Width;
            int viewportHeight = FlatRedBallServices.GraphicsDevice.Viewport.Height;

            int totalHeight = menuBarHeight + objectSelectionHeight;
            int drawPositionX = 0;
            int drawPositionY = menuBarHeight + objectSelectionHeight + 1;

            // Add the tree category control, offsetting positions so that it isn't drawn on top of other controls
            TreeCategory.Location = new System.Drawing.Point(drawPositionX, drawPositionY);
            TreeCategory.Size = new Size(200, viewportHeight - totalHeight);

            // Add the placeable properties control
            drawPositionX = viewportWidth - PlaceableProperties.Width + 1;
            PlaceableProperties.Location = new System.Drawing.Point(drawPositionX, drawPositionY);
            PlaceableProperties.Size = new Size(PlaceableProperties.Width, viewportHeight - totalHeight);

        }

        /// <summary>
        /// Adds custom user controls to the screen for this particular screen type
        /// </summary>
        private void InitializeFormControls()
        {
            TreeCategory = new TreeCategoryControl();
            PlaceableProperties = new PlaceablePropertiesControl();

            PlaceableProperties.BorderStyle = BorderStyle.None;
            TreeCategory.BorderStyle = BorderStyle.None;
            TreeCategory.GameObjectResourceType = GameObjectTypeEnum.Placeable;

            TreeCategory.Enabled = false;
            PlaceableProperties.Enabled = false;

            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(TreeCategory);
            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(PlaceableProperties);
            UpdateControlPositions();
        }

        public void RefreshAllControls()
        {
            TreeCategory.RefreshTreeView();
            PlaceableProperties.RefreshAllControls();
        }

        public void UnloadAllControls()
        {
            TreeCategory.UnloadTreeView();
            PlaceableProperties.UnloadAllControls();
        }

        #endregion

        #region Control Positioning Methods

        /// <summary>
        /// Returns the width of the tree category control.
        /// </summary>
        /// <returns></returns>
        public int GetLeftWindowWidth()
        {
            return TreeCategory.Width;
        }

        /// <summary>
        /// Returns the width of the Placeable Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowWidth()
        {
            return PlaceableProperties.Width;
        }

        /// <summary>
        /// Unused by this PlaceableView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowWidth()
        {
            return 0;
        }

        /// <summary>
        /// Unused by this PlaceableView control.
        /// </summary>
        /// <returns></returns>
        public int GetBottomWindowWidth()
        {
            return 0;
        }

        /// <summary>
        /// Returns the height of the tree category control.
        /// </summary>
        /// <returns></returns>
        public int GetLeftWindowHeight()
        {
            return TreeCategory.Height;
        }

        /// <summary>
        /// Returns the height of the Placeable Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowHeight()
        {
            return PlaceableProperties.Height;
        }

        /// <summary>
        /// Unused by this PlaceableView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowHeight()
        {
            return 0;
        }

        /// <summary>
        /// Unused by this PlaceableView control
        /// </summary>
        /// <returns></returns>
        public int GetBottomWindowHeight()
        {
            return 0;
        }

        #endregion
    }
}
