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
using FlatRedBall.Screens;
using WinterEngine.DataTransferObjects.EventArgsExtended;
#endif

namespace WinterEngine.Game.Screens
{
	public partial class MainMenuScreen
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Events / Delegates

        #endregion

        #region Methods

        void CustomInitialize()
        {
            MainMenuGuiEntityInstance.OnChangeScreen += base.ChangeScreen;
		}

		void CustomActivity(bool firstTimeCalled)
		{


		}

		void CustomDestroy()
		{
            MainMenuGuiEntityInstance.OnChangeScreen -= base.ChangeScreen;
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        #endregion

    }
}
