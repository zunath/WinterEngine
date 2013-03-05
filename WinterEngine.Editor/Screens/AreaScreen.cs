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
using WinterEngine.Forms.Shared;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using WinterEngine.Forms.Toolset;
using System.Windows.Forms;
using System.Drawing;
using WinterEngine.Editor.Controls;
using WinterEngine.Editor.Enums;
#endif

namespace WinterEngine.Editor.Screens
{
	public partial class AreaScreen
    {
        #region Fields

        TreeCategoryControl _treeCategoryControl;
        AreaPropertiesControl _areaPropertiesControl;

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
        /// Gets or sets the area properties control
        /// </summary>
        public AreaPropertiesControl AreaProperties
        {
            get { return _areaPropertiesControl; }
            set { _areaPropertiesControl = value; }
        }

        #endregion

        #region FRB Methods

        void CustomInitialize()
		{
            InitializeFormControls();
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

        #region Methods

        private void InitializeFormControls()
        {
            int menuBarHeight = Control.FromHandle(FlatRedBallServices.WindowHandle).Controls[(int)UserControlIDEnum.MenuBarControl].Height;
            int objectSelectionHeight = Control.FromHandle(FlatRedBallServices.WindowHandle).Controls[(int)UserControlIDEnum.ObjectSelectionControl].Height;
            int viewportWidth = FlatRedBallServices.GraphicsDevice.Viewport.Width;
            int viewportHeight = FlatRedBallServices.GraphicsDevice.Viewport.Height;

            int totalHeight = menuBarHeight + objectSelectionHeight;

            int drawPositionX = 0;
            int drawPositionY = menuBarHeight + objectSelectionHeight + 1;
            
            // Add the tree category control, offsetting positions so that it isn't drawn on top of other controls
            TreeCategory = new TreeCategoryControl();
            TreeCategory.Location = new System.Drawing.Point(drawPositionX, drawPositionY);
            TreeCategory.BorderStyle = BorderStyle.None;
            TreeCategory.Size = new Size(100, viewportHeight - totalHeight);

            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(TreeCategory);


            // Add the area properties control
            AreaProperties = new AreaPropertiesControl();
            drawPositionX = viewportWidth - AreaProperties.Width + 1;
            AreaProperties.Location = new System.Drawing.Point(drawPositionX, drawPositionY);
            AreaProperties.BorderStyle = BorderStyle.None;
            AreaProperties.Size = new Size(AreaProperties.Width, viewportHeight - totalHeight);

            Control.FromHandle(FlatRedBallServices.WindowHandle).Controls.Add(AreaProperties);


        }


        #endregion

    }
}
