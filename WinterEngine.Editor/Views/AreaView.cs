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
using WinterEngine.Editor.ExtendedEventArgs;

namespace WinterEngine.Editor.Views
{
    public class AreaView : IEditorControl
    {
        #region Fields

        TreeCategoryControl _treeCategoryControl;
        AreaPropertiesControl _areaPropertiesControl;
        AreaNavigationControl _areaNavigationControl;

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

        /// <summary>
        /// Gets or sets the area navigation control
        /// </summary>
        public AreaNavigationControl AreaNavigation
        {
            get { return _areaNavigationControl; }
            set { _areaNavigationControl = value; }
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
            AreaNavigation.Visible = isVisible;
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
            AreaNavigation.OnCameraButtonPress += MoveCamera;
            AreaNavigation.OnObjectRotationButtonPress += RotateObject;
            AreaNavigation.OnCameraButtonReleased += StopCameraMovement;
            AreaNavigation.OnObjectRotationButtonReleased += StopObjectRotation;
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

            // Set up the tree category control, offsetting positions so that it isn't drawn on top of other controls
            TreeCategory.Location = new Point(drawPositionX, drawPositionY);
            TreeCategory.Size = new Size(200, viewportHeight - totalHeight);

            // Set up the area properties control
            drawPositionX = viewportWidth - AreaProperties.Width + 1;
            AreaProperties.Location = new Point(drawPositionX, drawPositionY);
            AreaProperties.Size = new Size(AreaProperties.Width, viewportHeight - totalHeight);

            // Set up the area navigation control
            AreaNavigation.Location = new Point(TreeCategory.Size.Width + 2, viewportHeight - AreaNavigation.Height);
            AreaNavigation.Size = new Size(viewportWidth - TreeCategory.Size.Width - AreaProperties.Size.Width - 3, AreaNavigation.Height);

        }

        /// <summary>
        /// Adds custom user controls to the screen for this particular screen type
        /// </summary>
        private void InitializeFormControls()
        {
            TreeCategory = new TreeCategoryControl();
            AreaProperties = new AreaPropertiesControl();
            AreaNavigation = new AreaNavigationControl();

            AreaProperties.BorderStyle = BorderStyle.None;
            TreeCategory.BorderStyle = BorderStyle.None;
            TreeCategory.GameObjectResourceType = GameObjectTypeEnum.Area;

            AreaNavigation.BorderStyle = BorderStyle.None;

            TreeCategory.Enabled = false;
            AreaProperties.Enabled = false;
            
            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(TreeCategory);
            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(AreaProperties);
            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(AreaNavigation);
            UpdateControlPositions();
        }

        /// <summary>
        /// Moves the camera left, right, up, or down depending on the type sent
        /// from the AreaNavigation user control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveCamera(object sender, CameraButtonPressEventArgs e)
        {
            const float MovementSpeed = 10.0f;

            switch (e.MovementType)
            {
                case CameraMovementTypeEnum.Down:
                    SpriteManager.Camera.YVelocity = -MovementSpeed;
                    break;
                case CameraMovementTypeEnum.Left:
                    SpriteManager.Camera.XVelocity = -MovementSpeed;
                    break;
                case CameraMovementTypeEnum.Right:
                    SpriteManager.Camera.XVelocity = MovementSpeed;
                    break;
                case CameraMovementTypeEnum.Up:
                    SpriteManager.Camera.YVelocity = MovementSpeed;
                    break;
            }
        }

        /// <summary>
        /// Rotates the selected object clockwise or counter-clockwise depending on the type
        /// sent from the AreaNavigation user control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateObject(object sender, ObjectRotationButtonPressEventArgs e)
        {
            switch (e.RotationType)
            {
                case ObjectRotationTypeEnum.Clockwise:
                    break;
                case ObjectRotationTypeEnum.CounterClockwise:
                    break;
            }
        }

        /// <summary>
        /// Stops rotation of the selected object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopObjectRotation(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Stops movement of the camera.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopCameraMovement(object sender, EventArgs e)
        {
            SpriteManager.Camera.XVelocity = 0.0f;
            SpriteManager.Camera.YVelocity = 0.0f;
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
        /// Returns the width of the Area Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowWidth()
        {
            return AreaProperties.Width;
        }

        /// <summary>
        /// Unused by this AreaView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowWidth()
        {
            return 0;
        }

        /// <summary>
        /// Returns the width of the area navigation control.
        /// </summary>
        /// <returns></returns>
        public int GetBottomWindowWidth()
        {
            return AreaNavigation.Width;
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
        /// Returns the height of the Area Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowHeight()
        {
            return AreaProperties.Height;
        }

        /// <summary>
        /// Unused by this AreaView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowHeight()
        {
            return 0;
        }

        /// <summary>
        /// Returns the height of the area navigation control.
        /// </summary>
        /// <returns></returns>
        public int GetBottomWindowHeight()
        {
            return AreaNavigation.Height;
        }

        #endregion
    }
}
