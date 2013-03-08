using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.Editor.Controls;
using System.Windows.Forms;
using WinterEngine.Editor.ExtendedEventArgs;
using WinterEngine.Editor.Enums;
using System.Drawing;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.Editor.Views;
#endif

namespace WinterEngine.Editor.Screens
{
	public partial class EditorScreen
    {
        #region Fields

        private ObjectBar _objectBar;
        private MenuBarControl _menuBar;
        private AreaView _areaView;
        private CreatureView _creatureView;
        private ItemView _itemView;
        private PlaceableView _placeableView;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the WinForms control for object selection.
        /// </summary>
        public ObjectBar ObjectSelectionBar
        {
            get { return _objectBar; }
            set { _objectBar = value; }
        }

        /// <summary>
        /// Gets or sets the WinForms control for the menu bar.
        /// </summary>
        public MenuBarControl MenuBar
        {
            get { return _menuBar; }
            set { _menuBar = value; }
        }

        /// <summary>
        /// Gets or sets the AreaView control.
        /// </summary>
        private AreaView AreaControl
        {
            get { return _areaView; }
            set { _areaView = value; }
        }

        /// <summary>
        /// Gets or sets the CreatureView control
        /// </summary>
        private CreatureView CreatureControl
        {
            get { return _creatureView; }
            set { _creatureView = value; }
        }

        /// <summary>
        /// Gets or sets the ItemView control
        /// </summary>
        private ItemView ItemControl
        {
            get { return _itemView; }
            set { _itemView = value; }
        }

        /// <summary>
        /// Gets or sets the PlaceableView control
        /// </summary>
        private PlaceableView PlaceableControl
        {
            get { return _placeableView; }
            set { _placeableView = value; }
        }

        #endregion

        #region FRB Events

        void CustomInitialize()
		{
            InitializeFormControls();
            FlatRedBallServices.CornerGrabbingResize += OnWindowResize;
            FlatRedBallServices.Game.IsMouseVisible = true;
            InitializeEventSubscriptions();

            SpriteManager.AddSprite("redball.bmp");
		}

		void CustomActivity(bool firstTimeCalled)
		{

		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Handles subscribing events on screen initialization.
        /// </summary>
        private void InitializeEventSubscriptions()
        {
            MenuBar.OnToggleControls += MenuBar_OnToggleControls;
            MenuBar.OnRefreshControls += RefreshTreeViews;

            FlatRedBallServices.CornerGrabbingResize += OnWindowResize;
        }

        void MenuBar_OnToggleControls(object sender, ModuleControlsEventArgs e)
        {
            AreaControl.SetEnabled(e.IsEnabled);
            CreatureControl.SetEnabled(e.IsEnabled);
            ItemControl.SetEnabled(e.IsEnabled);
            PlaceableControl.SetEnabled(e.IsEnabled);
        }


        /// <summary>
        /// Handles refreshing the tree view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshTreeViews(object sender, EventArgs e)
        {
            AreaControl.TreeCategory.RefreshTreeView();
            CreatureControl.TreeCategory.RefreshTreeView();
            ItemControl.TreeCategory.RefreshTreeView();
            PlaceableControl.TreeCategory.RefreshTreeView();
        }


        private void ObjectSelectionBar_OnObjectSelected(object sender, ObjectSelectionEventArgs e)
        {
            AreaControl.SetVisible(false);
            CreatureControl.SetVisible(false);
            ItemControl.SetVisible(false);
            PlaceableControl.SetVisible(false);

            switch (e.ObjectType)
            {
                case ObjectSelectionTypeEnum.Advanced:
                    break;
                case ObjectSelectionTypeEnum.Area:
                    AreaControl.SetVisible(true);
                    break;
                case ObjectSelectionTypeEnum.Conversation:
                    break;
                case ObjectSelectionTypeEnum.Creature:
                    CreatureControl.SetVisible(true);
                    break;
                case ObjectSelectionTypeEnum.Item:
                    ItemControl.SetVisible(true);
                    break;
                case ObjectSelectionTypeEnum.Placeable:
                    PlaceableControl.SetVisible(true);
                    break;
                case ObjectSelectionTypeEnum.Script:
                    break;

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles creating the form controls and drawing them in the appropriate locations
        /// </summary>
        private void InitializeFormControls()
        {
            // Force the window's minimum size based on its initial width and height
            Control mainWindow = Control.FromHandle(FlatRedBallServices.WindowHandle);
            mainWindow.MinimumSize = new Size(mainWindow.Width, mainWindow.Height);

            MenuBar = new MenuBarControl();
            ObjectSelectionBar = new ObjectBar();

            mainWindow.Controls.Add(MenuBar);
            mainWindow.Controls.Add(ObjectSelectionBar);

            ObjectSelectionBar.OnObjectSelected += ObjectSelectionBar_OnObjectSelected;

            UpdateControlPositions();

            AreaControl = new AreaView();
            CreatureControl = new CreatureView();
            ItemControl = new ItemView();
            PlaceableControl = new PlaceableView();
        }

        /// <summary>
        /// Relocates windows to the appropriate positions
        /// </summary>
        private void UpdateControlPositions()
        {
            MenuBar.Location = new System.Drawing.Point(0, 0);
            MenuBar.BorderStyle = BorderStyle.None;
            MenuBar.Size = new Size(FlatRedBallServices.GraphicsDevice.Viewport.Width, MenuBar.Height);

            ObjectSelectionBar.Location = new System.Drawing.Point(0, MenuBar.Size.Height + 1);
            ObjectSelectionBar.BorderStyle = BorderStyle.None;
            ObjectSelectionBar.Size = new Size(FlatRedBallServices.GraphicsDevice.Viewport.Width, ObjectSelectionBar.Height);
        }

        /// <summary>
        /// Updates the positions of controls when the main window is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowResize(object sender, EventArgs e)
        {
            UpdateControlPositions();
        }

        #endregion

    }
}
