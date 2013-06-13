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
    public class CreatureView : IEditorControl
    {
        #region Fields

        TreeCategoryControl _treeCategoryControl;
        CreaturePropertiesControl _creaturePropertiesControl;

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
        /// Gets or sets the creature properties control
        /// </summary>
        public CreaturePropertiesControl CreatureProperties
        {
            get { return _creaturePropertiesControl; }
            set { _creaturePropertiesControl = value; }
        }

        #endregion

        #region Constructors

        public CreatureView()
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
            CreatureProperties.LoadCreature(e.GameObject as Creature);
        }

        /// <summary>
        /// Handles updating the tree control with the latest version of the active creature.
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
            CreatureProperties.Visible = isVisible;
        }

        /// <summary>
        /// Enables or disables all user controls managed by this object.
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetEnabled(bool isEnabled)
        {
            TreeCategory.Enabled = isEnabled;
            CreatureProperties.Enabled = isEnabled;
        }

        /// <summary>
        /// Handles subscribing to events.
        /// </summary>
        private void InitializeEventSubscriptions()
        {
            FlatRedBallServices.CornerGrabbingResize += OnScreenResize;
            TreeCategory.OnOpenObject += new EventHandler<GameObjectEventArgs>(LoadObject);
            CreatureProperties.OnSaveCreature += new EventHandler<GameObjectEventArgs>(SaveObject);
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

            // Add the creature properties control
            drawPositionX = viewportWidth - CreatureProperties.Width + 1;
            CreatureProperties.Location = new System.Drawing.Point(drawPositionX, drawPositionY);
            CreatureProperties.Size = new Size(CreatureProperties.Width, viewportHeight - totalHeight);

        }

        /// <summary>
        /// Adds custom user controls to the screen for this particular screen type
        /// </summary>
        private void InitializeFormControls()
        {
            TreeCategory = new TreeCategoryControl();
            CreatureProperties = new CreaturePropertiesControl();

            CreatureProperties.BorderStyle = BorderStyle.None;
            TreeCategory.BorderStyle = BorderStyle.None;
            TreeCategory.GameObjectResourceType = GameObjectTypeEnum.Creature;

            TreeCategory.Enabled = false;
            CreatureProperties.Enabled = false;

            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(TreeCategory);
            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(CreatureProperties);
            UpdateControlPositions();
        }

        public void RefreshAllControls()
        {
            TreeCategory.RefreshTreeView();
            CreatureProperties.RefreshAllControls();
        }

        public void UnloadAllControls()
        {
            TreeCategory.UnloadTreeView();
            CreatureProperties.UnloadAllControls();
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
        /// Returns the width of the Creature Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowWidth()
        {
            return CreatureProperties.Width;
        }

        /// <summary>
        /// Unused by this CreatureView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowWidth()
        {
            return 0;
        }

        /// <summary>
        /// Unused by this CreatureView control.
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
        /// Returns the height of the Creature Properties control.
        /// </summary>
        /// <returns></returns>
        public int GetRightWindowHeight()
        {
            return CreatureProperties.Height;
        }

        /// <summary>
        /// Unused by this CreatureView control.
        /// </summary>
        /// <returns></returns>
        public int GetTopWindowHeight()
        {
            return 0;
        }

        /// <summary>
        /// Unused by this CreatureView control
        /// </summary>
        /// <returns></returns>
        public int GetBottomWindowHeight()
        {
            return 0;
        }

        #endregion

    }
}
