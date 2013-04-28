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
using WinterEngine.Editor.Enums;

namespace WinterEngine.Editor.Views
{
    public class ItemView : IEditorControl
    {
        #region Fields

        TreeCategoryControl _treeCategoryControl;
        ItemPropertiesControl _itemPropertiesControl;

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
        /// Gets or sets the item properties control
        /// </summary>
        public ItemPropertiesControl ItemProperties
        {
            get { return _itemPropertiesControl; }
            set { _itemPropertiesControl = value; }
        }

        #endregion

        #region Constructors

        public ItemView()
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
            ItemProperties.LoadItem(e.GameObject as Item);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active item.
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
            ItemProperties.Visible = isVisible;
        }

        /// <summary>
        /// Enables or disables all user controls managed by this object.
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetEnabled(bool isEnabled)
        {
            TreeCategory.Enabled = isEnabled;
            ItemProperties.Enabled = isEnabled;
        }

        /// <summary>
        /// Handles subscribing to events.
        /// </summary>
        private void InitializeEventSubscriptions()
        {
            FlatRedBallServices.CornerGrabbingResize += OnScreenResize;
            TreeCategory.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadObject);
            ItemProperties.OnSaveItem += new EventHandler<GameObjectEventArgs>(SaveObject);
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

            // Add the item properties control
            drawPositionX = viewportWidth - ItemProperties.Width + 1;
            ItemProperties.Location = new System.Drawing.Point(drawPositionX, drawPositionY);
            ItemProperties.Size = new Size(ItemProperties.Width, viewportHeight - totalHeight);

        }

        /// <summary>
        /// Adds custom user controls to the screen for this particular screen type
        /// </summary>
        private void InitializeFormControls()
        {
            TreeCategory = new TreeCategoryControl();
            ItemProperties = new ItemPropertiesControl();

            ItemProperties.BorderStyle = BorderStyle.None;
            TreeCategory.BorderStyle = BorderStyle.None;
            TreeCategory.GameObjectResourceType = GameObjectTypeEnum.Item;

            TreeCategory.Enabled = false;
            ItemProperties.Enabled = false;

            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(TreeCategory);
            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(ItemProperties);
            UpdateControlPositions();
        }

        public void RefreshAllControls()
        {
            TreeCategory.RefreshTreeView();
            ItemProperties.RefreshAllControls();
        }

        public void UnloadAllControls()
        {
            TreeCategory.UnloadTreeView();
            ItemProperties.UnloadAllControls();
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
        /// Returns the width of the Item Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowWidth()
        {
            return ItemProperties.Width;
        }

        /// <summary>
        /// Unused by this ItemView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowWidth()
        {
            return 0;
        }

        /// <summary>
        /// Unused by this ItemView control.
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
        /// Returns the height of the Item Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowHeight()
        {
            return ItemProperties.Height;
        }

        /// <summary>
        /// Unused by this ItemView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowHeight()
        {
            return 0;
        }

        /// <summary>
        /// Unused by this ItemView control
        /// </summary>
        /// <returns></returns>
        public int GetBottomWindowHeight()
        {
            return 0;
        }

        #endregion
    }
}
