using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlatRedBall;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.Editor.Controls;
using WinterEngine.Editor.Enums;

namespace WinterEngine.Editor.Views
{
    public class AreaView : IEditorControl
    {
        #region Fields

        TreeCategoryControl _treeCategoryControl;
        AreaPropertiesControl _areaPropertiesControl;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tree category control
        /// </summary>
        public TreeCategoryControl TreeCategory
        {
            get { return _treeCategoryControl; }
            set { _treeCategoryControl = value; }
        }

        /// <summary>
        /// Gets or sets the area properties control
        /// </summary>
        public AreaPropertiesControl AreaProperties
        {
            get { return _areaPropertiesControl; }
            set { _areaPropertiesControl = value; }
        }

        #endregion

        #region Constructors

        public AreaView()
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
            AreaProperties.LoadArea(e.GameObject as Area);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active area.
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
            AreaProperties.Visible = isVisible;
        }

        /// <summary>
        /// Enables or disables all user controls managed by this object.
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetEnabled(bool isEnabled)
        {
            TreeCategory.Enabled = isEnabled;
            AreaProperties.Enabled = isEnabled;
        }

        /// <summary>
        /// Handles subscribing to events.
        /// </summary>
        private void InitializeEventSubscriptions()
        {
            FlatRedBallServices.CornerGrabbingResize += OnScreenResize;
            TreeCategory.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadObject);
            AreaProperties.OnSaveArea += new EventHandler<GameObjectEventArgs>(SaveObject);
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

            // Add the area properties control
            drawPositionX = viewportWidth - AreaProperties.Width + 1;
            AreaProperties.Location = new System.Drawing.Point(drawPositionX, drawPositionY);
            AreaProperties.Size = new Size(AreaProperties.Width, viewportHeight - totalHeight);

        }

        /// <summary>
        /// Adds custom user controls to the screen for this particular screen type
        /// </summary>
        private void InitializeFormControls()
        {
            TreeCategory = new TreeCategoryControl();
            AreaProperties = new AreaPropertiesControl();

            AreaProperties.BorderStyle = BorderStyle.None;
            TreeCategory.BorderStyle = BorderStyle.None;
            TreeCategory.GameObjectResourceType = ResourceTypeEnum.Area;

            TreeCategory.Enabled = false;
            AreaProperties.Enabled = false;

            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(TreeCategory);
            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(AreaProperties);
            UpdateControlPositions();
        }


        #endregion
    }
}
